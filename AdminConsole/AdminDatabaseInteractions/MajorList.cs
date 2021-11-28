using System;
using System.Collections.Generic;
using System.Text;


namespace AdminDatabaseInteraction
{
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

        public void AddMajorToList(MajorData major)
        {
            
            int length = m_majorDataList.Length;
            if (length < 1)
            {
                m_majorDataList = new MajorData[1];
                m_majorDataList[0] = major;
            }
            else
            {
                MajorData[] tempStorage = new MajorData[length];
                tempStorage = m_majorDataList;
                m_majorDataList = new MajorData[length + 1];
                m_majorDataList = tempStorage;
                m_majorDataList[length] = major;
            }
        }
       
    }
}
