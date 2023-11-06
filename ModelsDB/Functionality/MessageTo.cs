﻿namespace ModelsDB.Functionality
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
        public int AdminId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Dietician Dietician { get; set; }
        public int DieticianId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }

        public List<MessageDietetician> MessageDieticians { get; set; }
        public List<MessagePatient> MessagePatient { get; set; }
        public List<MessageAdmin> MessageAdmin { get; set; }

    }
}
