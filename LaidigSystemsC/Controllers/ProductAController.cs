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
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using PagedList;

namespace LaidigSystemsC.Controllers
{
    public class ProductAController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: ProductA
        public ActionResult Index()
        {
            //return View(db.datalogabs.ToList());
            return RedirectToAction("LoggedIn", "Account");
        }





        [HttpPost]
        public ActionResult ImportA(HttpPostedFileBase files)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ModelState.IsValid)
                {
                    // List<FileDetail> fileDetails = new List<FileDetail>();
                    List<DataLogAB> listcsvfiles = new List<DataLogAB>();

                    for (int i = 0; i <= Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0 && file.ContentLength <= 52428800)
                        {
                            var fileName = Path.GetFileName(file.FileName);


                            var path = Path.Combine(Server.MapPath("~/App_Data/DatalogAB/"), fileName);
                            file.SaveAs(path);
                            dt = ProcessCSV(path);
                            ViewBag.Message = ProcessBulkCopy(dt);
                            DataLogAB upload = new DataLogAB();
                            listcsvfiles.Add(upload);
                            db.datalogabs.Add(upload);
                            db.SaveChanges();
                        }
                        ViewBag.Message = "File Uploaded successfully.";
                        return RedirectToAction("displayABFile");

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

        [AllowAnonymous]
        public ViewResult displayABFile(int page = 1, int pagesize = 10)
        {
            List<DataLogAB> DataLogABFileList = db.datalogabs.ToList();
            PagedList<DataLogAB> pageList = new PagedList<DataLogAB>(DataLogABFileList, page, pagesize);
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
                    copy.DestinationTableName = "DataLogABs";
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
            dt.Columns.Add("LocalDate");
            dt.Columns.Add("LocalTime");
            dt.Columns.Add("LMIG12000");
            dt.Columns.Add("LMIM12000");
            dt.Columns.Add("LMIG11301");
            dt.Columns.Add("LMIG11300");
            dt.Columns.Add("LMIG11401");
            dt.Columns.Add("LMIG11400");
            dt.Columns.Add("LMIP11400");
            dt.Columns.Add("LMIG10100");
            dt.Columns.Add("LMIG10101");
            dt.Columns.Add("LMIG10102");
            dt.Columns.Add("LMIP10103");
            dt.Columns.Add("LMIP10104");
            dt.Columns.Add("LMIF10100");
            dt.Columns.Add("LMIG11001");
            dt.Columns.Add("LMIT11001");
            dt.Columns.Add("LMIT11002");
            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                strArray = line.Split(',');
                row = dt.NewRow();
                //row["Id"] = strArray[0];
                row["LocalDate"] = strArray[0];
                row["LocalTime"] = strArray[1];
                row["LMIG12000"] = strArray[2];
                row["LMIM12000"] = strArray[3];
                row["LMIG11301"] = strArray[4];
                row["LMIG11300"] = strArray[5];
                row["LMIG11401"] = strArray[6];
                row["LMIG11400"] = strArray[7];
                row["LMIP11400"] = strArray[8];
                row["LMIG10100"] = strArray[9];
                row["LMIG10101"] = strArray[10];
                row["LMIG10102"] = strArray[11];
                row["LMIP10103"] = strArray[12];
                row["LMIP10104"] = strArray[13];
                row["LMIF10100"] = strArray[14];
                row["LMIG11001"] = strArray[15];
                row["LMIT11001"] = strArray[16];
                row["LMIT11002"] = strArray[17];

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




        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataLogAB datalogab = await db.datalogabs.FindAsync(id);
            if (datalogab == null)
            {
                return HttpNotFound();
            }
            return View(datalogab);
        }

    }
}