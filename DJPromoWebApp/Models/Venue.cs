using System.ComponentModel.DataAnnotations;

namespace DJPromoWebApp.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}