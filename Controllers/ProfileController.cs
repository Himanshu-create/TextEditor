using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TextEditor.Models;

namespace TextEditor.Controllers
{
    public class ProfileController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;
        public ProfileController(IConfiguration configuration)
        {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("texteditorDB"));
        }
        public ProfileModel getProfile()
        {
            ProfileModel prof = new ProfileModel();

            _Connection.Open();
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Profile";
            SqlDataReader reader = cmd.ExecuteReader();


            //Console.WriteLine(id);
            while (reader.Read())
            {

                prof.id = (int)reader["personID"];
                prof.name = (string)reader["Pname"];
                prof.email = (string)reader["emailid"];
                prof.dob = (DateTime)reader["dob"];


            }
            return prof;
        }
        // GET: ProfileController
        public ActionResult Index()
        {
            return View(getProfile());
        }

        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
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
