using AwesomeForum.Data.Base.AweForum.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomeForum.Models
{
    public class Message_Reaction : IModelBase
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        [ForeignKey(nameof(MessageId))]
        public TopicMessage TopicMessage { get; set; }
        public int ReactionId { get; set; }
        [ForeignKey(nameof(ReactionId))]
        public Reaction Reaction { get; set; }
    }
}
