using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces
{
    public interface IGroup
    {
        string Name { get; }

        List<IStudent> Students { get; }
    }
}
