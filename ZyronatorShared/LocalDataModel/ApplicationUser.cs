using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZyronatorShared.LocalDataModel
{
    [Table("ApplicationUser")]
    internal class ApplicationUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(32)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(68)]
        public string UserPassword { get; set; }
        public Guid AuthorizationToken { get; set; }
        public DateTime AuthorizationDate { get; set; }
    }
}
