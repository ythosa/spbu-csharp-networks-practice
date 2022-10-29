using System.Web;

namespace spbu_csharp_networks_practice.Vk;

public static class UriBuilderExtension
{
    public static UriBuilder AppendQueryParameters(this UriBuilder uriBuilder, params (string, string)[] parameters)
    {
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var p in parameters)
        {
            query[p.Item1] = p.Item2;
        }

        uriBuilder.Query = query.ToString();

        return uriBuilder;
    }
}
