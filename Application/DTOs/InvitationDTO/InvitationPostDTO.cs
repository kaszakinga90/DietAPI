namespace Application.DTOs.InvitationDTO
{
    public class InvitationPostDTO
    {
        public int PatientId { get; set; }
        public int DieticianId { get; set; }
        public bool IsSended { get; set; }
    }
}
