namespace Application.DTOs.MessagesDTO
{
    public class MessageToDieteticianDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DieticianId { get; set; }
    }
}
