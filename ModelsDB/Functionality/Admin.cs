using ModelsDB.Functionality;

namespace ModelsDB
{
    public class Admin : User
    {
        public List<MessageTo> MessageTo { get; set; }
        public List<MessageAdmin> MessageAdmins { get; set; }
    }
}
