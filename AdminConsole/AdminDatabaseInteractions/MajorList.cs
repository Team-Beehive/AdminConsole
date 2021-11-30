using System;
using System.Collections.Generic;
using System.Text;


namespace AdminDatabaseInteraction
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
    public class MajorList
    {
        MajorData[] m_majorDataList;
        public MajorList()
        { }

        public MajorList(int size)
        {
            m_majorDataList = new MajorData[size];
        }
        public MajorList(MajorData[] majorDatas)
        {
            m_majorDataList = new MajorData[majorDatas.Length];
            majorDatas.CopyTo(m_majorDataList, 0);
        }

        public void AddMajor(MajorData major)
        {


            int length;
            if (m_majorDataList == null)
            {
                m_majorDataList = new MajorData[1];
                m_majorDataList[0] = major;
                length = 1;
            }
            else
            {
                length = m_majorDataList.Length;
                MajorData[] tempStorage = new MajorData[length];
                tempStorage = m_majorDataList;
                m_majorDataList = new MajorData[length + 1];
                tempStorage.CopyTo(m_majorDataList, 0);
                m_majorDataList[length] = major;
            }
        }

        public MajorData ReturnMajor(int pos)
        {
            return m_majorDataList[pos];
        }

        public void RemoveMajor(int pos)
        {
            if(m_majorDataList.Length == 1)
            {

            }
            else if(m_majorDataList == null)
            {

            }
            else
            {

            }
        }

        public string[] GetAllCategories()
        {
            try
            {
                LinkedList<string> categoresLL = new LinkedList<string>();
                foreach (MajorData majorData in m_majorDataList)
                {
                    if (!categoresLL.Contains(majorData.MajorCategory))
                    {
                        categoresLL.AddLast(majorData.MajorCategory);
                    }
                }

                string[] categoriesString = new string[categoresLL.Count];
                categoresLL.CopyTo(categoriesString, 0);

                return categoriesString;
            }
            catch
            {
                throw;
            }

        }
        


    }
}
