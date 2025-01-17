using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IEnumerable<ApplicationUser> Users { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.MaxValue;
    }
}
