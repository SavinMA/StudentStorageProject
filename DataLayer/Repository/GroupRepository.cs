using DomainLayer.Interfaces;
using DomainLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly StorageContext db;

        public GroupRepository(StorageContext context)
        {
            db = context;
        }
        
        public void Create(Group item)
        {
            db.Groups.Add(item);
        }

        public void Delete(Guid id)
        {
            var group = db.Groups.Find(id);
            if (group != null)
                db.Groups.Remove(group);
        }

        public Group Get(Guid id)
        {
            return db.Groups.Find(id);
        }

        public IEnumerable<Group> GetAll()
        {
            return db.Groups.ToList();
        }

        public void Update(Guid id, Group item)
        {
            var group = db.Groups.Find(id);
            if (group != null)
            {
                group.Name = item.Name;
                db.Entry(group).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }
    }
}
