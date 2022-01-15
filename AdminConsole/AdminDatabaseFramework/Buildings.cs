using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class Buildings
    {
        public LinkedList<BuildingData> BuildingList { get; set; }
        private FirestoreDb firestoreDb { get; set; }
        public string project = "oit-kiosk";
        public Buildings()
        {
            firestoreDb = FirestoreDb.Create(project);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\nulty\\Documents\\JrProject\\Database\\oit-kiosk-firebase-adminsdk-u24sq-8f7958c50f.json");
        }
    }
}
