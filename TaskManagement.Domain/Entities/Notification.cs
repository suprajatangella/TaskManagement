using Microsoft.AspNetCore.Mvc.Rendering;
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
        [NotMapped]
        public IEnumerable<SelectListItem>? UserList { get; set; }

        [Required]
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
