using AwesomeForum.Data.Base.AweForum.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomeForum.Models
{
    public class Topic : IModelBase
    {
        public Topic()
        {
            MessageCount = 1;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public AppUser Creator { get; set; }
        public int ForumId { get; set; }
        [ForeignKey(nameof(ForumId))]
        public Forum Forum { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastPosted { get; set; }
        public int MessageCount { get; set; }
    }
}
