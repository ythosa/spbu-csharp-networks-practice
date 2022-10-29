namespace spbu_csharp_networks_practice.Vk;

public class VkException : Exception
{
    public VkException(string method, string errorResponse)
        : base($"failed to execute {method}, response: {errorResponse}")
    {
    }
}
