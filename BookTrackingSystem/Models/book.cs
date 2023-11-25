using BookTrackingSystem.Data.Migrations;
using BookTrackingSystem.Models.ConnectionString;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Globalization;

namespace BookTrackingSystem.Models
{
    public class book
    {
        
        public Guid bookID { get; set; }

        [Required(ErrorMessage = "Book Name is required.")]
        public string bookName { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string author { get; set; }

        public string status { get; set; }


        public DateTime registerTime { get; set; }

        [Required(ErrorMessage = "Issuer is required.")]
        public string issuer { get; set; }

        //public string status { get; set; }

        internal static object Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            con.Open();

            string dateString = registerTime.ToString();
            string format = "d/M/yyyy h:mm:ss tt";

            //Parse datetime to make sure the format receive is correct
            DateTime parsedDateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);


            // Convert to DateTime2 with 7 decimal places precision
            string dateTime2 = parsedDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            string query = "INSERT INTO books(bookID , bookName, author, registerTime,issuer) values ('" + bookID + "','" + bookName + "','" + author + "','" + dateTime2 + "','" + issuer + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }



    }
}
