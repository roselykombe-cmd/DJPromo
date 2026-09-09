using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DJPromoWebApp.Models
{
    public class Gig
    {
        [Key]
        public int GigID { get; set; }

        [Required]
        [ForeignKey(nameof(DJ))]
        public int DJID { get; set; }

        [Required]
        [ForeignKey(nameof(Venue))]
        public int VenueID { get; set; }

        public DJ? DJ { get; set; }
        public Venue? Venue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
    }
}