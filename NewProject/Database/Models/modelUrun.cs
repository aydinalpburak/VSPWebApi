
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

            public string price { get; set; }
            public string isreceteli { get; set; }
        }

        public class EczaneKonumlari
        {
           
            public decimal latitude { get; set; }
            public decimal longitude { get; set; }
            public decimal latitudeDelta { get; set; }
            public decimal longitudeDelta { get; set; }
            public string? eczanetelefon { get; set; }
            public string? eczaneismi { get; set; }
            public string? eczaneadress { get; set; }
            [Key]
            public int? eczaneid { get; set; }

        }


        [Keyless]
        public class Favori
        {
            public int userid { get; set; }
            public int productid { get; set; }
            public int? pharmacyid { get; set; }

        }
        public class RecetelerDTO
        {
            public string receteid { get; set; }
            public string tcno { get; set; }
        }

        public class Receteler
        {
            [Key]
            public string receteid { get; set; }
            public string tcno { get; set; }

            [Column(TypeName = "jsonb")]
            public IList<Ilaclar> ilaclar { get; set; }
        }
        public class Ilaclar
        {
            public int? id { get; set; }
            public int? count { get; set; }
        }
        public class Eczaneler
        {
            [Key]
            public int eczaneid { get; set; }
            public string isim { get; set; }

            [Column(TypeName = "jsonb")]
            public IList<StokBilgisi> ilacvestok { get; set; }
        }

        public class StokBilgisi
        {
            public int? id { get; set; }
            public int? count { get; set; }
        }
        public class Orders
        {
            [Key]
            public long orderid { get; set; }
            public string? adress { get; set; }
            [Column(TypeName = "jsonb")]
            public IList<StokBilgisiSparis> medicines { get; set; }
            public int userid { get; set; }
            public string? ordertype { get; set; }
            public string? date { get; set; }
            public string? status { get; set; }
            public string? phonenumber { get; set; }
            public string? lat { get; set; }
            public string? longt { get; set; }
        }
        public class StokBilgisiSparis
        {
            public int? id { get; set; }
            public int? count { get; set; }
            public string? price { get; set; }
            public int? pharmacyid { get; set; }
        }
        public class UserLogin 
        {
            public string? name { get; set; }
            public string? surname { get; set; }        
            public string? tc { get; set; }
            public string? password { get; set; }
            public string? address { get; set; }
            [Key]
            public int id { get; set; } 
            public string? email { get; set; }
            public string? phonenumber { get; set; }
            public string? hastaliklar { get; set; }
            public string? bdate { get; set; }

            public string? candc { get; set; }



        }
        public class UserLoginDTO
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        public class UserLoginWpfDTO
        {
            public int id { get; set; }
            public string password { get; set; }
        }
        public class MyTableToWpf
        {
            public int id { get; set; }
            public string? name { get; set; }
            public string? description { get; set; }
            public string? author { get; set; }
            public string? image1 { get; set; }
        }
        public class updateStockDto 
        {
            public int eczaneid { get; set; }
            public int urunid { get; set; }
            public int urunstok { get; set; }           
            public int isupdate { get; set; }
        }
        public class updatePassDTO 
        {
            public int userid { get; set; }
            public string oldPass { get; set; }
            public string newPass { get; set; }
            public string email { get; set; }

        }
        public class EczaneLogin
        {
            [Key]
            public int? eczaneid { get; set; }
            public string? eczanesifre { get; set; }

        }
        public class KuryeNavigasyon 
        {

            public int? kuryeid{ get; set; }
            [Key]
            public int? pkeys { get; set; }
            public string? userlong{ get; set; }
            public string? userlat{ get; set; }
            public string? eczanelong { get; set; }
            public string? eczanelat { get; set; }
            public int? orderid { get; set; }
            public int? eczaneid { get; set; }
            public string? teslimdurumu { get; set; }
        }
        public class KuryeLogin
        {
            [Key]
            public int? id { get; set; }
            public string password { get; set; }
        }
    }
}
