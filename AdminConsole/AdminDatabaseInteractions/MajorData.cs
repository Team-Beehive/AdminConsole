using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDatabaseInteractions
{
    public class MajorData
    {
        public string MajorName { get; set; }
        public List<string> type { get; set; }
        public List<string> Classes { get; set; }
        public List<string> Professors { get; set; }
        public List<string> campuses { get; set; }
        public List<string> about { get; set; }

    }
}
