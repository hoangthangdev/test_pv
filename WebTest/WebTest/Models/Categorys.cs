using System.ComponentModel.DataAnnotations;

namespace WebTest.Models
{
    public class Categorys
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
