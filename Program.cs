using spbu_csharp_networks_practice.Vk;

namespace spbu_csharp_networks_practice;

public static class Program
{
    private record Command(string Name, string Description, Func<VkClient, Task> Action);

    private static readonly Command[] Commands =
    {
        new("friends", "print friends online", PrintOnlineFriends),
        new("photo", "update profile photo", UpdateProfilePhoto)
    };

    public static async Task Main()
    {
        var accessToken = Authorization.GetAccessTokenByUser();
        if (accessToken is null or "")
        {
            Console.WriteLine("Error: empty access token");

            return;
        }

        var vkClient = new VkClient(accessToken, "5.131");

        foreach (var command in Commands)
        {
            Console.WriteLine($"Print <{command.Name}> if u want {command.Description}");
        }

        var commandName = Console.ReadLine();

        foreach (var command in Commands)
        {
            if (commandName != command.Name) continue;

            try
            {
                await command.Action(vkClient);
            }
            catch (VkException exception)
            {
                Console.WriteLine($"Failed to {command.Description}, error: {exception.Message}");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Internal error: {exception.Message}");
            }

            return;
        }

        Console.WriteLine("Invalid command");
    }

    private static async Task PrintOnlineFriends(VkClient vkClient)
    {
        var friendsOnlineIds = await vkClient.FriendsGetOnline();
        var friendsOnline = await vkClient.UsersGet(friendsOnlineIds);
        Console.WriteLine("Your friends online:");
        foreach (var friend in friendsOnline)
        {
            Console.WriteLine($"[{friend.Id}] {friend.FirstName} {friend.LastName}");
        }
    }

    private static async Task UpdateProfilePhoto(VkClient vkClient)
    {
        Console.Write("Input photo url: ");
        var photoUrl = Console.ReadLine();
        if (photoUrl is null or "")
        {
            Console.WriteLine("Error: empty photo url");

            return;
        }

        var uploadUrl = await vkClient.PhotosGetOwnerPhotoUploadServer();
        var uploadData = await vkClient.UploadPhoto(uploadUrl, photoUrl);
        var savedPhoto = await vkClient.PhotosSaveOwnerPhoto(uploadData.Server, uploadData.Hash, uploadData.Photo);

        Console.WriteLine($"Photo has saved! {savedPhoto.PhotoSrc}");
    }
}
