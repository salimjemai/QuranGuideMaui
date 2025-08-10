using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuranGuide.Maui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TafseerController : ControllerBase
    {
        // GET: api/<TafseerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TafseerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TafseerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TafseerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TafseerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
