using System.Text.Json.Serialization;

namespace BusinessObject.Models.Dto
{
    public class AccountData
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfilePic { get; set; }
        public string RoleName { get; set; } = string.Empty;
        [JsonIgnore]
        public List<string>? Permissions { get; set; }
    }
}
