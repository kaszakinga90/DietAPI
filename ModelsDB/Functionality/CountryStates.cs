namespace ModelsDB.Functionality
{
    public class CountryStates
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
