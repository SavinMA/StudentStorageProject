using System;

namespace DomainLayer.Interfaces
{
    public interface IUniqueID
    {
        Guid ID { get; }
        string UID { get; set; }
    }
}
