using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KubeApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private MongoClient mongo;  // = new MongoClient("mongodb://mongo:27017");

        public MongoController() {

             mongo = new MongoClient("mongodb://mongo:27017");


        }



        // GET: api/<MongoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
             
            return new string[] { "value1", "value2" };
        }

        // GET api/<MongoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MongoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MongoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MongoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
