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
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Status { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
        [NotMapped]
        public string? StatusText { get; set; }
        [NotMapped]
        public string? PriorityText { get; set; }
        [NotMapped]
        public string? AssignedToText { get; set; }
        public string? CreatedBy { get; set; } 
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; } 

        [NotMapped]
        public IEnumerable<SelectListItem>? StatusList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Not Started" },
            new SelectListItem { Value = "2", Text = "In Progress" },
            new SelectListItem { Value = "3", Text = "Completed" },
        };

        [NotMapped]
        public IEnumerable<SelectListItem>? PriorityList { get; set; }  = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Critical" },
            new SelectListItem { Value = "2", Text = "High" },
            new SelectListItem { Value = "3", Text = "Medium" },
            new SelectListItem { Value = "4", Text = "Low" }
        };

        [NotMapped]
        public IEnumerable<SelectListItem>? AssignedList { get; set; }

        [NotMapped]
        public ICollection<TaskComment> TaskComments { get; set; }
    }
}
