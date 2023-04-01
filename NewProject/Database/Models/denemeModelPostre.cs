using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VSPWebApi.API.Database.Models
{
    public class denemeModelPostre
    {
        public string? name { get; set; }
        public string? surname { get; set; }
        [Key]
        public string? id { get; set; }
    }
}
