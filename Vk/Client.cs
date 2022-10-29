using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using spbu_csharp_networks_practice.Vk.dto;

namespace spbu_csharp_networks_practice.Vk;

public class VkClient
{
    private readonly string _accessToken;
    private readonly string _version;

    private readonly HttpClient _httpClient = new();

    public VkClient(string accessToken, string version)
    {
        _accessToken = accessToken;
        _version = version;
    }

    public async Task<User[]> UsersGet(params string[] userIds)
    {
        var uriBuilder = GetUriBuilder("users.get").AppendQueryParameters(
            ("user_ids", string.Join(',', userIds)),
            ("fields", "sex")
        );

        var response = await _httpClient.GetAsync(uriBuilder.ToString());

        return (await ParseResponse<Response<User[]>>("UsersGet", response)).Value;
    }

    public async Task<string[]> FriendsGetOnline()
    {
        var uriBuilder = GetUriBuilder("friends.getOnline").AppendQueryParameters(
            ("order", "random")
        );

        var response = await _httpClient.GetAsync(uriBuilder.ToString());

        return (await ParseResponse<Response<string[]>>("FriendsGetOnline", response)).Value;
    }

    public async Task<string> PhotosGetOwnerPhotoUploadServer()
    {
        var uriBuilder = GetUriBuilder("photos.getOwnerPhotoUploadServer");

        var response = await _httpClient.GetAsync(uriBuilder.ToString());

        return (await ParseResponse<Response<PhotosGetOwnerPhotoUploadServerResponse>>(
            "PhotosGetOwnerPhotoUploadServer",
            response
        )).Value.UploadUrl;
    }

    public async Task<UploadPhotoResponse> UploadPhoto(string uploadUrl, string photoUrl)
    {
        var photoResponse = await _httpClient.GetAsync(photoUrl);
        var photoStream = await photoResponse.Content.ReadAsStreamAsync();

        var formContent = new MultipartFormDataContent();
        formContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
        formContent.Add(new StreamContent(photoStream), "profile", "profile.jpg");

        var response = await _httpClient.PostAsync(uploadUrl, formContent);

        return await ParseResponse<UploadPhotoResponse>("UploadPhoto", response);
    }

    public async Task<PhotosSaveOwnerPhotoResponse> PhotosSaveOwnerPhoto(int server, string hash, string photo)
    {
        var uriBuilder = GetUriBuilder("photos.saveOwnerPhoto").AppendQueryParameters(
            ("server", server.ToString()),
            ("hash", hash),
            ("photo", photo)
        );

        var response = await _httpClient.GetAsync(uriBuilder.ToString());

        return await ParseResponse<PhotosSaveOwnerPhotoResponse>("PhotosSaveOwnerPhoto", response);
    }

    private UriBuilder GetUriBuilder(string method)
    {
        var builder = new UriBuilder
        {
            Scheme = Uri.UriSchemeHttps,
            Port = -1,
            Host = "api.vk.com",
            Path = $"method/{method}"
        };

        var query = HttpUtility.ParseQueryString(builder.Query);
        query["access_token"] = _accessToken;
        query["v"] = _version;
        builder.Query = query.ToString();

        return builder;
    }

    private static async Task<T> ParseResponse<T>(string method, HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        if (responseContent.Contains("error"))
        {
            throw new VkException(method, responseContent);
        }

        var result = JsonConvert.DeserializeObject<T>(responseContent);
        if (result is null)
        {
            throw new VkException(method, "failed to parse response");
        }

        return result;
    }
}
