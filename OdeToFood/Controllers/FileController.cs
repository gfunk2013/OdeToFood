using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Upload()
        {
            ViewBag.RouteData = RouteData.Values;
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string baseData)
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("/Files"));
            string[] fileNames = filePaths.Select(filePath => Path.GetFileName(filePath)).ToArray();

            try
            {
                HttpFileCollectionBase files = HttpContext.Request.Files;
                Regex regex = new Regex(@"[\w.-]+");

                foreach (string key in files.AllKeys)
                {
                    bool match = Regex.IsMatch(key, @"[\w.-]+");
                    if (!match)
                    {
                        throw new ArgumentException("The file name '" + key + "' contains invalid characters");
                    }
                    if (fileNames.Contains(key))
                    {
                        throw new ArgumentException("The file name '" + key + "' is already being used in this directory.");
                    }
                    string fileSavePath = Path.Combine(Server.MapPath("/Files"), key);
                    files[key].SaveAs(fileSavePath);
                }

                string response = JsonConvert.SerializeObject(new { status = "success", data = "" });
                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(response);
                Response.End();
                Debug.WriteLine("success in c#");
            }
            catch (Exception ex)
            {
                string response = JsonConvert.SerializeObject(new { status = "fail", data = ex.Message });
                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(response);
                Response.End();
                Debug.WriteLine("fail in c#");
            }

            ViewBag.RouteData = RouteData.Values;
            return View();
        }

        public ActionResult Download()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("/Files"));
            string[] fileNames = filePaths.Select(filePath => Path.GetFileName(filePath)).ToArray();
            ViewBag.fileNames = fileNames;
            ViewBag.RouteData = RouteData.Values;
            return View();
        }

        public FileResult DownloadFile(string fileName)
        {
            string filepath = Path.Combine(Server.MapPath("/Files"), fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }
    }
}