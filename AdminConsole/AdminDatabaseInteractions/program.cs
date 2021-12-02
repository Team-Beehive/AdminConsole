using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDatabaseInteractions
{
    class program
    {
        public static void Main()
        {
            Majors majors = new Majors();
            MajorData majorData = new MajorData();
            majorData.MajorName = "Test Major";
            majorData.about = "about testing";
            majorData.type = "Bachelor of Science";
            majors.EditMajor(majorData);
            majors.printMajors();
        }
    }
}
