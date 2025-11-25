using System.Text.Json.Serialization;

namespace ECMS.API.Models
{
    public class AbstractApiResponse
    {
        [JsonPropertyName("email_deliverability")]
        public EmailDeliverability? EmailDeliverability { get; set; }

        [JsonPropertyName("email_address")]
        public string? EmailAddress { get; set; }
    }

    public class EmailDeliverability
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
