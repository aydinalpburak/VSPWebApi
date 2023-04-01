
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace VSPWebApi.API.Database.Models
{
    namespace eczane_tracker
    {


        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
      

        public class Information
        {
            public string? title { get; set; }
            public string? content { get; set; }
        }

        public class Nutrition
        {
            public string? name { get; set; }
            public string? amount { get; set; }
        }

        public class Recipe
        {
            public List<string> instructions { get; set; }
            public List<Nutrition> nutrition { get; set; }
        }

        public class Root
        {
        
         
            public int id { get; set; }
            public string? name { get; set; }
            public string? image1 { get; set; }
            public string? tagalog { get; set; }
           
            [Column(TypeName = "jsonb")]
            public List<string>? type { get; set; }
            public string? description { get; set; }
            public string? link { get; set; }

            public string? author { get; set; }

            [Column(TypeName = "jsonb")]
            public IList<Information> information { get; set; }


            [Column(TypeName = "jsonb")]
            public Recipe recipe { get; set; }
            public string video { get; set; }
        }

        public class Root2
        {

            public int id { get; set; }
            public string name { get; set; }
            public string tagalog { get; set; }
            public List<string> type { get; set; }
            public string description { get; set; }
            public string link { get; set; }
            public string author { get; set; }
            public List<Information> information { get; set; }
            public Recipe recipe { get; set; }
            public string video { get; set; }
        }

        public class EczaneKonumlari { 
             [Key]
             public decimal latitude { get; set; }
             public decimal longitude { get; set; }
             public decimal latitudeDelta { get; set; }
             public decimal longitudeDelta { get; set; }
             public string? eczanetelefon { get; set; }
             public string? eczaneismi { get; set; }
             public string? eczaneadress { get; set; }
        }


        [Keyless]
        public class Favori
        {
            public int userid { get; set; }
            public int productid { get; set; }
        }
    }
}
