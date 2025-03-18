namespace compito_17_03.ViewModels
{
    public class EditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cognome { get; set; }
        public DateOnly DataNascita { get; set; }
        public string Email { get; set; }
    }
}
