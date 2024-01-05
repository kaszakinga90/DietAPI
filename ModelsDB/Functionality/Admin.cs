using ModelsDB.Functionality;

namespace ModelsDB
{
    public class Admin : User
    {
       
        public List<MessageTo> MessageTo { get; set; }
    }
}
