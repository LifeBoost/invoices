using System.Text.Json.Serialization;

namespace Infrastructure.Domain.Users;

public class User
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}