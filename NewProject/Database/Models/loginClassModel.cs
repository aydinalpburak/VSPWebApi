using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VSPWebApi.API.Database.Models
{
    public class loginClassModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public string? id { get; set; }
        public string? pass { get; set; }

    }
}
