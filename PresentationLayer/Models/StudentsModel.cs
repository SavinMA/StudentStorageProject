using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class StudentsModel
    {
        public string ID { get; set; }
        public string FIO { get; set; }
        public string UID { get; set; }
        public string Gender { get; set; }
        public IEnumerable<string> GroupNames { get; set; }
    }
}
