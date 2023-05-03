using Microsoft.AspNetCore.Mvc;
using NewProject.Database;
using NewProject.Services;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using VSPWebApi.API.Database.Models;
using VSPWebApi.API.Database.Models.eczane_tracker;
using Microsoft.Data.SqlClient;
using System.Linq;
using Npgsql;

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
                    List<int> vs = new List<int>();
                    var favoriIdler = db.favoriler.Where(s => s.userid == id).ToList();
                    favoriIdler.ForEach(item => vs.Add((int)item.productid));
                    var getFromDb = db.mytable.Where(item => vs.Contains(item.id));    
                    return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
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

            try
            {
                string connectionString = "Server=databasepharmacy.ckugpbwqwvch.eu-central-1.rds.amazonaws.com;Port=5432;Database=pharmacyMain;User ID=squezzoo;Password=burak3845;";
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                string sql = "INSERT INTO favoriler (userid,productid) VALUES (@val1,@val2);";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@val1", data.userid);
                command.Parameters.AddWithValue("@val2", data.productid);
                command.ExecuteNonQuery();
                connection.Close();
                var response = new returnClassModel()
                {
                    message = "Islem Basarili Bir Sekilde Gerceklesti",
                    response = "Arac Basarili Bir Sekilde Eklendi",
                    status_code = Ok().StatusCode.ToString(),
                };
                return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));

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

        ////eski projeden kalan kisim ------------------------------------------------------------------
        //[HttpPost("addNewCar")]
        //public IActionResult AddNewCar(carClassDTO data)
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }

        //        using (var db = new DataContext())
        //        {
        //            var pMarka = new SqlParameter("@pMarka", data.Marka);
        //            var pModel = new SqlParameter("@pModel", data.Model);
        //            var pPlaka = new SqlParameter("@pPlaka", data.Plaka);
        //            var pKM = new SqlParameter("@pKM", data.KM);
        //            var pAlisFiyat = new SqlParameter("@pAlisFiyat", data.AlisFiyat);
        //            var pSatisFiyat = new SqlParameter("@pSatisFiyat", data.SatisFiyat);
        //            var pMotorHacmi = new SqlParameter("@pMotorHacmi", data.MotorHacmi);
        //            var addToDb = db.Database.ExecuteSqlRaw("INSERT INTO carsTable (Marka, Model, Plaka, KM, AlisFiyat, SatisFiyat, MotorHacmi)" +
        //                " VALUES (@pMarka,@pModel,@pPlaka,@pKM,@pAlisFiyat,@pSatisFiyat, @pMotorHacmi)", pMarka, pModel, pPlaka, pKM, pAlisFiyat, pSatisFiyat, pMotorHacmi);
        //        }
        //        var response = new returnClassModel()
        //        {
        //            message = "Islem Basarili Bir Sekilde Gerceklesti",
        //            response = "Arac Basarili Bir Sekilde Eklendi",
        //            status_code = Ok().StatusCode.ToString(),
        //        };
        //        return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }

        //}

        //[HttpDelete("deleteGivenCar")]
        //public async Task<ActionResult<carClassModel>> deleteGivenCar(string plaka)
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }

        //        using (var db = new DataContext())
        //        {
        //            var product = await db.carsTable.FindAsync(plaka);
        //            if (product == null)
        //            {
        //                return NotFound();
        //            }
        //            db.carsTable.Remove(product);
        //            await db.SaveChangesAsync();

        //            return product;
        //            //var pPlaka = new SqlParameter("@pPlaka",plaka);
        //            //var addToDb = db.Database.ExecuteSqlRaw("DELETE FROM carsTable WHERE Plaka=@pPlaka",pPlaka);
        //            //if (addToDb > 0)
        //            //{
        //            //    return Ok("Arac Basarili Bir Sekilde Silindi");
        //            //}
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }

        //}
        //[HttpGet("gelAllCars")]
        //public IActionResult getAllCars()
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }

        //        using (var db = new DataContext())
        //        {
        //            var getFromDb = db.carsTable.FromSqlRaw("SELECT * FROM carsTable").ToList();
        //            return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }

        //}

        //[HttpGet("getCar")]
        //public IActionResult getCar(String plaka)
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }
        //        var pPlaka = new SqlParameter("@pPlaka", plaka);
        //        using (var db = new DataContext())
        //        {
        //            var getFromDb = db.carsTable.FromSqlRaw("SELECT * FROM carsTable where Plaka=@pPlaka", pPlaka).ToList();
        //            return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }

        //}

        //[HttpPut("updateCar")]
        //public async Task<IActionResult> putCar(String plaka, carClassModel data)
        //{
        //    var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //    if (token.Issuer != Constants.username)
        //    {
        //        return Unauthorized();
        //    }

        //    if (plaka != data.Plaka)
        //    {
        //        return BadRequest();
        //    }
        //    using (var db = new DataContext())
        //    {
        //        db.Entry(data).State = EntityState.Modified;

        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            var product = await db.carsTable.FindAsync(plaka);
        //            if (product == null)
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return NoContent();
        //    }


        //}

        //[HttpGet("getCarProfit")]
        //public IActionResult getCarProfit(String plaka)
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }
        //        var pPlaka = new SqlParameter("@pPlaka", plaka);
        //        using (var db = new DataContext())
        //        {
        //            var getFromDb = db.carsTable.FromSqlRaw("SELECT * FROM carsTable where Plaka=@pPlaka", pPlaka).FirstOrDefault();
        //            if (getFromDb == null)
        //            {
        //                return NotFound();
        //            }
        //            double alis = Convert.ToDouble(getFromDb.AlisFiyat.ToString());
        //            double satis = Convert.ToDouble(getFromDb.SatisFiyat.ToString());
        //            double kar = satis - alis;
        //            var response = new returnClassModel()
        //            {
        //                message = "Islem Basarili Bir Sekilde Gerceklesti",
        //                response = kar.ToString(),
        //                status_code = Ok().StatusCode.ToString(),
        //            };
        //            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }

        //}

        //[HttpGet("getAracKaskoDegeri")]
        //public IActionResult getKasko(String plaka)
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }
        //        var pPlaka = new SqlParameter("@pPlaka", plaka);
        //        using (var db = new DataContext())
        //        {
        //            var getFromDb = db.carsTable.FromSqlRaw("SELECT * FROM carsTable where Plaka=@pPlaka", pPlaka).FirstOrDefault();
        //            if (getFromDb == null)
        //            {
        //                return NotFound();
        //            }
        //            //double motorHacmi = Convert.ToDouble(getFromDb.MotorHacmi.ToString());
        //            double model = Convert.ToDouble(getFromDb.Model.ToString());
        //            double alisFiyat = Convert.ToDouble(getFromDb.AlisFiyat.ToString());
        //            double kaskoDeger = kaskoHesapla(model, alisFiyat);
        //            var response = new returnClassModel()
        //            {
        //                message = "Islem Basarili Bir Sekilde Gerceklesti",
        //                response = kaskoDeger.ToString(),
        //                status_code = Ok().StatusCode.ToString(),
        //            };
        //            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("getAracTeklif")]
        //public IActionResult getAracTeklif(String plaka)
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }

        //        var pPlaka = new SqlParameter("@pPlaka", plaka);
        //        using (var db = new DataContext())
        //        {
        //            var getFromDb = db.teklifTablosu.FromSqlRaw("SELECT * FROM teklifTablosu where plaka=@pPlaka", pPlaka).ToList();
        //            return Ok(JsonConvert.SerializeObject(getFromDb, Formatting.Indented));
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("getMtvBilgisi")]
        //public IActionResult getMtvBilgisi(String plaka)
        //{
        //    try
        //    {
        //        var token = _jwtService.Verify(Request.Cookies["Authorization"]);
        //        if (token.Issuer != Constants.username)
        //        {
        //            return Unauthorized();
        //        }
        //        var pPlaka = new SqlParameter("@pPlaka", plaka);
        //        using (var db = new DataContext())
        //        {
        //            var getFromDb = db.carsTable.FromSqlRaw("SELECT * FROM carsTable where Plaka=@pPlaka", pPlaka).FirstOrDefault();
        //            if (getFromDb == null)
        //            {
        //                return NotFound();
        //            }
        //            //double motorHacmi = Convert.ToDouble(getFromDb.MotorHacmi.ToString());
        //            double model = Convert.ToDouble(getFromDb.Model.ToString());
        //            double motorHacmi = Convert.ToDouble(getFromDb.MotorHacmi.ToString());
        //            double mtvDeger = mtvHesapla(model, motorHacmi);
        //            var response = new returnClassModel()
        //            {
        //                message = "Islem Basarili Bir Sekilde Gerceklesti",
        //                response = mtvDeger.ToString(),
        //                status_code = Ok().StatusCode.ToString(),
        //            };
        //            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}

        //[ApiExplorerSettings(IgnoreApi = true)]
        //public double kaskoHesapla(double model, double alisFiyat)
        //{
        //    double carpan = 0;
        //    if (model >= 2021)
        //    {
        //        carpan = 0.92;
        //    }
        //    else if (model >= 2010 && model < 2021)
        //    {
        //        carpan = 0.88;
        //    }
        //    else if (model >= 2005 && model < 2010)
        //    {
        //        carpan = 0.80;
        //    }
        //    if (model < 2005)
        //    {
        //        carpan = 0.70;
        //    }
        //    return alisFiyat * carpan;
        //}
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public double mtvHesapla(double model, double motorHacimi)
        //{
        //    double carpan = 0;
        //    if (model >= 2021)
        //    {
        //        carpan = 1;
        //    }
        //    else if (model >= 2010 && model < 2021)
        //    {
        //        carpan = 0.88;
        //    }
        //    else if (model >= 2005 && model < 2010)
        //    {
        //        carpan = 0.80;
        //    }
        //    else if (model < 2005)
        //    {
        //        carpan = 0.70;
        //    }
        //    if (motorHacimi <= 1300)
        //    {
        //        return 2000 * carpan;
        //    }
        //    else if (motorHacimi > 1300 && motorHacimi <= 1600)
        //    {
        //        return 2500 * carpan;
        //    }
        //    else if (motorHacimi > 1300 && motorHacimi <= 1600)
        //    {
        //        return 2500 * carpan;
        //    }
        //    else if (motorHacimi > 1600 && motorHacimi <= 2000)
        //    {
        //        return 3000 * carpan;
        //    }
        //    else if (motorHacimi > 2000 && motorHacimi <= 2500)
        //    {
        //        return 3500 * carpan;
        //    }
        //    else if (motorHacimi > 2500 && motorHacimi <= 3000)
        //    {
        //        return 4000 * carpan;
        //    }
        //    else if (motorHacimi >= 3000)
        //    {
        //        return 5000 * carpan;
        //    }
        //    return 0;
        //}
        ////---------------------------------------------------------------------------



    }
}
