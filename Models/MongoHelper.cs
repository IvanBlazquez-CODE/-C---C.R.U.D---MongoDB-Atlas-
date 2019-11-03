using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace CRUD_with_MongoDB.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb+srv://crud_user:12345678i@cluster0-ombyi.mongodb.net/test?retryWrites=true&w=majority";
        public static string MongoDatabase = "crud_mongodb";

        public static IMongoCollection<Models.Student> students_collection { get; set; }

        internal static void ConnectionToMongoService()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}