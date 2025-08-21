using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.DAOs
{
    public class RoleDAO : IRoleDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<RoleDAO> _logger;
        public RoleDAO(CulinaryContext context, ILogger<RoleDAO> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddRole(Role role)
        {
            _logger.LogInformation("Adding new role: {RoleName}", role.RoleName);
            try
            {
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                throw new Exception("Database error occurred while adding role.", ex);
            }
        }

        public async Task<Role?> GetRoleById(int roleId)
        {
            _logger.LogInformation("Retrieving role by ID: {RoleId}", roleId);
            try
            {
                return await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.RoleId == roleId);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception("Database error occurred while retrieving role by ID.", ex);
            }
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            _logger.LogInformation("Retrieving all roles from the database.");
            try
            {
                return await _context.Roles.AsNoTracking().ToListAsync();
            }
            catch (NpgsqlException ex)
            {
                throw new Exception("Database error occurred while retrieving roles.", ex);
            }
        }

        public async Task UpdateRole(Role role)
        {
            _logger.LogInformation("Updating role: {RoleName}", role.RoleName);
            try
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                throw new Exception("Database error occurred while updating role.", ex);
            }
        }

        public async Task<IEnumerable<RolePermission>> GetRolePermissions()
        {
            try
            {
                return await _context.RolePermissions
                    .Include(rp => rp.Role)
                    .Include(rp => rp.Permission)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (NpgsqlException ex)
            {
                throw new Exception("Database error occurred while retrieving role permissions.", ex);
            }
        }
    }
}