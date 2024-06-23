using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using OnlineShopProject.Server.Repository;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.Server.Controllers
{


    [Route("api/[controller]")]
    [ApiController]

    public class RoleController : Controller
    {

        private readonly IRoleRepository _RoleRepository;

        private readonly IUserRepository _UserRepository;

        private readonly DataContext _Context;

        public RoleController(IRoleRepository roleRepository, DataContext dataContext, IUserRepository userRepository)
        {

            _RoleRepository = roleRepository;
            _Context = dataContext;
            _UserRepository = userRepository;

        }

        [HttpGet("UserRole")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserRole>))]

        public IActionResult GetUserRoles()
        {
            var UserRoles = _RoleRepository.GetUserRoles();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(UserRoles);
        }

        [HttpGet("UserRole/{UserId}")]
        [ProducesResponseType(200, Type = typeof(UserRole))]
        [ProducesResponseType(400)]

        public IActionResult GetUserRole(int UserId)
        {

            if(!_UserRepository.UserExists(UserId))
            {
                return NotFound();
            }

            var UserRole = _RoleRepository.GetUserRole(UserId);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(UserRole);
        }


        [HttpGet("{RoleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]

        public IActionResult GetRole(int RoleId)
        {
            var Role = _RoleRepository.GetRole(RoleId);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Role);
        }


        [HttpPost("{UserId}")]
        [ProducesResponseType(200, Type = typeof(UserRole))]

        public IActionResult CreateUserRole(int UserId, [FromBody][Required] int RoleId)
        {

            if (!_UserRepository.UserExists(UserId))
            {
                return NotFound("user not founded");
            }

            if (!_RoleRepository.RoleExists(RoleId))
            {
                return NotFound("role not founded");
            }

            var UserRole = _Context.UserRoles.Where(ur => ur.UserId == UserId).FirstOrDefault();

            if(UserRole != null)
            {
                return BadRequest("User already has a role");
            }

            UserRole role = new UserRole()
            {
                RoleId = RoleId,
                UserId = UserId
            };


            _RoleRepository.CreateUserRole(role);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(role);
        }


        [HttpDelete("{UserId}")]
        [ProducesResponseType(200, Type = typeof(Role))]

        public IActionResult DeleteUserRole(int UserId)
        {

            if (!_UserRepository.UserExists(UserId))
            {
                return NotFound("user not founded");
            }

            var RoleToDelete = _Context.UserRoles.Where(ur => ur.UserId == UserId).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (RoleToDelete != null)
            {

                if (!_RoleRepository.DeleteUserRole(UserId))
                {
                    return BadRequest("error deleting the role");
                } else
                {
                    return Ok(RoleToDelete);
                }
            } else
            {
                return NotFound("user doesn't have a role");
            }




          
        }


    }
}
