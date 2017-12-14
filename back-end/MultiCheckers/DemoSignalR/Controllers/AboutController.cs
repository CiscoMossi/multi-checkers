using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace MultiCheckers.Api.Controllers
{
    [RoutePrefix("api/about")]
    public class AboutController : ApiController
    {
        static string ApiVersion;

        static AboutController()
        {
            ApiVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        [HttpGet]
        [Route("Version")]
        public IHttpActionResult GetVersion()
        {
            return Ok(ApiVersion);
        }

        [HttpGet]
        [Route("ServerName")]
        public IHttpActionResult GetServerName()
        {
            return Ok(Environment.MachineName);
        }
    }
}
