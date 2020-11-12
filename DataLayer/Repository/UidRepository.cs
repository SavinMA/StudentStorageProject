using DomainLayer.Interfaces;
using DomainLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repository
{
    public class UidRepository : IUidRepository
    {
        private readonly StorageContext db;

        public UidRepository(StorageContext context)
        {
            db = context;
        }

        public void Create(UniqueID item)
        {
            db.UniqueIds.Add(item);
        }

        public bool TryCreate(string uid, out UniqueID uniqueID)
        {
            uniqueID = default(UniqueID);

            var uniqueIds = db.UniqueIds.FirstOrDefault(u => u.UID == uid);
            if (uniqueIds is null)
            {
                var newUid = new UniqueID() { UID = uid };
                var entityEntry = db.UniqueIds.Add(newUid);
                db.SaveChanges();
                uniqueID = entityEntry.Entity;
                return true;
            }

            return false;
        }

        public void Delete(Guid id)
        {
            var uniqueId = Get(id);
            if (uniqueId != null)
            {
                db.UniqueIds.Remove(uniqueId);
            }
        }

        public UniqueID Get(Guid id)
        {
            return db.UniqueIds.Find(id);
        }

        public IEnumerable<UniqueID> GetAll()
        {
            return db.UniqueIds.ToList();
        }


        public void Update(Guid id, UniqueID item)
        {
            throw new NotImplementedException();
        }
    }
}
