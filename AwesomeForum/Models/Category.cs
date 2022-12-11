using AwesomeForum.Data.Base.AweForum.Data.Base;

namespace AwesomeForum.Models
{
    public class Category : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNr { get; set; }
        public List<Forum> Forums { get; set; }
        public List<Message_Reaction> Message_Reactions { get; set; }
    }
}
