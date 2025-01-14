using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace calendar.Entites
{
    [Table(nameof(Slot))]
    [Index(nameof(Start), nameof(End), nameof(VeterinarianId))]
    public class Slot
    {
        public enum SlotState
        {
            Closed,
            Available,
            Booked,
        }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SlotState State { get; set; }


        [Required]
        [ForeignKey("Veterinarian")]
        public int VeterinarianId { get; set; }
        [JsonIgnore]
        public Veterinarian? Veterinarian { get; set; }

    }
}
