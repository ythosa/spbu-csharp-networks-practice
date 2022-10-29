using Newtonsoft.Json;

namespace spbu_csharp_networks_practice.Vk.dto;

public class PhotosGetOwnerPhotoUploadServerResponse
{
    [JsonProperty("upload_url")] public string UploadUrl { get; set; }
}
