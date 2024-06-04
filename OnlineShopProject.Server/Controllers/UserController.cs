using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;



namespace OnlineShopProject.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {

        private readonly IUserRepository _UserRepository;
        private readonly DataContext _DataContext;
        private readonly ICartRepository _CartRepository;
        private readonly ICartItemRepository _CartItemRepository;


        public UserController(IUserRepository userRepository, DataContext context, ICartItemRepository cartItemRepository, ICartRepository cartrepository)
        {
            _UserRepository = userRepository;
            _DataContext = context;
            _CartItemRepository = cartItemRepository;
            _CartRepository = cartrepository;

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

        [HttpGet("username/{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]

        public IActionResult GetUserByUsername(string username)
        {
            if (!_UserRepository.UserExists(username))
            {
                return NotFound();
            }

            var User = _UserRepository.GetUserByUsername(username);

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

            NewUser.Password = _UserRepository.EncryptPassword(NewUser.Password);


            if(NewUser.Phone_number.ToString().Length != 9 && NewUser.Phone_number >= 0)
            {
                ModelState.AddModelError("", "phone number doesn't have 9 digits" + NewUser.Phone_number.ToString());
                return StatusCode(422, ModelState);
            }

            if (!_UserRepository.CreateUser(NewUser))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new ApiResponse { Status = 200, Message = "User successfully created"});
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

        [HttpPut("password/{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult ChangeUserPassword(int UserId, [FromBody] string Password)
        {
            if (Password is null)
                return BadRequest(ModelState);


            if (!_UserRepository.UserExists(UserId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_UserRepository.ChangePassword(UserId, Password))
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

            Cart CartToDelete = _DataContext.Carts.Where(c => c.UserId == UserId).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(CartToDelete != null)
            {
                if (!_CartRepository.DeleteCart(CartToDelete))
                {
                    ModelState.AddModelError("", "something went wrong deleting the user");
                }
               
            }

            if (!_UserRepository.DeleteUser(UserToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting the user");
            }

            return NoContent();
        
        }

    }
}
