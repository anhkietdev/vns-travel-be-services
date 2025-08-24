namespace DAL.Models
{
    public class OtpCode
    {
        public int OtpCodeId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime Expiry { get; set; }
    }
}
