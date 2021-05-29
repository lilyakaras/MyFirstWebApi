using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepository repo;
        public UsersController(IUserRepository r)
        {
            repo = r;
        }
        /// <summary>
        /// Get all Users
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return repo.GetUsers();
        }
        /// <summary>
        /// Get a specific User
        /// </summary>
        /// <param name="id"></param>  
        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            User user =  repo.Get(id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>  
        // POST api/users
        [HttpPost]
        public ActionResult Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            repo.Create(user);
            return Ok(user);
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>  
        // PUT api/users/
        [HttpPut]
        public  ActionResult Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!repo.GetUsers().Any(x => x.Id == user.Id))
            {
                return NotFound();
            }
            repo.Update(user);
            return Ok(user);
        }
        /// <summary>
        /// Deletes a specific User
        /// </summary>
        /// <param name="id"></param>   
        // DELETE api/users/5
        [HttpDelete("{id}")]
        public  ActionResult Delete(int id)
        {
            User user = repo.GetUsers().FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            repo.Delete(id);
            return Ok(user);
        }
    }
}
