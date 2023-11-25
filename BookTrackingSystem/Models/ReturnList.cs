using System.ComponentModel.DataAnnotations;

namespace BookTrackingSystem.Models
{
    public class ReturnList
    {
        [Key]
        public Guid returnID { get; set; }
        public Guid borrowID { get; set; }
        public string bookID { get; set; }

        [Required(ErrorMessage = "Book Name is required.")]
        public string bookName { get; set; }

        public string fullName { get; set; }

        public string libraryCardNo { get; set; }

        public DateTime borrowDate { get; set; }
        public DateTime expectReturnDate { get; set; }

        public string remark { get; set; }

    }
}
