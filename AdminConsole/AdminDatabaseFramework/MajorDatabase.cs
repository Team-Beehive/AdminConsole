using System;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AdminDatabaseFramework
{

    public class MajorDatabase
    {
        /*--------------
        Class: DatabaseInteraction
        Purpose: Used to interact with the NoSQL Firestore DB
        Date Created: 11/24/2021
        Author: Tucker Nulty
        
        Funtions:
            -InitlaizeProject(string project)
                -Not used currently but could be used to clean code in the future
            -db_EditMajorName(string project, MajorData major, string oldName)
                -Used for changing the name of a degree, requires an string with a degree that currently exists
            -db_EditMajorData(string project, MajorData major)
                -Used to update a document stored in the Degrees collection or create it if it does not exitst
            -db_DeleteMajorData(string project, MajorData major)
                -Deletes a document with a matching id to the string stored in major.MajorName
            -PrintDocumentSnap(DocumentSnapshot documentSnapshot)
                -DEBUG ONLY
                -Prints a single DocumentSnapshot to the console
            -db_StoreMajorData(string project)
                -Retrives all documents stored in the degrees collection and puts them in a Linked List
            -GetMajorData(string project)
                -Public facing fucntion used to call db_StoreMajorData
                -Returns a LinkedList<DocumentSnapshot>
            -EditMajorData(string project, MajorData major)
                -Public facing function used to call db_EditMajorData
            -DeleteMajorData(string project, MajorData major)
                -Public facing function used to call db_EditMajorData
            -EditMajorName(string project, MajorData major, string oldName)
                -Public facing function used to call db_EditMajorName


        */


        private static void InitializeProject(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
        }

        private static async Task db_EditMajorName(string project, MajorData major, string oldName)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRefDelete = db.Collection("pages").Document("Majors").Collection("Degrees").Document(oldName);
            db_EditMajorData(project, major).Wait();
            await docRefDelete.DeleteAsync();
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
        
        private static async Task db_DeleteMajorData(string project, MajorData major)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRef = db.Collection("pages").Document("Majors").Collection("Degrees").Document(major.MajorName);
            await docRef.DeleteAsync();
        }


        private static async Task<LinkedList<DocumentSnapshot>> db_StoreMajorData(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            CollectionReference userRef = db.Collection("pages").Document("Majors").Collection("Degrees");
            Debug.WriteLine("Before query");
            QuerySnapshot snapshot = await userRef.GetSnapshotAsync();

            LinkedList<DocumentSnapshot> tempDocs = new LinkedList<DocumentSnapshot>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                tempDocs.AddLast(document);
                //Debug.WriteLine("Help");
            }
            return tempDocs;
            
        }

        private static async Task db_EditMajorCatagoryTitle (string project, int CatagoryPos, string newTitle)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRef = db.Collection("pages").Document("Majors");
            List<object> tags = new List<object>();
            foreach(object j in docRef.)
            {
                tags.Add(j);
            }
        }
        private static async Task<DocumentSnapshot> db_StoreMajorCategories(string project)
        { 
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference userRef = db.Collection("pages").Document("Majors");
            DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

            return snapshot;

        }

        public DocumentSnapshot StoreMajorCategories(string project)
        {
            return Task<DocumentSnapshot>.Run( () => db_StoreMajorCategories(project)).Result;
        }
        public LinkedList<DocumentSnapshot> GetMajorData(string project)
        {         
            LinkedList<DocumentSnapshot> documentSnapshots = Task<LinkedList<DocumentSnapshot>>.Run(() => db_StoreMajorData(project)).Result;
            return documentSnapshots;
        }

        public void EditMajorData(string project, MajorData major)
        {
            Task<LinkedList<DocumentSnapshot>>.Run(() => db_EditMajorData(project, major)).Wait();
        }

        public void DeleteMajorData(string project, MajorData major)
        {
            Task<LinkedList<DocumentSnapshot>>.Run(() => db_DeleteMajorData(project, major)).Wait();
        }
        public void EditMajorName(string project, MajorData major, string oldName)
        {
            Task<LinkedList<DocumentSnapshot>>.Run(() => db_EditMajorName(project, major, oldName)).Wait();
        }

        public void EditCategoryTitle(int catagoryPosition, string newTitle)
        {

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

        public void PrintDocumentSnap(DocumentSnapshot documentSnapshot)
        {
            Dictionary<string, object> documentDictionary = documentSnapshot.ToDictionary();
            Console.WriteLine("Major: {0}", documentSnapshot.Id);
            Console.WriteLine("Major_Category: {0}", EmployersToCSV(documentDictionary["type"] as List<object>));


            Console.WriteLine("Campus: {0}", EmployersToCSV(documentDictionary["campuses"] as List<object>));
            //Console.WriteLine("Classes: {0}", EmployersToCSV(documentDictionary["Classes"] as List<object>));
            Console.WriteLine("about: {0}", EmployersToCSV(documentDictionary["about"] as List<object>));
        }
    }
}

