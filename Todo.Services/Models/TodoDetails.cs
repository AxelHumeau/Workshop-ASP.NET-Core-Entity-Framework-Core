using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services.Models
{
    public class TodoDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
