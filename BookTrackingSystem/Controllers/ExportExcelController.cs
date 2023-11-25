using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Table;
using BookTrackingSystem.Data;
using System.Text;
using BookTrackingSystem.Models.ConnectionString;
using System.Data.SqlClient;
using BookTrackingSystem.Models;
using System.Reflection;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace BookTrackingSystem.Controllers
{
    public class ExportExcelController : Controller
    {
        private ApplicationDbContext bookDbContext;

        public ExportExcelController(ApplicationDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }
        public IActionResult Index()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "select * From books";
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();
            IList<book> model = new List<book>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                model.Add(new book()
                {

                    bookName = dt.Rows[i]["bookName"].ToString(),
                    author = dt.Rows[i]["author"].ToString(),
                    issuer = dt.Rows[i]["issuer"].ToString(),
                });
            }

            return View(model);
        }

        public ActionResult ExportToExcel()
        {
            int i = 0;
            int j = 0;
            string sql = null;
            string data = null;
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlApp.Visible = false;
            xlWorkBook = (Excel.Workbook)(xlApp.Workbooks.Add(Missing.Value));
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.ActiveSheet;
            SqlConnection con = new SqlConnection(GetConString.ConString());
            con.Open();
            var cmd = new SqlCommand("SELECT TOP 0 * FROM books", con);
            var reader = cmd.ExecuteReader();
            int k = 0;
            for (i = 0; i < reader.FieldCount; i++)
            {
                data = (reader.GetName(i));
                xlWorkSheet.Cells[1, k + 1] = data;
                k++;
            }
            char lastColumn = (char)(65 + reader.FieldCount - 1);
            xlWorkSheet.get_Range("A1", lastColumn + "1").Font.Bold = true;
            xlWorkSheet.get_Range("A1", lastColumn + "1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            reader.Close();

            sql = "SELECT * FROM books";
            SqlDataAdapter dscmd = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                var newj = 0;
                for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                {
                    data = ds.Tables[0].Rows[i].ItemArray[j].ToString();

                    xlWorkSheet.Cells[i + 2, newj + 1] = data;
                    newj++;
                }
            }
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            return RedirectToAction("Index", "ExportExcel");
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }

}
