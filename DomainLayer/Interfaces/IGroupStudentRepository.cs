using DomainLayer.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces
{
    public interface IGroupStudentRepository
    {
        IEnumerable<Group> GetGroups();
        IEnumerable<Student> GetStudents();

        IEnumerable<Group> GetGroupsByStudent(Student student);
        IEnumerable<Student> GetStudentsByGroup(Group group);
        
        void AddStudentToGroup(Guid groupId, IEnumerable<Student> students);
    }
}
