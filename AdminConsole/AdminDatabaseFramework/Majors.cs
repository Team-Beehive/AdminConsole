using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;
using System.Threading.Tasks;


namespace AdminDatabaseFramework
{
    /*--------------
        Class: MajorList
        Purpose: Used to store the Majors stored in the cloud database in memory on the admin computer
        Date Created: 11/28/2021
        Author: Tucker Nulty

        Ctors:
            -()
                -Creates an inital download of the database

        Local Variables:
            -private LinkedList<DocumentSnapshot> m_dataBaseRefs
                -Used to store the majors from the database
            -private MajorDatabase majorDatabase
                -An instance of MajorDatabase used to interact with the firestore database
            
        
        Funtions:
            -EditMajor(MajorData majorData)
                -Creates or updates a major
                -CANNOT CHANGE THE NAME OF A MAJOR!
            -EditMajor(MajorData major, string oldName)
                -Updates a major that requires a name change
            -DeleteMajor(MajorData major)
                -Deletes a major that exists in the database
            -EditMajorCatagoryTitle(string oldName, string newName)
                -Changes a Catagory's title requires the old name
            -AddMajorToCat(string CatName, MajorData major)
                -Adds a major to a catagory
                -This can only be ran if the major exists in the database, highly reccommended to run updateLocal before this 
            -RemoveMajorFromCat(MajorCategories majorCategories, MajorData major)
                -Removes the passed MajorData from the passed MajorCategories
                -Only works if a DocumentReference exists in the MajorData
            -CreateMajorCategory(string catTitle)
                -Creates a new Major Category with the title passes
            - DeleteMajorCategory(MajorCategories majorCategories)
                -Deletes the MajorCategory passed if found
            -UpdateLocal()
                -Updates the m_dataBaseRefs
                -Reccomended to call after each update
            -GetMajors()
                -Returns all majors stored in the database in the MajorData class
                -Returns a LinkedList<MajorData>
            -printMajors()
                -Prints all majors to the console
                -USE FOR DEBUGGING ONLY 
        */
    public class Majors
    {
        private LinkedList<DocumentSnapshot> m_dataBaseRefs;
        private MajorDatabase majorDatabase;
        private LinkedList<MajorData> m_localMajors = new LinkedList<MajorData>();
        public Majors(FirestoreDb m_db)
        {
            majorDatabase = new MajorDatabase(m_db);
            m_dataBaseRefs = majorDatabase.GetMajorData();
        }

        public void updateDB(FirestoreDb m_db)
        {
            majorDatabase = new MajorDatabase(m_db);
        }

        public void EditMajor(MajorData major)
        {
            majorDatabase.EditMajorData(major);
        }

        public void EditMajor(MajorData major, string oldName)
        {
            majorDatabase.EditMajorName(major, oldName);
        }

        public void DeleteMajor(MajorData major)
        {
            majorDatabase.DeleteMajorData(major);
        }

        public void EditMajorCatagoryTitle(MajorCategories majorCategories)
        {
            majorDatabase.EditCategoryTitle(majorCategories.oldTitle, majorCategories.categoryTitle);
        }

        public void AddMajorToCat(MajorCategories majorCategories, MajorData major)
        {
            majorDatabase.AddMajorToCat(majorCategories, major);
        }

        public void RemoveMajorFromCat(MajorCategories majorCategories, MajorData major)
        {
            majorDatabase.RemoveMajorFromCat(majorCategories, major);
        }

        public void CreateMajorCategory(string catTitle)
        {
            majorDatabase.CreateMajorCategory(catTitle);
        }

        public void DeleteMajorCategory(MajorCategories majorCategories)
        {
            majorDatabase.DeleteMajorCategory(majorCategories);
        }
        public void UpdateLocal()
        {
            m_dataBaseRefs = majorDatabase.GetMajorData();
        }

        public LinkedList<MajorData> GetMajors()
        {

            try
            {
                LinkedList<MajorData> datas = new LinkedList<MajorData>();
                foreach (DocumentSnapshot document in m_dataBaseRefs)
                {
                    Dictionary<string, object> documentDictionary = document.ToDictionary();
                    MajorData tempMajor = new MajorData();
                    tempMajor.MajorName = document.Id;
                    tempMajor.OldName = document.Id;
                    if (documentDictionary.ContainsKey("about")
                        )
                        tempMajor.about = ObjectFunctions.ObjToStr(documentDictionary["about"] as List<object>);

                    if (documentDictionary.ContainsKey("campuses"))
                        tempMajor.campuses = ObjectFunctions.ObjToStr(documentDictionary["campuses"] as List<object>);

                    if (documentDictionary.ContainsKey("type"))
                        tempMajor.type = ObjectFunctions.ObjToStr(documentDictionary["type"] as List<object>);

                    if (documentDictionary.ContainsKey("classes"))
                        tempMajor.Classes = ObjectFunctions.ObjToStr(documentDictionary["classes"] as List<object>);


                    tempMajor.DocumentReferenceSelf = document.Reference;
                    datas.AddLast(tempMajor);
                }
                m_localMajors = datas;
                return datas;
            }
            catch
            {
                throw new DatabaseException("Could not save data from database");
            }
            
        }

        public LinkedList<MajorCategories> GetCategories()
        {
            
            DocumentSnapshot documentSnapshot = majorDatabase.StoreMajorCategories();
            LinkedList<MajorCategories> majorCategories = new LinkedList<MajorCategories>();

            List<Dictionary<string, object>> categoryList = documentSnapshot.GetValue<List<Dictionary<string, object>>>("Categories");
            foreach (Dictionary<string, object> category in categoryList)
            {
                MajorCategories majorCat = new MajorCategories();
                majorCat.categoryTitle =  category.ContainsKey("categoryTitle") ? category["categoryTitle"].ToString() : null;
                majorCat.relatedDegrees = category.ContainsKey("relatedDegrees") ? category["relatedDegrees"] as List<object> : null;
                majorCat.oldTitle = majorCat.categoryTitle;
                majorCategories.AddLast(majorCat);
            }
            return majorCategories;
        }

        public MajorData toMajorData(DocumentReference doc)
        {
            foreach(DocumentSnapshot snap in m_dataBaseRefs)
            {
                if(doc.Id == snap.Id)
                {
                    Dictionary<string, object> data = snap.ToDictionary();
                    MajorData mdata = new MajorData();
                    mdata.MajorName = doc.Id;
                    mdata.OldName = doc.Id;
                    if (data.ContainsKey("about"))
                        mdata.about = ObjectFunctions.ObjToStr(data["about"]);
                    if (data.ContainsKey("campuses"))
                        mdata.campuses = ObjectFunctions.ObjToStr(data["campuses"]);
                    if (data.ContainsKey("type"))
                        mdata.type = ObjectFunctions.ObjToStr(data["type"]);
                    if (data.ContainsKey("classes"))
                        mdata.Classes = ObjectFunctions.ObjToStr(data["classes"]);

                    mdata.DocumentReferenceSelf = doc;
                    return mdata;
                }
            }
            return new MajorData();
        }
        public void printMajors()
        {
            foreach(DocumentSnapshot document in m_dataBaseRefs)
            {
                majorDatabase.PrintDocumentSnap(document);
            }    

        }
    }
}
