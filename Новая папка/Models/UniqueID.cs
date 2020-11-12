using DomainLayer.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class UniqueID 
    {
        [Key]
        [MinLength(6)]
        [MaxLength(16)]
        public string UID { get; set; }
    }
}
