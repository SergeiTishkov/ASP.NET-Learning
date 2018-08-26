using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    [RoutePrefix("best/service")]
    public class ValuesController : ApiController
    {
        // GET api/values
        /// <summary>
        /// Get all values.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.NoContent, "Get all existing values", typeof(IEnumerable<string>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "List of values is empty")]
        [SwaggerResponseRemoveDefaults]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("{id:int:min(5)}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [SwaggerRequestExample(typeof(Demo), typeof(DemoProvider))]
        public void Post([FromBody]Demo value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class DemoProvider : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Demo() { Name = "TestData", Created = DateTime.Now.AddDays(-7) };
        }
    }

    public class Demo
    {
        public string Name { get; set; }

        public DateTime Created { get; set; }
    }
}
