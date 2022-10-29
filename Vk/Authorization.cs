using System.Web;

namespace spbu_csharp_networks_practice.Vk;

public static class Authorization
{
    private const string ClientId = "51462268";
    private const string RedirectUri = "https://oauth.vk.com/blank.html";

    public static string? GetAccessTokenByUser()
    {
        var builder = new UriBuilder
        {
            Scheme = Uri.UriSchemeHttps,
            Port = -1,
            Host = "oauth.vk.com",
            Path = "authorize"
        };
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["redirect_uri"] = RedirectUri;
        query["client_id"] = ClientId;
        query["scope"] = "friends";
        query["response_type"] = "token";
        builder.Query = query.ToString();

        Console.WriteLine($"Follow the link and paste address where you were redirected: {builder}");
        var address = Console.ReadLine();
        if (address == null)
            return null;

        address = address.Replace("#", "?");

        return HttpUtility.ParseQueryString(new Uri(address).Query).Get("access_token");
    }
}
