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
            -public string project
                -Stores the project id used to connect to firestore
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
        private LinkedList<DocumentSnapshot> m_dataBaseRefs = new LinkedList<DocumentSnapshot>();
        public string project = "oit-kiosk";
        private MajorDatabase majorDatabase = new MajorDatabase();
        public Majors()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\nulty\\Documents\\JrProject\\Database\\oit-kiosk-firebase-adminsdk-u24sq-8f7958c50f.json");
            m_dataBaseRefs = majorDatabase.GetMajorData(project);
        }

        public void EditMajor(MajorData major)
        {
            majorDatabase.EditMajorData(project, major);

            if (major.MajorName != major.OldName)
            {
                string temp = major.OldName;
                major.OldName = major.MajorName;
                majorDatabase.EditMajorName(project, major, temp);
            }
            else
            {
                majorDatabase.EditMajorData(project, major);
            }
        }

        public void EditMajor(MajorData major, string oldName)
        {
            majorDatabase.EditMajorName(project, major, oldName);
        }

        public void DeleteMajor(MajorData major)
        {
            majorDatabase.DeleteMajorData(project, major);
        }

        public void UpdateLocal()
        {
            m_dataBaseRefs = majorDatabase.GetMajorData(project);
        }

        public LinkedList<MajorData> GetMajors()
        {
            LinkedList<MajorData> datas = new LinkedList<MajorData>();
            foreach(DocumentSnapshot document in m_dataBaseRefs)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                MajorData tempMajor = new MajorData();
                tempMajor.MajorName = document.Id;
                tempMajor.OldName = document.Id;
                tempMajor.about = ObjToStr(documentDictionary["about"] as List<object>);
                tempMajor.campuses = ObjToStr(documentDictionary["campuses"] as List<object>);
                tempMajor.type = ObjToStr(documentDictionary["type"] as List<object>);
                datas.AddLast(tempMajor);
            }
            return datas;
        }

        public LinkedList<MajorCategories> GetCategories()
        {
            DocumentSnapshot documentSnapshot = majorDatabase.StoreMajorCategories(project);
            LinkedList<MajorCategories> majorCategories = new LinkedList<MajorCategories>();

            List<Dictionary<string, object>> categoryList = documentSnapshot.GetValue<List<Dictionary<string, object>>>("Categories");
            foreach (Dictionary<string, object> category in categoryList)
            {
                MajorCategories majorCat = new MajorCategories();
                majorCat.categoryTitle = category["categoryTitle"].ToString();
                majorCat.relatedDegrees = category["relatedDegrees"] as List<object>;
                majorCategories.AddLast(majorCat);
            }
            return majorCategories;
        }

        public void EditCategories(int catagoryPosition, string newTitle)
        {
            
        }

        public void printMajors()
        {
            foreach(DocumentSnapshot document in m_dataBaseRefs)
            {
                majorDatabase.PrintDocumentSnap(document);
            }    

        }

        private List<string> ObjToStr(List<object> objList)
        {
            List<string> tempList = new List<string>(objList.Count);

            foreach(object obj in objList)
            {
                tempList.Add(obj.ToString());
            }

            return tempList;
        }
        


    }
}
