using System.ComponentModel.DataAnnotations;

namespace Todo.web.Models
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EndDate { get; set;}
        public DateTime CreatedDate { get; set; }
    }
}
