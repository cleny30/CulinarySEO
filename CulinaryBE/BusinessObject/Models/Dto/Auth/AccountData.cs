﻿namespace BusinessObject.Models.Dto
{
    public class AccountData
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public List<string>? Permissions { get; set; }
    }
}
