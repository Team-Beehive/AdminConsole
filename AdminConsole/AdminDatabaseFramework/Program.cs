﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDatabaseFramework
{
    class program
    {
        public static void Main()
        {
            Majors majors = new Majors();
            MajorData majorData = new MajorData();
            majorData.MajorName = "Testing Major";
            List<string> aboutL = new List<string>(2);
            aboutL.Add("The best major at OIT");            
            List<string> TypeL = new List<string>(2);
            TypeL.Add("Degree in Poggers");            
            List<string> campusesL = new List<string>(2);
            campusesL.Add("Klamath");
            campusesL.Add("Portland");
            majorData.about = aboutL;
            majorData.type = TypeL;
            majorData.campuses = campusesL;
            //majors.DeleteMajor(majorData);
            //majors.EditMajor(majorData);
            //majors.EditMajor(majorData, "Test Major");
            majors.printMajors();

            majors.GetMajors();

            Majors testMajor = new Majors();
            testMajor.GetMajors();
        }
    }
}
