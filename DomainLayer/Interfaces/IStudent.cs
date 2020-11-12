using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces
{
    public interface IStudent
    {
        public Guid ID { get; }
        public string Gender { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronomic { get; set; }
        public IUniqueID UniqueId { get; }
        public List<IGroup> Groups { get; }
    }
}
