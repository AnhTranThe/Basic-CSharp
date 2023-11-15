namespace Basic_CSharp.Models
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid LogInUserId { get; set; } = Guid.Empty;
    }
}
