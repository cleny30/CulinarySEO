using AutoMapper;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleDAO _roleDAO;
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public RoleService(IRoleDAO roleDAO, IMapper mapper, IPermissionService permissionService)
        {
            _roleDAO = roleDAO;
            _mapper = mapper;
            _permissionService = permissionService;
        }

        public async Task AddRole(RoleDto roleDto)
        {
            try
            {
                Role role = _mapper.Map<Role>(roleDto);
                await _roleDAO.AddRole(role);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the role.", ex);
            }
        }

        public async Task<RoleDto?> GetRoleById(int roleId)
        {
            Role? role = await _roleDAO.GetRoleById(roleId);
            return role == null ? null : _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            IEnumerable<Role> roles = await _roleDAO.GetRoles();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task UpdateRole(RoleDto roleDto)
        {
            try
            {
                Role role = _mapper.Map<Role>(roleDto);
                await _roleDAO.UpdateRole(role);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the role.", ex);
            }
        }

        public async Task<byte[]> ExportRolesPermission()
        {
            try
            {
                IEnumerable<PermissionDto> permissions = await _permissionService.GetAllPermissions();
                IEnumerable<RoleDto> roles = await GetRoles();
                IEnumerable<RolePermission> rolePermissions = await _roleDAO.GetRolePermissions();

                using (var workbook = new NPOI.XSSF.UserModel.XSSFWorkbook())
                {
                    //Style for the header row
                    var sheet = workbook.CreateSheet("Roles - Permissions");
                    var headerStyle = workbook.CreateCellStyle();
                    var headerFont = workbook.CreateFont();
                    headerFont.IsBold = true;
                    headerFont.FontHeightInPoints = 14;
                    headerStyle.SetFont(headerFont);
                    headerStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                    var headerRow = sheet.CreateRow(0);
                    var headerCell = headerRow.CreateCell(0);
                    headerCell.SetCellValue("Quyền của các vai trò hệ thống CulinarySEO");
                    headerCell.CellStyle = headerStyle;

                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, roles.Count()));

                    var roleRow = sheet.CreateRow(1);
                    int colInex = 1;
                    foreach (var role in roles)
                    {
                        roleRow.CreateCell(colInex++).SetCellValue(role.RoleName);
                    }

                    int rowIndex = 2;
                    foreach (var per in permissions)
                    {
                        var permissionRow = sheet.CreateRow(rowIndex++);
                        var permissionCell = permissionRow.CreateCell(0);
                        permissionCell.SetCellValue(per.PermissionName);
                        colInex = 1;
                        foreach (var role in roles)
                        {
                            var rolePermission = rolePermissions.FirstOrDefault(rp => rp.RoleId == role.RoleId && rp.PermissionId == per.PermissionId);
                            var cell = permissionRow.CreateCell(colInex++);
                            cell.SetCellValue(rolePermission != null ? "✓" : "✗");
                        }
                    }

                    for (int i = 0; i <= roles.Count(); i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.Write(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while exporting roles and permissions.", ex);
            }
        }
    }
}
