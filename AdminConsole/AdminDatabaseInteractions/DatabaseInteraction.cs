using System;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDatabaseInteraction
{

    public class DatabaseInteraction
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

        private static async Task AddMajorData(string project, MajorData majorData)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRef = db.Collection("Majors").Document(majorData.MajorCategory).Collection("MajorsList").Document(majorData.MajorName);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                {"Major_Name", majorData.MajorName },
                {"Major_Category", majorData.MajorCategory },
                {"Professors", majorData.Professors },
                {"Classes", majorData.Classes },
                {"Description", majorData.Description }
            };

            await docRef.SetAsync(user);

        }

        private static async Task RetrieveMajorData(string project, string[] categories)
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
        }

        private static async Task<MajorList> StoreMajorData(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference userRef = db.Collection("Majors");
            QuerySnapshot snapshot = await userRef.GetSnapshotAsync();
            MajorList majorList = new MajorList();
            foreach(DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                MajorData temp = new MajorData();
                temp.MajorName = documentDictionary["Major_Name"].ToString();
                temp.MajorCategory = documentDictionary["Major_Category"].ToString();
                temp.Professors = documentDictionary["Employers"] as List<string>;
                temp.Classes = documentDictionary["Classes"] as List<string>;
                temp.Description = documentDictionary["Description"].ToString();
                majorList.AddMajor(temp);
            }
            return majorList;
        }

        static string EmployersToCSV(List<object> list)
        {
            string employers = null;
            foreach (object obj in list)
            {
                if(employers == null)
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

        static void Main()
        {
            MajorList majorList = new MajorList();
            MajorData CSET = new MajorData();
            CSET.MajorName = "Software Engineering Technology";
            CSET.MajorCategory = "CSET";
            List<string> employers = new List<string>();
            employers.Add("Todd");
            employers.Add("Tory");

            List<string> classes = new List<string>();
            classes.Add("GUI");
            classes.Add("Object Oreinted Programming");

            CSET.Professors = employers as List<string>;
            CSET.Classes = classes;
            CSET.Description = "Descp goes here";

            MajorData AppMath = new MajorData();
            AppMath.MajorName = "Applied Mathmatics";
            AppMath.MajorCategory = "Mathematics";
            List<string> Mathemployers = new List<string>();
            Mathemployers.Add("Tiernan");
            Mathemployers.Add("Kenneth");

            List<string> Mathclasses = new List<string>();
            Mathclasses.Add("Discrete Mathmatics");
            Mathclasses.Add("Liner Algebra");

            AppMath.Professors = Mathemployers as List<string>;
            AppMath.Classes = Mathclasses;
            AppMath.Description = "Math Descp goes here";
            majorList.AddMajor(AppMath);
            majorList.AddMajor(CSET);


            InitializeProject("x-circle-327618");
            AddMajorData("x-circle-327618", CSET).Wait();
            AddMajorData("x-circle-327618", AppMath).Wait();
            RetrieveMajorData("x-circle-327618", majorList.GetAllCategories()).Wait();
        }
    }
}
