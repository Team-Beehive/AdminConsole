using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDatabaseInteraction
{
    class MajorData
    {
        public string MajorName { get; set; }
        public int FirstOffered { get; set; }
        public string[] Employers { get; set; }
        public int ExpectedCredHours { get; set; }
        public string Description { get; set; }
    }
}
