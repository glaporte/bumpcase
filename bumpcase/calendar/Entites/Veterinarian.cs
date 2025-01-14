using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace calendar.Entites
{
    [Table(nameof(Veterinarian))]
    public class Veterinarian
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ICollection<Customer> Customers { get; } = new List<Customer>();
        public ICollection<Meeting> Meetings { get; } = new List<Meeting>();
        public ICollection<Slot> Slots { get; } = new List<Slot>();

    }
}
