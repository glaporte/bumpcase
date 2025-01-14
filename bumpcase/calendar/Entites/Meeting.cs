using calendar.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace calendar.Entites
{
    [Table(nameof(Meeting))]
    public class Meeting /*: IValidatableObject*/
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string MeetingPurpose { get; set; }
        public string Report {  get; set; }

        [Required]
        [ForeignKey("Veterinarian")]
        public int VeterinarianId { get; set; }
        public Veterinarian? Veterinarian { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{

        //}
    }
}
