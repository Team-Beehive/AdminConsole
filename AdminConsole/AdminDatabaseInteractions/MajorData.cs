using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDatabaseInteraction
{
    public class MajorData
    {
        public string MajorName { get; set; }
        public List<string> Classes { get; set; }
        public List<string> Professors { get; set; }
        public string MajorCategory { get; set; }
        public string Description { get; set; }

    }
}
