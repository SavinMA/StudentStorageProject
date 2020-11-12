using DomainLayer.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces
{
    public interface IUidRepository : IRepository<UniqueID>
    {
        bool TryCreate(string uid, out UniqueID uniqueID);
    }
}
