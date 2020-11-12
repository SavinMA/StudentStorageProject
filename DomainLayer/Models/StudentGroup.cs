using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class StudentGroup
    {
        public Guid StudentGuid { get; set; }
        public Guid GroupGuid { get; set; }

        public Student Student { get; set; }
        public Group Group { get; set; }
    }
}
