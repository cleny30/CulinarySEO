using BusinessObject.AppDbContext;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataAccess.DAOs
{
    public class CustomerDAO : ICustomerDAO
    {
        private readonly CulinaryContext _context;

        public CustomerDAO(CulinaryContext contextl)
        {
            _context = contextl;
        }

        public async Task<AccountData> VerifyAccountAsync(LoginAccountModel model)
        {

            try
            {
                var customer = await _context.Customers
                    .Where(m => (m.Email == model.Email) && m.Status != UserStatus.Suspended)
                    .FirstOrDefaultAsync();

                if (customer == null)
                {
                    throw new NotFoundException("Invalid email or password");
                }

                if (!VerifyPassword(model.Password, customer.Password))
                {
                    throw new NotFoundException("Invalid email or password");
                }

                return new AccountData
                {
                    UserId = customer.CustomerId,
                    FullName = customer.FullName,
                    Phone = customer.Phone,
                    ProfilePic = customer.ProfilePic,
                    Email = customer.Email,
                    RoleName = "Customer"                
                };
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving the customer account.", ex);
            }

        }

        public async Task<string> SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryDate)
        {

            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.CustomerId == userId);

                if (customer == null)
                {
                    throw new NotFoundException("Customer not found");
                }

                customer.Token = refreshToken;
                customer.ExpiresAt = expiryDate;
                customer.Revoked = false;

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                return customer.Token;
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to save refresh token: " + ex.Message);
            }
        }

        public async Task<Customer?> GetRefreshTokenAsync(string refreshToken)
        {
            try
            {
                return await _context.Customers
                    .FirstOrDefaultAsync(m => m.Token == refreshToken && m.Revoked != true && m.ExpiresAt > DateTime.UtcNow);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to retrieve refresh token: " + ex.Message);
            }
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {

            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.Token == refreshToken);

                if (customer != null)
                {
                    customer.Revoked = true;
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to revoke refresh token: " + ex.Message);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }

        // Method này chỉ dùng khi tạo user mới hoặc đổi password
        private string GeneratePasswordHash(string password)
        {
            var hasher = new PasswordHasher<object>();
            string passwordHash = hasher.HashPassword(null, password);
            return passwordHash;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            try
            {
                return await _context.Customers.AnyAsync(m => m.Email == email);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to check email exist: " + ex.Message);
            }
        }

        public async Task<bool> AddNewCustomer(Customer customer)
        {
            try
            {
                customer.Password = GeneratePasswordHash(customer.Password!);

                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to add new customer: " + ex.Message);
            }
        }
    }
}
