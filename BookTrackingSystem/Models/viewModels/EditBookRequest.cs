namespace BookTrackingSystem.Models.viewModels
{
    public class EditBookRequest
    {
        public Guid bookID { get; set; }
        public string bookName { get; set; }

        public string author { get; set; }

        public DateTime registerTime { get; set; }

        public string issuer { get; set; }
    }
}
