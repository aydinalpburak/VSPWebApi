

//using Microsoft.AspNetCore.Mvc;
//using NewProject.API.DTO;
//using NewProject.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Data.SqlClient;
//using NewProject.Database;
//using Newtonsoft.Json;
//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Query.Internal;


//namespace NewProject.API.Controllers
//{
//    [Route("/api/auth")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly JwtService _jwtService;

//        public AuthController(JwtService jwtService)
//        {
//            _jwtService = jwtService;
//        }

//        [HttpPost("login")]
//        public IActionResult Login(LoginDataDto data)
//        {


//            try
//            {

//                using (var db = new DataContext())
//                {
//                    //var p1 = new SqlParameter("@p1", data.Username);
//                    //var p2 = new SqlParameter("@p2", data.Password);
//                    //var orderList = db.loginTable.FromSqlRaw("SELECT * From loginTable Where id=@p1 and pass=@p2",p1,p2).FirstOrDefault(); // connection string buradan bilgi cekecegiz. debug ett
//                    //if (orderList == null)
//                    //{
//                    //    return Unauthorized();
//                    //}

//                    var jwt = _jwtService.Generate(Constants.username);
//                    Response.Cookies.Append("Authorization", jwt/*, new CookieOptions
//                {
//                    HttpOnly = true,
//                }*/ );
//                    return Ok(new
//                    {
//                        auth_token = jwt,
//                    });
//                }


               
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }

//        }

//    }
//}
