namespace UniMagContributions.Models
{
    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public EmailContent Content { get; set; }
    }
}
