using LaidigSystemsC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Text.RegularExpressions;
using PagedList;


  
namespace LaidigSystemsC.Controllers
{
    public class SiemensController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: Siemens
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> fileNames, string rbGrp)
        {
            string name = rbGrp.ToString();

            if (name == "Delayed")
            {
                foreach (HttpPostedFileBase fileAB in fileNames)
                {
                    if (fileAB != null && fileAB.ContentLength > 0)
                    {
                        String FileExtn = System.IO.Path.GetExtension(fileAB.FileName);
                        if (!(FileExtn == ".csv" || FileExtn == ".CSV"))
                        {
                            ViewBag.Message = "Only CSV are allowed!";
                            return View();
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            if (ModelState.IsValid)
                            {
                                List<FileDetail> fileDetails = new List<FileDetail>();
                                List<CsvFile> listcsvfiles = new List<CsvFile>();

                                List<CsvFile> Csvfiles = new List<CsvFile>();
                                //for (int i = 0; i < Request.Files.Count; i++)
                                //{

                                    var fileName = Path.GetFileName(fileAB.FileName);

                                    var path = Path.Combine(Server.MapPath("~/App_Data/Delayed/CsvFile/"), fileName);
                                    fileAB.SaveAs(path);

                                //}

                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please Select file within 20 MB.";
                        return View();
                    }
                }

            }
            else
            {
                foreach (HttpPostedFileBase fileAB in fileNames)
                {
                    if (fileAB != null && fileAB.ContentLength > 0)
                    {
                        String FileExtn = System.IO.Path.GetExtension(fileAB.FileName);
                        if (!(FileExtn == ".csv" || FileExtn == ".CSV"))
                        {
                            ViewBag.Message = "Only CSV are allowed!";
                            return View();
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            if (ModelState.IsValid)
                            {
                                List<FileDetail> fileDetails = new List<FileDetail>();
                                List<CsvFile> listcsvfiles = new List<CsvFile>();

                                List<CsvFile> Csvfiles = new List<CsvFile>();
                                //for (int i = 0; i < Request.Files.Count; i++)
                                //{

                                    var fileName = Path.GetFileName(fileAB.FileName);

                                    var path = Path.Combine(Server.MapPath("~/App_Data/Instant/Siemens/"), fileName);
                                    fileAB.SaveAs(path);
                                    dt = ProcessCSV(path);
                                    ViewBag.Message = ProcessBulkCopy(dt);
                                    CsvFile upload = new CsvFile();
                                    listcsvfiles.Add(upload);
                                    db.csvfiles.Add(upload);
                                    db.SaveChanges();
                                    ViewBag.Message = "Successfully Uploaded Sample files !!!";
                                //}

                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please Select file within 20 MB.";
                        return View();
                    }
                }

            }
            return View();
            //return RedirectToAction("csvFileUpload");
        }

        [AllowAnonymous]
        public ViewResult csvFileUpload(int page = 1, int pagesize = 10)
        {
            List<CsvFile> csvFileList = db.csvfiles.ToList();
            PagedList<CsvFile> pageList = new PagedList<CsvFile>(csvFileList, page, pagesize);
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
                    copy.DestinationTableName = "CsvFiles";
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
            dt.Columns.Add("DateofTime");
            dt.Columns.Add("CurSysRateCFM");
            dt.Columns.Add("ReqRecRateCFM");
            dt.Columns.Add("RecSpeedRPM");
            dt.Columns.Add("RecTorque");
            dt.Columns.Add("RecPressurePSI");
            dt.Columns.Add("RecFlowGPM");
            dt.Columns.Add("RecPowerHP");
            dt.Columns.Add("AdvSpeedDPH");
            dt.Columns.Add("AdvTorque");
            dt.Columns.Add("AdvPressurePSI");
            dt.Columns.Add("Disc1Torq");
            dt.Columns.Add("Disc1SpeedRPM");
            dt.Columns.Add("Disc1PowerHP");
            dt.Columns.Add("Disc2Torq");
            dt.Columns.Add("Disc2SpeedRPM");
            dt.Columns.Add("Disc2PowerHP");


            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                strArray = line.Split(',');
                row = dt.NewRow();
                //row["Id"] = strArray[0];
                row["DateofTime"] = strArray[0];
                row["CurSysRateCFM"] = strArray[1];
                row["ReqRecRateCFM"] = strArray[2];
                row["RecSpeedRPM"] = strArray[3];
                row["RecTorque"] = strArray[4];
                row["RecPressurePSI"] = strArray[5];
                row["RecFlowGPM"] = strArray[6];
                row["RecPowerHP"] = strArray[7];
                row["AdvSpeedDPH"] = strArray[8];
                row["AdvTorque"] = strArray[9];
                row["AdvPressurePSI"] = strArray[10];
                row["Disc1Torq"] = strArray[11];
                row["Disc1SpeedRPM"] = strArray[12];
                row["Disc1PowerHP"] = strArray[13];
                row["Disc2Torq"] = strArray[14];
                row["Disc2SpeedRPM"] = strArray[15];
                row["Disc2PowerHP"] = strArray[16];


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