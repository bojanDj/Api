using Api.jwt;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api.Controllers
{
    public class AccountController : ApiController
    {
        private UserEntities db = new UserEntities();
        [HttpGet, Route("api/ValidLogin")]
        public HttpResponseMessage ValidLogin(string username, string password)
        {
            try
            {
                User user = db.Users.Find(username);
                if (username == user.username && password == user.password)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(username));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "User and pass are invalid");
                }
            } catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "User doesn't exist.");
            }
        }
        [HttpGet, Route("api/ChechIsValid")]
        [CustomAuthenticationFilter]
        public HttpResponseMessage ChechIsValid()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully Valid");
        }
    }
}
