using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDatabaseFramework
{
    public class MajorData
    {
        public string MajorName { get; set; }
        public string OldName { get; set; }
        public List<string> type { get; set; }
        public List<string> Classes { get; set; }
        public List<string> Professors { get; set; }
        public List<string> campuses { get; set; }
        public List<string> about { get; set; }

    }
    
    public class MajorCategories
    {
        public string categoryTitle { get; set; }
        public List<Object> relatedDegrees { get; set; }
    }
}
