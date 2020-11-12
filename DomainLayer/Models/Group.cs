
using DomainLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Group
    {
        [Required]
        public Guid ID { get; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Студенты")]
        public List<StudentGroup> Students { get; set; } = new List<StudentGroup>();
    }
}
