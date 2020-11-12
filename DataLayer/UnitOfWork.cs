using DataLayer.Repository;

using DomainLayer.Interfaces;

using System;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StorageContext db = new StorageContext();

        private IStudentRepository studentRepository;
        private IGroupRepository groupRepository;
        private IUidRepository uidRepository;
        private IGroupStudentRepository groupStudentRepository;

        public IStudentRepository StudentRepository
        {
            get
            {
                return studentRepository ??= new StudentRepository(db);
            }
        }

        public IGroupRepository GroupRepository
        {
            get
            {
                return groupRepository ??= new GroupRepository(db);
            }
        }

        public IUidRepository UidRepository
        {
            get
            {
                return uidRepository ??= new UidRepository(db);
            }
        }

        public IGroupStudentRepository GroupStudentRepository
        {
            get
            {
                return groupStudentRepository ??= new GroupStudentRepository(db);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
