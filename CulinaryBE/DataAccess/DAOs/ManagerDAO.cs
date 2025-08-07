using BusinessObject.AppDbContext;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class ManagerDAO : IManagerDAO
    {
        public async Task<Manager> GetManagerAccount(LoginAccountModel loginAccountModel)
        {
            using (var context = new CulinaryContext())
            {
                //var user = await context.Managers
                // .Where(u => u.Email == loginAccountModel.Email && u.Password == loginAccountModel.Password)
                // .Select(u => new
                // {
                //     u.FullName,
                //     u.Email,
                //     Role = new
                //     {
                //         u.Role.RoleName,
                //         RolePermissions = u.Role.RolePermissions.Select(rp => new
                //         {
                //             PermissionName = rp.Permission.PermissionName
                //         }).ToList()
                //     }
                // })
                // .FirstOrDefaultAsync();

                var user = await context.Managers
                    .Include(m => m.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                    .FirstOrDefaultAsync(u => u.Email == loginAccountModel.Email && u.Password == loginAccountModel.Password);
                return user;
            }
        }
    }
}
