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
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\nulty\\Documents\\JrProject\\Database\\oit-kiosk-firebase-adminsdk-u24sq-8f7958c50f.JSON");
            m_dataBaseRefs = majorDatabase.GetMajorData(project);
        }

        public void EditMajor(MajorData major)
        {
            majorDatabase.EditMajorData(project, major);
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
                Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
                MajorData tempMajor = new MajorData();
                tempMajor.MajorName = document.Id;
                tempMajor.about = documentDictionary["about"] as List<string>;
                tempMajor.campuses = documentDictionary["campuses"] as List<string>;
                tempMajor.type = documentDictionary["type"] as List<string>;
                datas.AddLast(tempMajor);
            }
            return datas;
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
