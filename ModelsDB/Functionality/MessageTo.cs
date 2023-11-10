namespace ModelsDB.Functionality
{
    /// <summary>
    /// Klasa odpowiedzialna za wysyłanie wiadomości prywatnych oraz ogólnych pomiędzy różnymi użytkownikami
    /// np. admin-pacjent, dietetyk-pacjent, admin-all, itd.
    /// </summary>
    public class MessageTo : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Admin Admin { get; set; }
        public int? AdminId { get; set; } = null;
        public Patient Patient { get; set; }
        public int? PatientId { get; set; } = null;
        public Dietician Dietician { get; set; }
        public int? DieticianId { get; set; } = null;
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }

    }
}
