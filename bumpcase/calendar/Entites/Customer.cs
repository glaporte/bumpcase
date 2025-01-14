using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace calendar.Entites
{
    [Table(nameof(Customer))]

    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public int? FavoriteVeterinarianId { get; set; } = null;
        [JsonIgnore]
        public Veterinarian? FavoriteVeterinarian { get; set; } = null;
        public ICollection<Patient> Patients { get; } = new List<Patient>();
        public ICollection<Meeting> Meetings { get; } = new List<Meeting>();
    }
}
