using ModelsDB.Functionality;

namespace ModelsDB
{
    public class Admin : User
    {
        public bool isSuperAdmin { get; set; } = false;
        public List<MessageTo> MessageTo { get; set; }
    }
}
