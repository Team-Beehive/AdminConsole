using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDatabaseFramework
{
    public struct ProfessorSortLinker
    {
        public ProfessorData m_professor;
        public string m_LastName;
    }

    public static class ObjectFunctions
    {
        public static List<string> ObjToStr(object objList)
        {

            if (objList != null)
            {
                List<string> tempList = new List<string>((objList as List<object>).Count);

                foreach (object obj in (objList as List<object>))
                {
                    tempList.Add(obj.ToString());
                }

                return tempList;
            }
            else
            {
                return new List<string>();
            }
        }


        public static LinkedList<ProfessorData> SortByLastName(LinkedList<ProfessorData> in_data)
        {
            List<ProfessorSortLinker> sortable_Data = new List<ProfessorSortLinker>();
            foreach(ProfessorData professor in in_data)
            {
                sortable_Data.Add(new ProfessorSortLinker { m_professor = professor, m_LastName = professor.professorName.Split(' ').Last()});
            }

            QuickSort(ref sortable_Data, 0, sortable_Data.Count - 1);

            LinkedList<ProfessorData> out_data = new LinkedList<ProfessorData>();
            foreach (ProfessorSortLinker linker in sortable_Data)
            { 
                out_data.AddLast(linker.m_professor);
            }
            return out_data;
        }

        public static void QuickSort(ref List<ProfessorSortLinker> data, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(ref data, low, high);

                QuickSort(ref data, low, pi - 1);
                QuickSort(ref data, pi + 1, high);
            }
        }

        private static int Partition(ref List<ProfessorSortLinker> data, int low, int high)
        {
            string pivot = data[high].m_LastName;
            int i = (low - 1);
            for (int j = low; j <= high; j++)
            {
                if (data[j].m_LastName.CompareTo(pivot) < 0)
                {
                    i++;
                    Swap(ref data, i, j);
                }
            }
            Swap(ref data, (i + 1), high);
            return i + 1;


        }

        private static void Swap (ref List<ProfessorSortLinker> data, int index, int swap)
        {
            ProfessorSortLinker temp = data[index]; ;
            data[index] = data[swap];
            data[swap] = temp;
        }
    }
}
