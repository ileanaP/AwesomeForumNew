using Microsoft.AspNetCore.Identity;

namespace AwesomeForum.Models
{
    public class AppUser : IdentityUser
    {
        public int NrOfMessages { get; set; }
        public int NrOfTopics { get; set; }
        public List<User_Reaction> User_Reactions { get; set; }
    }
}
