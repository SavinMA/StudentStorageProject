using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository StudentRepository { get; }
        IGroupRepository GroupRepository { get; }
        IUidRepository UidRepository { get; }
        IGroupStudentRepository GroupStudentRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
