using Newtonsoft.Json;

namespace spbu_csharp_networks_practice.Vk.dto;

public class UploadPhotoResponse
{
    [JsonProperty("server")] public int Server { get; set; }
    [JsonProperty("photo")] public string Photo { get; set; }
    [JsonProperty("mid")] public int Mid { get; set; }
    [JsonProperty("hash")] public string Hash { get; set; }
    [JsonProperty("message_code")] public int MessageCode { get; set; }
    [JsonProperty("profile_aid")] public int ProfileAid { get; set; }
}
