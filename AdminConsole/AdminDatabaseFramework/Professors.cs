using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class ProfessorData
    {
        public string professorName { get; set; }
        public string professorDepartment { get; set; }
        public string professorOffice { get; set; }
        public string professorEmail { get; set; }
        public string professorPhoneNumber { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "department", professorDepartment },
                { "email", professorEmail },
                {"office", professorOffice },
                {"phone_number", professorPhoneNumber }
            };
        }
    }
    public class Professors
    {
        private FirestoreDb db;
        public string project = "oit-kiosk";

        public Professors()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "F:\\CSTCode\\JP\\Database\\oit-kiosk-firebase-adminsdk-u24sq-8f7958c50f.json")
            db = FirestoreDb.Create(project);
        }
    }
}
