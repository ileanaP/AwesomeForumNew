using AwesomeForum.Data.Base.AweForum.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomeForum.Models
{
    public class Forum : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TopicCount { get; set; }
        public List<Topic> Topics { get; set; }
        public int OrderNr { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
