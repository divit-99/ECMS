namespace ECMS.API.Models
{
    public class AbstractApiResponse
    {
        public EmailDeliverability Email_Deliverability { get; set; }
    }

    public class EmailDeliverability
    {
        public string Status { get; set; }
    }
}
