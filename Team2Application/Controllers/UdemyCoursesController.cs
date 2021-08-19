using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team2Application.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Team2Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UdemyCoursesController : ControllerBase
    {
        // GET: api/<UdemyCoursesController>
        [HttpGet]
        public IEnumerable<UdemyCoursesRecord> Get()
        {
            var client = new RestClient($"https://www.udemy.com/api-2.0/courses/?search=C#");
            client.Authenticator = new HttpBasicAuthenticator("CkaIqUMDHO4Dp96Xc2z1Lwg9BcwS3etRvtHHuGUE", "0iS2boCGNqVoTap046T1r9UzJsVMXxxu4WOwTQDhWpaGrnZCRrwFSlL7YraegarBLM5Qcwq5bm9tAnVRQ2Yh60OExsVZRdXnVrwDub26yLdO0If4ieZ9sBWDmajn7Qq4");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return this.ConvertResponseToCourseRecord(response.Content);
        }

        [NonAction]
        public IEnumerable<UdemyCoursesRecord> ConvertResponseToCourseRecord(string content)
        {
            var json = JObject.Parse(content);
            if (json["results"] == null)
            {
                throw new Exception("Apikey not valid.");
            }

            var jsonArray = json["results"].Take(7);
            return jsonArray.Select(CreatingUdemyCoursesRecordFromJToken);
        }

        private UdemyCoursesRecord CreatingUdemyCoursesRecordFromJToken(JToken item)
        {
            string courseTitle = (string)item.SelectToken("title");
            string description = (string)item.SelectToken("headline");
            string specificUrl = (string)item.SelectToken("url");
            string clickableUrl = $"https://www.udemy.com{specificUrl}";

            UdemyCoursesRecord udemyCoursesRecord = new UdemyCoursesRecord(courseTitle, clickableUrl, description);
            return udemyCoursesRecord;
        }

        // GET api/<UdemyCoursesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UdemyCoursesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UdemyCoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UdemyCoursesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
