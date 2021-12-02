using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;
using System.Threading.Tasks;


namespace AdminDatabaseInteractions
{
    /*--------------
        Class: MajorList
        Purpose: Used to store the Majors stored in the cloud database in memory on the admin computer
        Date Created: 11/28/2021
        Author: Tucker Nulty

        Ctors:
         -(int size)
            Creates an array with a size equal to the passed int
         -(MajorData[] majorData)
            Copies a MajorData array into the MajorList
        
        Funtions:
          -AddMajor(MajorData majorData)
            -Used to add a single MajorData object into the MajorList
          -ReturnMajor(int pos)
            -Used to return the MajorData stored at the position passed
          -
        */
    public class Majors
    {
        LinkedList<DocumentSnapshot> m_dataBaseRefs = new LinkedList<DocumentSnapshot>();
        public string project = "oit-kiosk";
        private MajorDatabase majorDatabase = new MajorDatabase();
        public Majors()
        {  
            m_dataBaseRefs = majorDatabase.GetMajorData(project);
        }

        public void EditMajor(MajorData major)
        {
            majorDatabase.EditMajorData(project, major);
        }

        public void UpdateLocal()
        {
            m_dataBaseRefs = majorDatabase.GetMajorData(project);
        }

        public void printMajors()
        {
            foreach(DocumentSnapshot document in m_dataBaseRefs)
            {
                majorDatabase.ShToMD(document);
            }    

        }
        


    }
}
