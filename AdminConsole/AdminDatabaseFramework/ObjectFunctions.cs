using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDatabaseFramework
{
    internal static class ObjectFunctions
    {
        public static List<string> ObjToStr(List<object> objList)
        {
            List<string> tempList = new List<string>(objList.Count);

            foreach (object obj in objList)
            {
                tempList.Add(obj.ToString());
            }

            return tempList;
        }
    }
}