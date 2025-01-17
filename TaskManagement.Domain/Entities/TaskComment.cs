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
        public IEnumerable<Task> Tasks { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IEnumerable<ApplicationUser> Users { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }


    }
}
