using Microsoft.AspNetCore.Mvc;
using NewProject.Database;
using NewProject.Services;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using VSPWebApi.API.Database.Models;
using VSPWebApi.API.Database.Models.eczane_tracker;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using Npgsql;
using System.Net;
//using System.Web.Security;

namespace NewProject.API.Controllers
{
    [Route("/api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly JwtService _jwtService;

        public ProductController(DataContext dbContext, JwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }
        [HttpGet("getAllocation")]
        public IActionResult getAllocation()
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.eczanekonumlari.ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getAllocationWithId")]
        public IActionResult getAllocation(int eczaneid)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.eczanekonumlari.First(item => item.eczaneid == eczaneid);
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getAllProducts")]
        public IActionResult getAllProducts()
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.mytable.FromSqlRaw("SELECT * FROM mytable").ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getAllOrdersKurye")]
        public IActionResult getAllOrdersKurye()
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.kuryenavigasyon.ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getAllProductsWpf")]
        public IActionResult getAllProductsWpf()
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.mytable
                        .Select(x => new MyTableToWpf { 
                            author = x.author,
                            description = x.description,
                            id = x.id,
                            image1 = x.image1,
                            name = x.name
                        })
                        .ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getEczane")]
        public IActionResult getEczane(int id)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.eczanevestokbilgisi.ToList();
                    getFromDb = getFromDb.Where(e => e.ilacvestok.Any(i => i.id == id && i.count > 0)).ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("updateAndGetStock")]
        public IActionResult updateAndGetStock(updateStockDto x)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.eczanevestokbilgisi.First(item => item.eczaneid == x.eczaneid);
                    if (x.isupdate == 1)
                    {
                        if (getFromDb != null)
                        {
                            var temp = getFromDb.ilacvestok;
                            for (int i = 0; i < temp.Count; i++)
                            {
                                if (temp[i].id == x.urunid)
                                {
                                    temp[i].count = x.urunstok;
                                }
                            }
                            getFromDb.ilacvestok = new List<StokBilgisi>(temp);
                            db.SaveChanges();
                        }                   
                    }
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                    //for (int kk = 0; kk < getFromDb.Count; kk++)
                    //{
                    //    if (getFromDb[kk].eczaneid == 123)
                    //    {
                    //        for (int i = 0; i < getFromDb[kk].ilacvestok.Count; i++)
                    //        {
                    //            if (getFromDb[kk].ilacvestok[i].id == 66)
                    //            {
                    //                getFromDb[kk].ilacvestok[i].count = 0;
                    //                break;
                    //            }
                    //        }
                    //    }

                    //}
                    //Console.WriteLine();              
                    //foreach (var item in getFromDb.First(x => x.eczaneid == 123).ilacvestok)
                    //{
                    //    if (item.id == 66)
                    //    {
                    //        item.id = 0;

                    //    }
                    //}
                    //getFromDb = getFromDb.Where(e => e.ilacvestok.Any(i => i.id == id && i.count > 0)).ToList();
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost("updatePassword")]
        public IActionResult updatePassword(updatePassDTO x)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.users.First(item => item.email == x.email && item.id == x.userid && item.password == x.oldPass);
                    if (getFromDb != null)
                    {
                        getFromDb.password = x.newPass;
                        db.SaveChanges();
                        return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                    }
                    else
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("addNewProduct")]
        public async Task<IActionResult> AddNewProduct(Root data)
        {
            using (var db = new DataContext())
            {

                try
                {
                    db.mytable.AddAsync(data);
                    await db.SaveChangesAsync();

                    var response = new returnClassModel()
                    {
                        message = "Islem Basarili Bir Sekilde Gerceklesti",
                        response = "Urun Basarili Bir Sekilde Eklendi",
                        status_code = Ok().StatusCode.ToString(),
                    };
                    return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

        }
        [HttpPost("addNewKurye")]
        public async Task<IActionResult> addNewKurye(KuryeNavigasyon data)
        {
            using (var db = new DataContext())
            {

                try
                {
                    db.kuryenavigasyon.AddAsync(data);
                    await db.SaveChangesAsync();

                    var response = new returnClassModel()
                    {
                        message = "Islem Basarili Bir Sekilde Gerceklesti",
                        response = "Urun Basarili Bir Sekilde Eklendi",
                        status_code = Ok().StatusCode.ToString(),
                    };
                    return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

        }

        [HttpPost("addNewUsers")]
        public async Task<IActionResult> AddNewUsers(UserLogin data)
        {
            using (var db = new DataContext())
            {

                try
                {
                    db.users.AddAsync(data);
                    await db.SaveChangesAsync();

                    var response = new returnClassModel()
                    {
                        message = "Islem Basarili Bir Sekilde Gerceklesti",
                        response = "Urun Basarili Bir Sekilde Eklendi",
                        status_code = Ok().StatusCode.ToString(),
                    };
                    return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

        }

        [HttpPost("makeOrder")]
        public async Task<IActionResult> makeOrder(Orders data)
        {
            using (var db = new DataContext())
            {

                try
                {
                    db.orders.AddAsync(data);
                    await db.SaveChangesAsync();

                    var response = new returnClassModel()
                    {
                        message = "Islem Basarili Bir Sekilde Gerceklesti",
                        response = "Urun Basarili Bir Sekilde Eklendi",
                        status_code = Ok().StatusCode.ToString(),
                    };
                    return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException);
                }

            }

        }

        [HttpPost("postLogin")]
        public async Task<IActionResult> postLogin(UserLoginDTO data)
        {
            using (var db = new DataContext())
            {

                try
                {
                    var getFromDb = db.users.Where(item => item.password == data.password && item.email==data.email).ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException);
                }

            }

        }

        [HttpPost("postLoginPharmacy")]
        public async Task<IActionResult> postLoginPharmacy(UserLoginWpfDTO data)
        {
            using (var db = new DataContext())
            {

                try
                {
                    var getFromDb = db.pharmacists.Where(item => item.eczanesifre == data.password && item.eczaneid == data.id).ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException);
                }

            }

        }

        [HttpGet("getOrders")]
        public IActionResult getOrders(int pharmacyid)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.orders.ToList();
                    List<Orders> filteredOrders = new List<Orders>();
                    getFromDb.ForEach(order =>
                    {
                        order.medicines = order.medicines.Where(med => med.pharmacyid == pharmacyid).ToList();
                        if (order.medicines.Count > 0)
                        {
                            filteredOrders.Add(order);
                        }
                    });
                    return Ok(JsonConvert.SerializeObject(filteredOrders, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getOrdersForUsers")]
        public IActionResult getOrdersForUsers(int userid)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.orders.Where(item => item.userid == userid).ToList();
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getInfoKuryeCardFromOrderId")]
        public IActionResult getInfoKuryeCardFromOrderId(int orderid)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.orders.First(item => item.orderid == orderid);
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getFavoritesProducts")]
        public IActionResult getFavoritesProducts(int id)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                  var result = db.mytable
                  .Join(db.favoriler,
                      t1 => t1.id,
                      t2 => t2.productid,
                      (t1, t2) => new { T1 = t1, T2 = t2 })
                  .Where(x => x.T2.userid == id)
                  .ToList();
                   return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("addNewFavoriteProduct")]
        public async Task<IActionResult> AddNewFavoriteProduct(Favori data)
        {
            bool isInDB = false;
            try
            {
                string connectionString = "Server=databasepharmacy.ckugpbwqwvch.eu-central-1.rds.amazonaws.com;Port=5432;Database=pharmacyMain;User ID=squezzoo;Password=burak3845;";
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                //once var mi kontrol etmelisin
                string sqlGet = "SELECT * FROM favoriler WHERE userid=@val1 and productid=@val2 and pharmacyid=@val3;";
                using (NpgsqlCommand command1 = new NpgsqlCommand(sqlGet, connection))
                {
                    command1.Parameters.AddWithValue("@val1", data.userid);
                    command1.Parameters.AddWithValue("@val2", data.productid);
                    command1.Parameters.AddWithValue("@val3", data.pharmacyid);
                    NpgsqlDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        isInDB = true;
                        break;
                    }
                    reader.Close();
                }


                //--------------------------------------------------------------------------------------------------------------
                if (!isInDB)
                {

                    string sql = "INSERT INTO favoriler (userid,productid,pharmacyid) VALUES (@val1,@val2,@val3);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@val1", data.userid);
                    command.Parameters.AddWithValue("@val2", data.productid);
                    command.Parameters.AddWithValue("@val3", data.pharmacyid);

                    command.ExecuteNonQuery();
                    connection.Close();
                    var response = new returnClassModel()
                    {
                        message = "Islem Basarili Bir Sekilde Gerceklesti",
                        response = "Ilac Basarili Bir Sekilde Eklendi",
                        status_code = Ok().StatusCode.ToString(),
                    };
                    return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
                else
                {
                    var response = new returnClassModel()
                    {
                        message = "Ilac Zaten DB de bulunuyor",
                        response = "Ilac Zaten DB de bulunuyor",
                        status_code = NoContent().StatusCode.ToString(),
                    };
                    return NoContent();
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("deleteFavoriteProduct")]
        public async Task<IActionResult> deleteFavoriteProduct(Favori data)
        {

            try
            {
                string connectionString = "Server=databasepharmacy.ckugpbwqwvch.eu-central-1.rds.amazonaws.com;Port=5432;Database=pharmacyMain;User ID=squezzoo;Password=burak3845;";
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                string sql = "DELETE FROM favoriler where userid=@val1 and productid=@val2;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@val1", data.userid);
                command.Parameters.AddWithValue("@val2", data.productid);
                command.ExecuteNonQuery();
                connection.Close();
                var response = new returnClassModel()
                {
                    message = "Islem Basarili Bir Sekilde Gerceklesti",
                    response = "Arac Basarili Bir Sekilde Silindi",
                    status_code = Ok().StatusCode.ToString(),
                };
                return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("deleteAllFavoriteProduct")]
        public async Task<IActionResult> deleteFavoriteProduct(int userid)
        {

            try
            {
                string connectionString = "Server=databasepharmacy.ckugpbwqwvch.eu-central-1.rds.amazonaws.com;Port=5432;Database=pharmacyMain;User ID=squezzoo;Password=burak3845;";
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                string sql = "DELETE FROM favoriler where userid=@val1";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@val1", userid);
                command.ExecuteNonQuery();
                connection.Close();
                var response = new returnClassModel()
                {
                    message = "Islem Basarili Bir Sekilde Gerceklesti",
                    response = "Arac Basarili Bir Sekilde Silindi",
                    status_code = Ok().StatusCode.ToString(),
                };
                return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("getReceteler")]
        public IActionResult getReceteler(RecetelerDTO data)
        {
            try
            {

                using (var db = new DataContext())
                {
                    List<int> vs = new List<int>();
                    var ilaclar = db.receteler.Where(s => s.receteid == data.receteid && s.tcno == data.tcno).ToList();
                    ilaclar.ForEach(item =>
                    {
                        var ilaclarListesi = item.ilaclar.ToList();
                        ilaclarListesi.ForEach(item2 =>
                        {
                            vs.Add(Convert.ToInt32(item2.id));
                        });
                    });
                    var getFromDb = db.mytable.Where(item => vs.Contains(item.id));
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getUserNameFromUserId")]
        public IActionResult getUserNameFromUserId(int userId)
        {
            try
            {

                using (var db = new DataContext())
                {
                    var getFromDb = db.users.First(item => item.id == userId);
                    if (getFromDb != null)
                    {
                        var stringResult = getFromDb.name + " " + getFromDb.surname;
                        return Ok(JsonConvert.SerializeObject(stringResult, Formatting.Indented));
                    }
                    else
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getMedicineNameFromId")]
        public IActionResult getMedicineNameFromId(int id)
        {
            try
            {

                using (var db = new DataContext())
                {
                    var getFromDb = db.mytable.First(item => item.id == id);
                    if (getFromDb != null)
                    {
                        var stringResult = getFromDb.name;
                        return Ok(JsonConvert.SerializeObject(stringResult, Formatting.Indented));
                    }
                    else
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getSendMailPass")]
        public IActionResult getSendMailPass(string email)
        {
            try
            {
                //var token = _jwtService.Verify(Request.Cookies["Authorization"]);
                //if (token.Issuer != Constants.username)
                //{
                //    return Unauthorized();
                //}

                using (var db = new DataContext())
                {
                    var getFromDb = db.users.Any(item => item.email == email);
                    if (getFromDb)
                    {
                        var tempObject = db.users.First(item => item.email == email);
                        string passString = "Merhaba " + tempObject.name+" "+ tempObject.surname +","+"\n\n" +
                        "Şifreniz: " + tempObject.password +
                        "\n\nUygulamaya giriş yaptıktan sonra lütfen şifrenizi değiştiriniz!" +
                        "\n\n\nSağlıklı günler dileriz." +
                        "\n\n\n\ne-CZANEM";
                        Email(passString, tempObject.email);
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        public static void Email(string yazi, string userEmail)
        {
            try
            {
                {
                    var fromAddress = new MailAddress("saglikli.yasama.basla@gmail.com", "e-CZANEM");

                    var toAddress = new MailAddress(userEmail, userEmail);

                    const string fromPassword = "xlmvtklkuclzopbh";

                    const string subject = "Şifre Hatırlatma Maili";

                    string body = yazi;

                    var smtp = new SmtpClient

                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                    };

                    using (var message = new MailMessage(fromAddress, toAddress)

                    {
                        Subject = subject,
                        Body = body
                    })

                    {
                        smtp.Send(message);
                    }

                }

            }
            catch (Exception) { }
        }
    }
    //[HttpGet("getPharmacyNameFromId")]
    //public IActionResult getPharmacyNameFromId(int userId)
    //{
    //    try
    //    {

    //        using (var db = new DataContext())
    //        {
    //            var getFromDb = db.users.First(item => item.id == userId);
    //            if (getFromDb != null)
    //            {
    //                var stringResult = getFromDb.name + " " + getFromDb.surname;
    //                return Ok(JsonConvert.SerializeObject(stringResult, Formatting.Indented));
    //            }
    //            else
    //            {
    //                return NotFound();
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        return BadRequest(ex.Message);
    //    }

    //}
}

