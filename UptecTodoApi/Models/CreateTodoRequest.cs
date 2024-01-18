using System.ComponentModel.DataAnnotations;

namespace UptecTodoApi.Models
{
    public class CreateTodoRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}