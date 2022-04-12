using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDatabaseFramework
{
    internal static class ObjectFunctions
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
    }
}
