using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApplication.Models
{
    public class ToDoListItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "ItemName is required")]
        [Column(TypeName = "nvarchar(200)")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "ItemDescription is required")]
        [Column(TypeName = "nvarchar(200)")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "ItemStatus is required")]
        [Column(TypeName = "bit")]
        public bool ItemStatus { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string loggedUser { get; set; }

    }
}
