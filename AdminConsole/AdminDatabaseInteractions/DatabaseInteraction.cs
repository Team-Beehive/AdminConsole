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
            DocumentReference docRef = db.Collection("Majors").Document(majorData.MajorName);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                {"Major_Name", majorData.MajorName },
                {"First_Offered", majorData.FirstOffered },
                {"Employers", majorData.Employers },
                {"Expected_Cred_Hours", majorData.ExpectedCredHours },
                {"Description", majorData.Description }
            };

            await docRef.SetAsync(user);

        }

        private static async Task RetrieveMajorData(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference userRef = db.Collection("Majors");
            QuerySnapshot snapshot = await userRef.GetSnapshotAsync();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Console.WriteLine("Major: {0}", documentDictionary["Major_Name"]);
                Console.WriteLine("First Offered: {0}", documentDictionary["First_Offered"]);

                List<object> temp = documentDictionary["Employers"] as List<object>;

                Console.WriteLine("Common Employers: {0}", EmployersToCSV(temp));
                Console.WriteLine("Expected Credit Hours: {0}", documentDictionary["Expected_Cred_Hours"]);
                Console.WriteLine("Description: {0}", documentDictionary["Description"]);

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
                temp.FirstOffered = int.Parse(documentDictionary["First_Offered"].ToString());
                temp.Employers = documentDictionary["Employers"] as List<string>;
                temp.ExpectedCredHours = int.Parse(documentDictionary["Expected_Cred_Hours"].ToString());
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
            CSET.FirstOffered = 1970;
            List<string> employers = new List<string>();
            employers.Add("Microsoft");
            employers.Add("Amazon");
            CSET.Employers = employers as List<string>;
            CSET.ExpectedCredHours = 500;
            CSET.Description = "Descp goes here";
            
            MajorData CET = new MajorData();
            CET.MajorName = "Computer Engineering Technology";
            CET.FirstOffered = 1960;
            List<string> CETemployers = new List<string>();
            CETemployers.Add("Microsoft");
            CETemployers.Add("Sony");
            CET.Employers = employers as List<string>;
            CET.ExpectedCredHours = 470;
            CET.Description = "Descp goes here for CET";
            
            MajorData CST = new MajorData();
            CST.MajorName = "Computer Science Technology";
            CST.FirstOffered = 1950;
            List<string> CSTemployers = new List<string>();
            CSTemployers.Add("Microsoft");
            CSTemployers.Add("Apply");
            CST.Employers = employers as List<string>;
            CST.ExpectedCredHours = 475;
            CST.Description = "Descp goes here for CST";


            

            //InitializeProject("x-circle-327618");
            //AddMajorData("x-circle-327618", CSET).Wait();
            //RetrieveMajorData("x-circle-327618").Wait();
        }
    }
}
