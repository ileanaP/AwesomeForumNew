using AwesomeForum.Data.Base.AweForum.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomeForum.Models
{
    public class TopicMessage : IModelBase
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        public int TopicId { get; set; }
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditedDate { get; set; }
        public List<Message_Reaction> Message_Reactions { get; set; }
        public List<User_Reaction> User_Reactions { get; set; }
    }
}
