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
    public class TaskComment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public IEnumerable<Task>? Tasks { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IEnumerable<ApplicationUser>? Users { get; set; }
        [Required]
        public string CommentText { get; set; } = string.Empty;
        [Required]
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? UpdatedBy { get; set; }
        public DateOnly? UpdatedDate { get; set; } = null;

        [NotMapped]
        public IEnumerable<SelectListItem>? UserList { get; set; }


    }
}
