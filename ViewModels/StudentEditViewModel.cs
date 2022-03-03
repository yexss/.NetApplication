namespace DemoApplication.ViewModels
{
    public class StudentEditViewModel:StudentCreateViewModel
    {
        public int Id { get; set; }

        public string? ExistingPhoto { get; set; }
    }
}
