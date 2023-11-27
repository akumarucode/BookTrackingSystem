using System.ComponentModel.DataAnnotations;

namespace BookTrackingSystem.Models
{
    public class ReturnHistory
    {
        [Key]
        public Guid returnTransID { get; set; }
        public string borrowID { get; set; }
        public string borrowerName { get; set; }
        public string libraryCardNo { get; set; }
        public string bookName { get; set; }
        public DateTime borrowDate { get; set; }
        public DateTime actualReturnDate { get; set; }
    }
}
