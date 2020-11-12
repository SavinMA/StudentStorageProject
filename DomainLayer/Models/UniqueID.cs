
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public class UniqueID
    {
        [Key]
        [Required]
        public Guid ID { get; }

        [MinLength(6)]
        [MaxLength(16)]
        public string UID { get; set; }


        public override string ToString()
        {
            return UID;
        }
    }
}
