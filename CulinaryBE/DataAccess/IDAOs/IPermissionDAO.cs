namespace DataAccess.IDAOs
{
    public interface IPermissionDAO
    {
        Task<HashSet<KeyValuePair<string, bool>>> GetPermissionsByCustomerIdAsync(Guid userId);
    }
}
