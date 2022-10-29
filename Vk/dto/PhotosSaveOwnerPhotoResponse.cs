using Newtonsoft.Json;

namespace spbu_csharp_networks_practice.Vk.dto;

public class PhotosSaveOwnerPhotoResponse
{
    [JsonProperty("photo_hash")] public string PhotoHash { get; set; }
    [JsonProperty("photo_src")] public string PhotoSrc { get; set; }
    [JsonProperty("photo_src_big")] public string PhotoSrcBig { get; set; }
    [JsonProperty("photo_src_small")] public string PhotoSrcSmall { get; set; }
    [JsonProperty("saved")] public string Saved { get; set; }
    [JsonProperty("post_id")] public string PostId { get; set; }
}
