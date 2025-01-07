using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography.X509Certificates;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("set/{name}")]
        public void Set(string name)
        {
            _memoryCache.Set("name",name);

        }
        [HttpGet]
        public string Get()
        {
            if(_memoryCache.TryGetValue<string>("name",out string name))
            {
                return name.Substring(3);
            }
             
            return "";
        }

        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                SlidingExpiration  = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("getDate")]
        public DateTime Date()
        {
            return _memoryCache.Get<DateTime>("date");
        }

    }
}
