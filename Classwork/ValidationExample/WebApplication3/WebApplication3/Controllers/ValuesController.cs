using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IValidator<User> validator;

        public ValuesController(IValidator<User> validator)
        {
            this.validator = validator;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            throw new ValidationException("alskjakjdhaks");
            return Ok(new User { Age = 0, Email = "aaa", FirstName = "asas" });
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]User value)
        {
            var result = validator.Validate(value);
            if (!result.IsValid) throw new ValidationException(
                result.Errors.Select(x => x.ErrorMessage)
                .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}")
                );

            return Ok();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}