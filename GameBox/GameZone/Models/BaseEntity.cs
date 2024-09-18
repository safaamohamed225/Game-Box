
namespace GameBox.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

    }
}
