using System;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDatabaseInteractions
{

    public class MajorDatabase
    {
        /*--------------
        Class: DatabaseInteraction
        Purpose: Used to interact with the NoSQL Firestore DB
        Date Created: 11/24/2021
        Author: Tucker Nulty
        
        Funtions:
          -
          -
          -
        */


        private static void InitializeProject(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
        }

        private static async Task db_EditMajorData(string project, MajorData major)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRef = db.Collection("pages").Document("Majors").Collection("Degrees").Document(major.MajorName);
            Dictionary<string, object> majorDict = new Dictionary<string, object>
            {
                {"about", major.about },
                {"campuses", major.campuses },
                {"type", major.type }
            };
            await docRef.SetAsync(majorDict);
        }

        /*private static async Task RetrieveMajorData(string project, string[] categories)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            foreach (string category in categories)
            {
                CollectionReference userRef = db.Collection("Majors").Document(category).Collection("MajorsList");
                QuerySnapshot snapshot = await userRef.GetSnapshotAsync();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    Dictionary<string, object> documentDictionary = document.ToDictionary();
                    Console.WriteLine("Major: {0}", documentDictionary["Major_Name"]);
                    Console.WriteLine("Major_Category: {0}", documentDictionary["Major_Category"]);


                    Console.WriteLine("Professors: {0}", EmployersToCSV(documentDictionary["Professors"] as List<object>));
                    Console.WriteLine("Classes: {0}", EmployersToCSV(documentDictionary["Classes"] as List<object>));
                    Console.WriteLine("Description: {0}", documentDictionary["Description"]);

                }
            }
        } */

        public void ShToMD(DocumentSnapshot documentSnapshot)
        {
            Dictionary<string, object> documentDictionary = documentSnapshot.ToDictionary();
            Console.WriteLine("Major: {0}", documentSnapshot.Id);
            Console.WriteLine("Major_Category: {0}", EmployersToCSV(documentDictionary["type"] as List<object>));


            Console.WriteLine("Campus: {0}", EmployersToCSV(documentDictionary["campuses"] as List<object>));
            //Console.WriteLine("Classes: {0}", EmployersToCSV(documentDictionary["Classes"] as List<object>));
            Console.WriteLine("about: {0}", EmployersToCSV(documentDictionary["about"] as List<object>));
        }

        private static async Task<LinkedList<DocumentSnapshot>> db_StoreMajorData(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference userRef = db.Collection("pages").Document("Majors").Collection("Degrees");
            QuerySnapshot snapshot = await userRef.GetSnapshotAsync();
            LinkedList<DocumentSnapshot> tempDocs = new LinkedList<DocumentSnapshot>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                tempDocs.AddLast(document);
            }
            return await Task.FromResult(tempDocs);
        }

        public LinkedList<DocumentSnapshot> GetMajorData(string project)
        {
            db_StoreMajorData(project).Wait();
            LinkedList<DocumentSnapshot> documentSnapshots = db_StoreMajorData(project).Result;
            return documentSnapshots;
        }

        public void EditMajorData(string project, MajorData major)
        {
            db_EditMajorData(project, major).Wait();
        }

        static string EmployersToCSV(List<object> list)
        {
            string employers = null;
            foreach (object obj in list)
            {
                if (employers == null)
                {
                    employers = obj.ToString();
                }
                else
                {
                    employers += ", " + obj.ToString();
                }
            }
            return employers;
        }

    }
}

