using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace calendar.Entites
{
    [Table(nameof(Veterinarian))]
    public class Veterinarian
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ICollection<Customer> Customers { get; } = new List<Customer>();
        public ICollection<Meeting> Meetings { get; } = new List<Meeting>();
        public ICollection<Slot> Slots { get; } = new List<Slot>();

    }
}
