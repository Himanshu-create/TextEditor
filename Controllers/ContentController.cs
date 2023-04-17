using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;
using TextEditor.Models;

namespace TextEditor.Controllers
{
    public class ContentController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;
        public  ContentController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("texteditorDB"));
        }
        public List<ContentModel> getContentAll()
        {
            List<ContentModel> model = new List<ContentModel>();

            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM content";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ContentModel ctn = new ContentModel();
                ctn.id = (int)reader["contentID"];
                ctn.heading = (string)reader["heading"];
                ctn.content = (string)reader["context"];
                ctn.show = ctn.content.ToString().Split().Take(2).Aggregate((i, j) => i + " " + j);
                model.Add(ctn);
            }
            return model;
        }
        // GET: ContentController
        public ActionResult Index()
        {
            return View(getContentAll());
        }


        public ContentModel getCntID(int id)
        {
            ContentModel ctn = new();
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM content WHERE contentID = {id}";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //ContentModel ctn = new ContentModel();
                ctn.id = (int)reader["contentID"];
                ctn.heading = (string)reader["heading"];
                ctn.content = (string)reader["context"];

                //Console.WriteLine(ctn.show);
                //Console.WriteLine();
                //Console.WriteLine(ctn.content.ToString().Split().Take(2).Aggregate((i, j) => i + " " + j));

            }
            return ctn;
        }
        // GET: ContentController/Details/5
        public ActionResult Details(int id)
        {

            Console.WriteLine($"DETAILS {id}");
            return View(getCntID(id));
        }

        // GET: ContentController/Create
        public ActionResult Create()
        {
            return View();
        }

        void createContent(ContentModel ctn)
        {

            //ContentModel ctn = new();
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = $"INSERT into content values ('{ctn.heading}', '{ctn.content}')";
            var r = cmd.ExecuteNonQuery();

            _Connection.Close();

        }

        // POST: ContentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContentModel ctn)
        {
            try
            {
                createContent(ctn);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContentController/Edit/5

        
        public ActionResult Edit(int id)
        {
            return View(getCntID(id));
        }

        public void update(ContentModel ctn)
        {

            Console.WriteLine("UPDATE");
            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();

            cmd.CommandText = $"UPDATE content set heading = '{ctn.heading}', context = '{ctn.content}' where contentID = {ctn.id}";
            var r = cmd.ExecuteNonQuery();


        }
        // POST: ContentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContentModel ctn)
        {
            try
            {
                update(ctn);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: ContentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
