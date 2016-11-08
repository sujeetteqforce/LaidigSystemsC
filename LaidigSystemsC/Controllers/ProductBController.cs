using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using LaidigSystemsC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;
using PagedList;
namespace LaidigSystemsC.Controllers
{
    public class ProductBController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: ProductB
        public ActionResult Index()
        {
            return RedirectToAction("LoggedIn", "Account");
        }



        [HttpPost]
        public ActionResult ImportB(HttpPostedFileBase files)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ModelState.IsValid)
                {

                    List<DataLogMonitouch> listcsvfiles = new List<DataLogMonitouch>();

                    for (int i = 0; i <= Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0 && file.ContentLength <= 52428800)
                        {
                            var fileName = Path.GetFileName(file.FileName);


                            var path = Path.Combine(Server.MapPath("~/App_Data/Monitouch/"), fileName);
                            file.SaveAs(path);
                            dt = ProcessCSV(path);
                            ViewBag.Message = ProcessBulkCopy(dt);

                            DataLogMonitouch upload = new DataLogMonitouch();
                            listcsvfiles.Add(upload);
                            db.datalogmonitouchs.Add(upload);
                            db.SaveChanges();
                        }
                        ViewBag.Message = "File Uploaded successfully";
                        return RedirectToAction("displayMonitouchFile");

                    }

                }
                //ViewBag.Success = "File Uploaded successfully.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error while uploading the files.";
                return View(ex);
            }




        }
        public ActionResult displayMonitouchFile(int page = 1, int pagesize = 10)
        {
            // ViewBag.Message = "File Uploaded successfully";
            List<DataLogMonitouch> csvFileList = db.datalogmonitouchs.ToList();
            PagedList<DataLogMonitouch> pageList = new PagedList<DataLogMonitouch>(csvFileList, page, pagesize);
            //return View(db.csvfiles.ToList().ToPagedList(pageNumber, pageSize));
            return View(pageList);
        }

        private static String ProcessBulkCopy(DataTable dt)
        {
            string Feedback = string.Empty;
            string connString = ConfigurationManager.ConnectionStrings["LaidigDbConStr"].ConnectionString;

            //make our connection and dispose at the end
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //make our command and dispose at the end
                using (var copy = new SqlBulkCopy(conn))
                {

                    //Open our connection
                    conn.Open();

                    ///Set target table and tell the number of rows
                    copy.DestinationTableName = "DataLogMonitouches";
                    copy.BatchSize = dt.Rows.Count;
                    try
                    {
                        //Send it to the server
                        copy.WriteToServer(dt);
                        Feedback = "Upload complete";
                    }
                    catch (Exception ex)
                    {
                        Feedback = ex.Message;
                    }
                }
            }

            return Feedback;
        }
        private static DataTable ProcessCSV(string fileName)
        {
            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            StreamReader sr = new StreamReader(fileName);

            //Read the first line and split the string at , with our regular expression in to an array
            //line = sr.ReadLine();
            //strArray = r.Split(line);


            //For each item in the new split array, dynamically builds our Data columns. Save us having to worry about it.
            // Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));
            dt.Columns.Add("Id");
            dt.Columns.Add("TimeDate");
            dt.Columns.Add("G12000");
            dt.Columns.Add("M12000");
            dt.Columns.Add("G11301");
            dt.Columns.Add("G11300");
            dt.Columns.Add("G11401");
            dt.Columns.Add("G11400");
            dt.Columns.Add("P11400");
            dt.Columns.Add("G10100");
            dt.Columns.Add("G10101");
            dt.Columns.Add("G10102");
            dt.Columns.Add("P10103");
            dt.Columns.Add("P10104");
            dt.Columns.Add("F10100");
            dt.Columns.Add("G11001");
            dt.Columns.Add("T11001");
            dt.Columns.Add("T11002");

            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                strArray = line.Split(',');
                row = dt.NewRow();
                //row["Id"] = strArray[0];
                row["TimeDate"] = strArray[0];
                row["G12000"] = strArray[1];
                row["M12000"] = strArray[2];
                row["G11301"] = strArray[3];
                row["G11300"] = strArray[4];
                row["G11401"] = strArray[5];
                row["G11400"] = strArray[6];
                row["P11400"] = strArray[7];
                row["G10100"] = strArray[8];
                row["G10101"] = strArray[9];
                row["G10102"] = strArray[10];
                row["P10103"] = strArray[11];
                row["P10104"] = strArray[12];
                row["F10100"] = strArray[13];
                row["G11001"] = strArray[14];
                row["T11001"] = strArray[15];
                row["T11002"] = strArray[16];


                //add our current value to our data row
                // row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }
            dt.Rows.RemoveAt(0);

            //Tidy Streameader up
            sr.Dispose();

            //return a the new DataTable
            return dt;

        }



    }
}
