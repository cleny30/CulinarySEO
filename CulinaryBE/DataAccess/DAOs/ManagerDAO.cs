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
    public class ManagerDAO : IManagerDAO
    {
        private readonly CulinaryContext _context;

        public ManagerDAO(CulinaryContext contextl)
        {
            _context = contextl;
        }

        public async Task<AccountData> VerifyAccountAsync(LoginAccountModel model)
        {

                try
                {
                    // Bước 1: Lấy thông tin user từ DB (bao gồm password hash)
                    var manager = await _context.Managers
                        .Include(m => m.Role)
                            .ThenInclude(r => r.RolePermissions)
                                .ThenInclude(rp => rp.Permission)
                        .Where(m => (m.Email == model.Email) && m.Status != UserStatus.Suspended)
                        .FirstOrDefaultAsync();

                    // Bước 2: Kiểm tra user có tồn tại không
                    if (manager == null)
                    {
                        throw new NotFoundException("Invalid email or password");
                    }

                    // Bước 3: Verify password
                    if (!VerifyPassword(model.Password, manager.Password))
                    {
                        throw new NotFoundException("Invalid email or password");
                    }

                    // Bước 4: Trả về thông tin user nếu password đúng
                    return new AccountData
                    {
                        UserId = manager.ManagerId,
                        FullName = manager.FullName,
                        Phone = manager.Phone,
                        Email = manager.Email,
                        RoleName = manager.Role.RoleName,
                        Permissions = manager.Role.RolePermissions
                            .Select(rp => rp.Permission.PermissionName)
                            .ToList()
                    };
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("An error occurred while retrieving the manager account.", ex);
                }
            
        }

        public async Task<string> SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryDate)
        {

            try
            {
                var manager = await _context.Managers
                    .FirstOrDefaultAsync(m => m.ManagerId == userId);

                if (manager == null)
                {
                    throw new NotFoundException("Manager not found");
                }

                manager.Token = refreshToken;
                manager.ExpiresAt = expiryDate;
                manager.Revoked = false;

                _context.Managers.Update(manager);
                await _context.SaveChangesAsync();

                return manager.Token;
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to save refresh token: " + ex.Message);
            }
        }

        public async Task<Manager?> GetRefreshTokenAsync(string refreshToken)
        {
            try
            {
                return await _context.Managers
                    .Include(m => m.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
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
                var manager = await _context.Managers
                    .FirstOrDefaultAsync(m => m.Token == refreshToken);

                if (manager != null)
                {
                    manager.Revoked = true;
                    _context.Managers.Update(manager);
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
    }
}
