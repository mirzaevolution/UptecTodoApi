using System.ComponentModel.DataAnnotations;

namespace UptecTodoApi.Models
{
    public class UpdateTodoRequest
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
