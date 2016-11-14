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
            string UserName = Session["UserName"].ToString();
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
                            ViewBag.Error = "Only CSV are allowed!";
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
                                string root = "~/App_Data/Delayed/CsvFile/UserName-" + UserName + "/";
                                var dirName = "~/App_Data/Delayed/CsvFile/UserName-" + UserName + "/Date-" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + "/";



                                if (!Directory.Exists(root))
                                {
                                    System.IO.Directory.CreateDirectory(Server.MapPath(root));
                                }
                                if (!Directory.Exists(dirName))
                                {
                                    System.IO.Directory.CreateDirectory(Server.MapPath(dirName));
                                }
                                var fileName = Path.GetFileNameWithoutExtension(fileAB.FileName);
                                var fileNameWithExt = Path.GetFileNameWithoutExtension(fileAB.FileName) + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv";
                                var path = Path.Combine(Server.MapPath(dirName), fileName + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv");
                                fileAB.SaveAs(path);
                                FileDetail fileDetail = new FileDetail()
                                {
                                    FileName = fileNameWithExt,
                                    Extension = Path.GetExtension(fileNameWithExt),
                                    Id = Guid.NewGuid()
                                };
                                fileDetails.Add(fileDetail);
                                DelayedUpload upload = new DelayedUpload();
                                upload.FileDetails = fileDetails;
                                db.DelayedUploads.Add(upload);
                                db.SaveChanges();
                                ViewBag.Message = "Sccessfully upload files on server.";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Please upload a files .";
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
                            ViewBag.Error = "Only CSV are allowed!";
                            return View();
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            if (ModelState.IsValid)
                            {
                                List<FileDetail> fileDetails = new List<FileDetail>();
                                List<CsvFile> listcsvfiles = new List<CsvFile>();



                                string root = "~/App_Data/Instant/CsvFile/UserName-" + UserName + "/";
                                var dirName = "~/App_Data/Instant/CsvFile/UserName-" + UserName + "/Date-" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + "/";



                                if (!Directory.Exists(root))
                                {
                                    System.IO.Directory.CreateDirectory(Server.MapPath(root));
                                }
                                if (!Directory.Exists(dirName))
                                {
                                    System.IO.Directory.CreateDirectory(Server.MapPath(dirName));
                                }
                                var fileName = Path.GetFileNameWithoutExtension(fileAB.FileName);
                                var fileNameWithExt = Path.GetFileNameWithoutExtension(fileAB.FileName) + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv";
                                FileDetail fileDetail = new FileDetail()
                                {
                                    FileName = fileNameWithExt,
                                    Extension = Path.GetExtension(fileNameWithExt),
                                    Id = Guid.NewGuid()
                                };
                                fileDetails.Add(fileDetail);
                                var path = Path.Combine(Server.MapPath(dirName), fileName + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".csv");
                                fileAB.SaveAs(path);
                                dt = ProcessCSV(path);
                                DelayedUpload upload1 = new DelayedUpload();
                                upload1.FileDetails = fileDetails;
                                db.DelayedUploads.Add(upload1);
                                ViewBag.Message = ProcessBulkCopy(dt);
                                CsvFile upload = new CsvFile();
                                listcsvfiles.Add(upload);
                                db.csvfiles.Add(upload);
                                db.SaveChanges();
                                ViewBag.Message = "Successfully Uploaded Sample files !!!";



                            }
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Please Select CSV Files.";
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
            dt.Columns.Add("Date");
            dt.Columns.Add("G10001");
            dt.Columns.Add("P10100");
            dt.Columns.Add("P10101");
            dt.Columns.Add("P11100");
            dt.Columns.Add("P10200");
            dt.Columns.Add("P10201");
            dt.Columns.Add("P10004");
            dt.Columns.Add("T10000");
            dt.Columns.Add("G10101");
            dt.Columns.Add("G103001");
            dt.Columns.Add("T101100");
            dt.Columns.Add("T10101");
            dt.Columns.Add("T10005");
            dt.Columns.Add("F101000");
            dt.Columns.Add("F102000");
            dt.Columns.Add("G115001");
            dt.Columns.Add("G115011");
            dt.Columns.Add("M12070");
            dt.Columns.Add("M12071");
            dt.Columns.Add("M12072");


            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                strArray = line.Split(',');
                row = dt.NewRow();
                //row["Id"] = strArray[0];
                row["Date"] = strArray[0];
                row["G10001"] = strArray[1];
                row["P10100"] = strArray[2];
                row["P10101"] = strArray[3];
                row["P11100"] = strArray[4];
                row["P10200"] = strArray[5];
                row["P10201"] = strArray[6];
                row["P10004"] = strArray[7];
                row["T10000"] = strArray[8];
                row["G10101"] = strArray[9];
                row["G103001"] = strArray[10];
                row["T101100"] = strArray[11];
                row["T10101"] = strArray[12];
                row["T10005"] = strArray[13];
                row["F101000"] = strArray[14];
                row["F102000"] = strArray[15];
                row["G115001"] = strArray[16];
                row["G115011"] = strArray[17];
                row["M12070"] = strArray[18];
                row["M12071"] = strArray[19];
                row["M12072"] = strArray[20];


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