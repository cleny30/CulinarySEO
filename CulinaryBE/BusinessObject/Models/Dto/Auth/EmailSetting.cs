namespace BusinessObject.Models.Dto
{
    public class EmailSetting
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public record EmailQueueItem(string ToEmail, string Otp);

}
