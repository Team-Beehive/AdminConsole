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
        private FirestoreDb db;
        public MajorDatabase(FirestoreDb m_db)
        {
            try
            {
                db = m_db;
            }
            catch
            {
                throw new DatabaseException("Unable to connect to Firestore");
            }
        }

        private async Task db_EditMajorName(MajorData major, string oldName)
        {
            try
            {
                DocumentReference docRefDelete = db.Collection("pages").Document("Majors").Collection("Degrees").Document(oldName);
                db_EditMajorData(major).Wait();
                await docRefDelete.DeleteAsync();
            }
            catch
            {
                throw new DatabaseException("Major Delete could not complete");
            }
        }

        private async Task db_EditMajorData(MajorData major)
        {
            try
            {
                DocumentReference docRef = db.Collection("pages").Document("Majors").Collection("Degrees").Document(major.MajorName);
                Dictionary<string, object> majorDict = new Dictionary<string, object>
            {
                {"about", major.about },
                {"campuses", major.campuses },
                {"type", major.type }
            };
                await docRef.SetAsync(majorDict);
            }
            catch
            {
                throw new DatabaseException("Major Data Edit could not complete");
            }
        }

        private async Task db_DeleteMajorData(MajorData major)
        {
            try
            {
                DocumentReference docRef = db.Collection("pages").Document("Majors").Collection("Degrees").Document(major.MajorName);
                await docRef.DeleteAsync();
            }
            catch
            {
                throw new DatabaseException("Major Delete could not complete");
            }
        }


        private async Task<LinkedList<DocumentSnapshot>> db_StoreMajorData()
        {
            try
            {
                CollectionReference userRef = db.Collection("pages").Document("Majors").Collection("Degrees");
                Debug.WriteLine("Before query");
                QuerySnapshot snapshot = await userRef.GetSnapshotAsync();

                LinkedList<DocumentSnapshot> tempDocs = new LinkedList<DocumentSnapshot>();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    tempDocs.AddLast(document);
                }
                return tempDocs;
            }
            catch
            {
                throw new DatabaseException("Major Store could not complete");
            }
        }

        private async Task db_EditMajorCatagoryTitle(string oldTitle, string newTitle)
        {
            try
            {
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
            catch (Exception ex)
            {
                throw new DatabaseException("Edit Major Category Title could not complete");
            }
        }

        private async Task db_AddMajorToCat(MajorCategories majorCat, MajorData majorData)
        {
            try
            {
                DocumentReference docRef = db.Collection("pages").Document("Majors");

                majorCat.relatedDegrees.Add(majorData.DocumentReferenceSelf);

                await docRef.UpdateAsync(majorCat.ToDictionary);
            }
            catch
            {
                throw new DatabaseException("Add Major to Cat could not complete");
            }
        }

        private async Task db_RemoveMajorFromCat(MajorCategories majorCategories, MajorData major)
        {
            try
            {
                DocumentReference docRef = db.Collection("pages").Document("Majors");
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
            catch
            {
                throw new DatabaseException("Remove Major from Cat could not complete");
            }
        }

        private async Task<MajorCategories> db_CreateMajorCategory(string catTitle)
        {
            try
            {
                DocumentReference docRef = db.Collection("pages").Document("Majors");
                DocumentSnapshot document = await docRef.GetSnapshotAsync();
                MajorCategories majorCategories = new MajorCategories();
                majorCategories.oldTitle = catTitle;
                majorCategories.categoryTitle = catTitle;

                List<Dictionary<string, object>> categoryList = document.GetValue<List<Dictionary<string, object>>>("Categories");
                categoryList.Add(majorCategories.ToDictionary);

                await docRef.UpdateAsync(dictListToDict(categoryList, "Categories"));
                return majorCategories;
            }
            catch
            {
                throw new DatabaseException("Create major Category could not complete");
            }
        }

        private async Task db_DeleteMajorCategory(MajorCategories majorCategories)
        {
            try
            {
                DocumentReference docRef = db.Collection("pages").Document("Majors");
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
            catch
            {
                throw new DatabaseException("Delete Major Category could not complete");
            }
        }

        private async Task<DocumentSnapshot> db_StoreMajorCategories()
        {
            try
            {
                DocumentReference userRef = db.Collection("pages").Document("Majors");
                DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

                return snapshot;
            }
            catch
            {
                throw new Exception("Store Major Categories could not complete");
            }
        }

        public DocumentSnapshot StoreMajorCategories()
        {
            return Task<DocumentSnapshot>.Run(() => db_StoreMajorCategories()).Result;
        }
        public LinkedList<DocumentSnapshot> GetMajorData()
        {
            LinkedList<DocumentSnapshot> documentSnapshots = Task<LinkedList<DocumentSnapshot>>.Run(() => db_StoreMajorData()).Result;
            return documentSnapshots;
        }



        public void EditMajorData(MajorData major)
        {
            Task<LinkedList<DocumentSnapshot>>.Run(() => db_EditMajorData(major)).Wait();
        }

        public void DeleteMajorData(MajorData major)
        {
            Task<LinkedList<DocumentSnapshot>>.Run(() => db_DeleteMajorData(major)).Wait();
        }
        public void EditMajorName(MajorData major, string oldName)
        {
            Task<LinkedList<DocumentSnapshot>>.Run(() => db_EditMajorName(major, oldName)).Wait();
        }

        public void EditCategoryTitle(string oldTitle, string newTitle)
        {
            Task.Run(() => db_EditMajorCatagoryTitle(oldTitle, newTitle)).Wait();
        }
        public void AddMajorToCat(MajorCategories majorCategories, MajorData major)
        {
            Task.Run(() => db_AddMajorToCat(majorCategories, major)).Wait();
        }

        public void RemoveMajorFromCat(MajorCategories majorCategories, MajorData major)
        {
            Task.Run(() => db_RemoveMajorFromCat(majorCategories, major)).Wait();
        }

        public MajorCategories CreateMajorCategory(string catTitle)
        {
            return Task<MajorCategories>.Run(() => db_CreateMajorCategory(catTitle)).Result;
        }

        public void DeleteMajorCategory(MajorCategories majorCategories)
        {
            Task.Run(() => db_DeleteMajorCategory(majorCategories)).Wait();
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

