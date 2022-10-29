using Newtonsoft.Json;

namespace spbu_csharp_networks_practice.Vk.dto;

public class Response<T>
{
    [JsonProperty("response")] public T Value { get; set; }
}
