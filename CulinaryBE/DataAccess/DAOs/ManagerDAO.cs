using BusinessObject.AppDbContext;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.DAOs
{
    public class ManagerDAO : IManagerDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<ManagerDAO> _logger;

        public ManagerDAO(CulinaryContext context, ILogger<ManagerDAO> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<bool> IsEmailExist(string email)
        {
            try
            {
                return await _context.Managers.AnyAsync(m => m.Email == email);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to check if email exists: " + ex.Message);
            }
        }

        public async Task AddManager(Manager model)
        {
            _logger.LogInformation("Adding new manager with email: {Email}", model.Email);
            try
            {
                await _context.Managers.AddAsync(model);
                await _context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to add manager: " + ex.Message);
            }
        }

        public async Task<bool> UpdateManager(Manager model)
        {
            _logger.LogInformation("Updating manager with ID: {ManagerId}", model.ManagerId);
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to update manager: " + ex.Message);
            }
        }

        public async Task<bool> DeleteManager(Guid managerId)
        {
            _logger.LogInformation("Deleting manager with ID: {ManagerId}", managerId);
            try
            {
                Manager? manager = await GetManagerById(managerId);
                if (manager == null)
                {
                    return false;
                }
                manager.Status = UserStatus.Suspended;
                _context.Entry(manager).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to delete manager: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Manager>> GetManagers()
        {
            _logger.LogInformation("Retrieving all managers from the database");
            try
            {
                return await _context.Managers.ToListAsync();
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to retrieve managers: " + ex.Message);
            }
        }

        public async Task<Manager?> GetManagerById(Guid managerId)
        {
            _logger.LogInformation("Retrieving manager with ID: {ManagerId}", managerId);
            try
            {
                return await _context.Managers.FirstOrDefaultAsync(m => m.ManagerId == managerId);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to retrieve manager by ID: " + ex.Message);
            }
        }

        public async Task<bool> IsEmailOfAccount(string email, Guid guid)
        {
            try
            {
                return await _context.Managers.AnyAsync(m => m.ManagerId != guid && m.Email == email);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to check email of account with ID: " + ex.Message);
            }
        }
    }
}

