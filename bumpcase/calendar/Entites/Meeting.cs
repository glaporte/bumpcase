using calendar.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace calendar.Entites
{
    [Table(nameof(Meeting))]
    public class Meeting : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string MeetingPurpose { get; set; }
        public string Report {  get; set; }

        [Required]
        [ForeignKey("Slot")]
        public int SlotId { get; set; }
        [JsonIgnore]
        public Slot? Slot { get; set; }

        [Required]
        [ForeignKey("Veterinarian")]
        public int VeterinarianId { get; set; }
        [JsonIgnore]
        public Veterinarian? Veterinarian { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var vete = validationContext.GetRequiredService<VeterinarianRepository>().GetVeterinarian(VeterinarianId);
            if (vete == null)
            {
                yield return new ValidationResult($"Veterinarian does not exist '{VeterinarianId}'.", new[] { nameof(VeterinarianId) });
            }

            var customer = validationContext.GetRequiredService<CustomerRepository>().GetCustomer(CustomerId);
            if (customer == null)
            {
                yield return new ValidationResult($"Customer does not exist '{CustomerId}'.", new[] { nameof(CustomerId) });
            }

            var patient = validationContext.GetRequiredService<PatientRepository>().GetPatient(PatientId);
            if (patient == null)
            {
                yield return new ValidationResult($"Patient does not exist '{PatientId}'.", new[] { nameof(PatientId) });
            }
        }
    }
}
