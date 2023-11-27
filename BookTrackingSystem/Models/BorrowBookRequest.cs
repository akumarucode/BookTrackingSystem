using System.ComponentModel.DataAnnotations;

namespace BookTrackingSystem.Models
{
    public class BorrowBookRequest
    {
        [Key]
        public Guid borrowID { get; set; }
        public string bookID { get; set; }

        public string bookName { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Library card is required.")]
        public string libraryCardNo { get; set; }

        [Required(ErrorMessage = "Borrow Date is required.")]
        public DateTime borrowDate { get; set; }

        [Required(ErrorMessage = "Return Date is required.")]
        public DateTime expectReturnDate { get; set; }

        public string? remark { get; set; }

    }
}
