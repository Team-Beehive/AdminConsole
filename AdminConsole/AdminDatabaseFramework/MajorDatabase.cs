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
            -db_EditMajorCatagoryTitle(string project, string oldTitle, string newTitle)
                -Uses the old title of a Category to update it to the new title
            -db_AddMajorToCat(string project, string catName, MajorData majorData)
                -Searches out a Category and adds the passes majorData to it
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

        private static async Task db_EditMajorCatagoryTitle(string project, string oldTitle, string newTitle)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            DocumentReference docRef = db.Collection("pages").Document("Majors");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            List<Dictionary<string, object>> categoryList = document.GetValue<List<Dictionary<string, object>>>("Categories");

            categoryList.ForEach(category => { if (category["categoryTitle"].ToString() == oldTitle) { category["categoryTitle"] = newTitle; } });

            Dictionary<string, object> categories = new Dictionary<string, object>()
            {
                {"Catagories", categoryList}
            };



            await docRef.UpdateAsync(categories);

        }

        private static async Task db_AddMajorToCat(string project, string catName, MajorData majorData)
        {
            FirestoreDb database = FirestoreDb.Create(project);
            DocumentReference docRef = database.Collection("pages").Document("Majors");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();

            List<Dictionary<string, object>> categoryList = document.GetValue<List<Dictionary<string, object>>>("Categories");
            foreach (Dictionary<string, object> category in categoryList)
            {
                if(category["categoryTitle"].ToString() == catName)
                {
                    (category["relatedDegrees"] as List<object>).Add(majorData.DocumentReferenceSelf);
                }
            }

            Dictionary<string, object> categories = new Dictionary<string, object>()
            {
                {"Catagories", categoryList}
            };
            await docRef.UpdateAsync(categories);

        }

        private static async Task db_RemoveMajorFromCat(string project, MajorCategories majorCategories, MajorData major)
        {
            FirestoreDb database = FirestoreDb.Create(project);
            DocumentReference docRef = database.Collection("pages").Document("Majors");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();

            List<Dictionary<string, object>> categoryList = document.GetValue<List<Dictionary<string, object>>>("Categories");
            foreach (Dictionary<string, object> category in categoryList)
            {
                if (category["categoryTitle"].ToString() == majorCategories.categoryTitle)
                {
                    (category["relatedDegrees"] as List<object>).Remove(major.DocumentReferenceSelf);
                }
            }

            await docRef.UpdateAsync(dictListToDict(categoryList, "Categories"));

        }

        private static async Task<MajorCategories> db_CreateMajorCategory(string project, string catTitle)
        {
            FirestoreDb database = FirestoreDb.Create(project);
            DocumentReference docRef = database.Collection("pages").Document("Majors");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            MajorCategories majorCategories = new MajorCategories();
            majorCategories.oldTitle = catTitle;
            majorCategories.categoryTitle = catTitle;

            List<Dictionary<string, object>> categoryList = document.GetValue<List<Dictionary<string, object>>>("Categories");
            categoryList.Add(majorCategories.ToDictionary);

            await docRef.UpdateAsync(dictListToDict(categoryList, "Categories"));
            return majorCategories;
        }

        private static async Task db_DeleteMajorCategory(string project, MajorCategories majorCategories)
        {
            FirestoreDb database = FirestoreDb.Create(project);
            DocumentReference docRef = database.Collection("pages").Document("Majors");
            DocumentSnapshot document = await docRef.GetSnapshotAsync();

            List<Dictionary<string, object>> categoryList = document.GetValue<List<Dictionary<string, object>>>("Categories");
            foreach (Dictionary<string, object> category in categoryList)
            {
                if (category["categoryTitle"].ToString() == majorCategories.categoryTitle)
                {
                    categoryList.Remove(category);
                    break;
                }
            }
            await docRef.UpdateAsync(dictListToDict(categoryList, "Categories"));
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

        public void EditCategoryTitle(string project, string oldTitle, string newTitle)
        {
            Task.Run(() => db_EditMajorCatagoryTitle(project, oldTitle, newTitle)).Wait();
        }
        public void AddMajorToCat(string project, string CatName, MajorData major)
        {
            Task.Run(() => db_AddMajorToCat(project, CatName, major)).Wait();
        }

        public void RemoveMajorFromCat(string project, MajorCategories majorCategories, MajorData major)
        {
            Task.Run(() => db_RemoveMajorFromCat(project, majorCategories, major)).Wait();
        }

        public MajorCategories CreateMajorCategory(string project, string catTitle)
        {
            return Task<MajorCategories>.Run(() => db_CreateMajorCategory(project, catTitle)).Result;
        }

        public void DeleteMajorCategory(string project, MajorCategories majorCategories)
        {
            Task.Run(()=> db_DeleteMajorCategory(project, majorCategories)).Wait();
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

        private static Dictionary<string, object> dictListToDict(List<Dictionary<string, object>> list, string listName)
        {
            Dictionary<string, object> categories = new Dictionary<string, object>()
            {
                {listName, list}
            };
            return categories;
        }

    }
}

