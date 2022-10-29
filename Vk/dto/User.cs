using Newtonsoft.Json;

namespace spbu_csharp_networks_practice.Vk.dto;

public class User
{
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("is_closed")] public bool IsClosed { get; set; }
    [JsonProperty("can_access_closed")] public bool CanAccessClosed { get; set; }
    [JsonProperty("first_name")] public string FirstName { get; set; }
    [JsonProperty("last_name")] public string LastName { get; set; }
    [JsonProperty("sex")] public int Sex { get; set; }
}
