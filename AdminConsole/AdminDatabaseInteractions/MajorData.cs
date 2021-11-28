using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDatabaseInteraction
{
    public class MajorData
    {
        public string MajorName { get; set; }
        public int FirstOffered { get; set; }
        public List<string> Employers { get; set; }
        public int ExpectedCredHours { get; set; }
        public string Description { get; set; }

    }
}
