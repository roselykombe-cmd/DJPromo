using System.ComponentModel.DataAnnotations;

namespace DJPromoWebApp.Models
{
    public class DJ
    {
        [Key]
        public int DJID { get; set; }

        public string StageName { get; set; }
    }
}