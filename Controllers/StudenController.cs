using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_with_MongoDB.Controllers
{
    public class StudenController : Controller
    {
        // GET: Studen
        public ActionResult Index()
        {
            Models.MongoHelper.ConnectionToMongoService();
            Models.MongoHelper.students_collection =
                Models.MongoHelper.database.GetCollection<Models.Student>("students");

            var filter = Builders<Models.Student>.Filter.Ne("_id","");
            var result = Models.MongoHelper.students_collection.Find(filter).ToList();

            return View(result);
        }

        // GET: Studen/Details/5
        public ActionResult Details(string id)
        {
            Models.MongoHelper.ConnectionToMongoService();
            Models.MongoHelper.students_collection =
                Models.MongoHelper.database.GetCollection<Models.Student>("students");

            var filter = Builders<Models.Student>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.students_collection.Find(filter).FirstOrDefault();

            return View(result);
        }

        // GET: Studen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Studen/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectionToMongoService();
                Models.MongoHelper.students_collection =
                    Models.MongoHelper.database.GetCollection<Models.Student>("students");

                //Create some _id
                Object id = GenerateRandomId(24);

                Models.MongoHelper.students_collection.InsertOneAsync(new Models.Student
                {
                    _id = id,
                    firstName = collection["firstName"],
                    lastName = collection["lastName"],
                    emailAddress = collection["emailAddress"]
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static Random random = new Random();
        private object GenerateRandomId(int v)
        {
            string strarray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(strarray, v).Select(s=>s[random.Next(s.Length)]).ToArray());
        }

        // GET: Studen/Edit/5
        public ActionResult Edit(string id)
        {
            Models.MongoHelper.ConnectionToMongoService();
            Models.MongoHelper.students_collection =
                Models.MongoHelper.database.GetCollection<Models.Student>("students");

            var filter = Builders<Models.Student>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.students_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Studen/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectionToMongoService();
                Models.MongoHelper.students_collection =
                    Models.MongoHelper.database.GetCollection<Models.Student>("students");

                var filter = Builders<Models.Student>.Filter.Eq("_id", id);
                var update = Builders<Models.Student>.Update
                    .Set("firstName", collection["firstName"])
                    .Set("lastName", collection["lastName"])
                    .Set("emailAddress", collection["emailAddress"]);
                var result = Models.MongoHelper.students_collection.UpdateOneAsync(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Studen/Delete/5
        public ActionResult Delete(string id)
        {
            Models.MongoHelper.ConnectionToMongoService();
            Models.MongoHelper.students_collection =
                Models.MongoHelper.database.GetCollection<Models.Student>("students");

            var filter = Builders<Models.Student>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.students_collection.Find(filter).FirstOrDefault();

            return View(result);
        }

        // POST: Studen/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectionToMongoService();
                Models.MongoHelper.students_collection =
                    Models.MongoHelper.database.GetCollection<Models.Student>("students");

                var filter = Builders<Models.Student>.Filter.Eq("_id", id);
                var result = Models.MongoHelper.students_collection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Studen/InsertMany/5
        public ActionResult InsertMany(string id)
        {
            //Insert Many to mongo database

            List<Models.Student> students = new List<Models.Student>();
            int contador = 0;
            for (int i = 0; i < 1000; i++)
            {
                Models.Student student = new Models.Student();
                student._id = GenerateRandomId(24);
                student.firstName = "Student|" + contador.ToString() + "|";
                student.lastName = "Student|" + contador.ToString() + "|";
                student.emailAddress = "Student|" + contador.ToString() + "|@gmail.com";
                students.Add(student);
                contador++;
            }

            Models.MongoHelper.ConnectionToMongoService();
            Models.MongoHelper.students_collection =
                Models.MongoHelper.database.GetCollection<Models.Student>("students");

            var result = Models.MongoHelper.students_collection.InsertManyAsync(students);
            return View(result);
        }

        // POST: Studen/InsertMany/5
        [HttpPost]
        public ActionResult InsertMany(string id, FormCollection collection)
        {
            try
            {
                List<Models.Student> students = new List<Models.Student>();
                int contador = 0;
                for (int i = 0; i < 2500; i++)
                {
                    Models.Student student = new Models.Student();
                    student._id = GenerateRandomId(24);
                    student.firstName = "Student|" + contador.ToString() + "|";
                    student.lastName = "Student|" + contador.ToString() + "|";
                    student.emailAddress = "Student|" + contador.ToString() + "|@gmail.com";
                    students.Add(student);
                    contador++;
                }

                Models.MongoHelper.ConnectionToMongoService();
                Models.MongoHelper.students_collection =
                    Models.MongoHelper.database.GetCollection<Models.Student>("students");

                var result = Models.MongoHelper.students_collection.InsertManyAsync(students);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
