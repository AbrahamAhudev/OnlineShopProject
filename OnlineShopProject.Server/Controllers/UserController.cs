using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;



namespace OnlineShopProject.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {

        private readonly IUserRepository _UserRepository;
     

        public UserController(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]

        public IActionResult GetUsers()
        {
            var Users = _UserRepository.GetUsers();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]

        public IActionResult GetUser(int id)
        {
            if (!_UserRepository.UserExists(id))
            {
                return NotFound();
            }

            var User = _UserRepository.GetUserById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(User);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateUser([FromBody] User NewUser)
        {
            if (NewUser == null)
            {
                return BadRequest();
            }

            var user = _UserRepository.GetUsers()
                .Where(u => u.Username.ToLower() == NewUser.Username.ToLower()).FirstOrDefault();

            if (user is not null)
            {
                ModelState.AddModelError("", "user already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_UserRepository.CreateUser(NewUser))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("user successfully created");
        }

        [HttpPut("{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateUser(int UserId, [FromBody] User UpdatedUser)
        {
            if (UpdatedUser is null)
                return BadRequest(ModelState);

            if (UserId != UpdatedUser.Id)
                return BadRequest(ModelState);

            if (!_UserRepository.UserExists(UserId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_UserRepository.UpdateUser(UpdatedUser))
            {
                ModelState.AddModelError("", "Something went wrong when saving");

                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteUser(int UserId)
        {
            if (!_UserRepository.UserExists(UserId))
                return NotFound();

            User UserToDelete = _UserRepository.GetUserById(UserId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_UserRepository.DeleteUser(UserToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the user");
            }

            return NoContent();
        
        }

    }
}
