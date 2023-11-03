using System.ComponentModel.DataAnnotations;

namespace CoolBlue.Products.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}