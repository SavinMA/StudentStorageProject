using DomainLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Group 
    {
        [Required]
        public Guid ID { get; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public List<Student> Students { get; }
    }
}
