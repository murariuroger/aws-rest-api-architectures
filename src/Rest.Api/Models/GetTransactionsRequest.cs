namespace Rest.Api.Models
{
    public class GetTransactionsRequest
    {
        public string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
