
using DomainLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Student
    {
        [Key]
        [Required]
        public Guid ID { get; }

        [Required]
        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [MaxLength(60)]
        [Display(Name = "Отчество")]
        public string Patronomic { get; set; }

        [Display(Name = "UID")]
        public UniqueID UniqueID { get; set; }

        [Display(Name = "Группы")]
        public List<StudentGroup> Groups { get; set; } = new List<StudentGroup>();
    }
}
