using System.ComponentModel.DataAnnotations;

namespace BookTrackingSystem.Models.viewModels
{
    public class EditBookRequest
    {
        public Guid bookID { get; set; }

        [Required(ErrorMessage = "Book Name is required.")]
        public string bookName { get; set; }

        [Required(ErrorMessage = "Reference No. is required.")]
        public string refNo { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string author { get; set; }

        public string? status { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Register Time is required.")]
        public DateTime registerTime { get; set; }

        [Required(ErrorMessage = "Issuer is required.")]
        public string issuer { get; set; }
    }
}
