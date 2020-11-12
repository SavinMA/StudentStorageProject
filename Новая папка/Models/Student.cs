
using DomainLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Student
    {
        [Key]
        [Required]
        public Guid ID { get; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [MaxLength(40)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string Patronomic { get; set; }

        public UniqueID UniqueId { get; set; }

        public List<Student> Groups { get; }
    }
}
