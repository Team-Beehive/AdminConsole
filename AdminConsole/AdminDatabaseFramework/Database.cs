using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class Database
    {
        public Majors Majors { get; set; }
        public Buildings Buildings { get; set; }
        public Professors Professors { get; set; }
        public FirestoreDb db;

        public Database(string Project, string envPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", envPath);
            db = FirestoreDb.Create(Project);
            Majors = new Majors(db);
            Buildings = new Buildings(db);
            Professors = new Professors(db);
        }

        public void updateProject(string project)
        {
            db= FirestoreDb.Create(project);
        }

        public void updateCreds(string envPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", envPath);
        }
    }
}
