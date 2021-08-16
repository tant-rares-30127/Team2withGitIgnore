using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Team2Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UdemyCoursesController : ControllerBase
    {
        // GET: api/<UdemyCoursesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var client = new RestClient($"https://www.udemy.com/api-2.0/courses/238934/?");
            string[] chestie = { "1" };
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return chestie;
            /*return ConvertResponseToCourseRecord(response.Content);*/
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
