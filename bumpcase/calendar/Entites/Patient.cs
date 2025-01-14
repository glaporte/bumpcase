using calendar.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace calendar.Entites
{
    [Table(nameof(Patient))]
    public class Patient : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public enum PatientFamily
        {
            Other,
            Feline,
            Fish,
            Rodent,
        }
        public PatientFamily Family { get; set; }
        public string PatientSpecies { get; set; } = string.Empty;
        public bool IsMale { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsFemale => !IsMale;

        [Required]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        [JsonIgnore]
        public Customer? Owner { get; set; }
        public ICollection<Meeting> Meetings { get; } = new List<Meeting>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var owner = validationContext.GetRequiredService<CustomerRepository>().GetCustomer(OwnerId);
        
            if (owner == null)
            {
                yield return new ValidationResult($"Owner does not exist '{OwnerId}'.", new[] { nameof(OwnerId) });
            }
        }
    }
}
