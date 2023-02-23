using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Interfaces;
using UserAPI.Models;

namespace UserAPI.Controllers
{

    [EnableCors("_allowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private IUserDetailsRepository _userDetailsRepository;

        public UserDetailsController(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUserDetails()
        {
            var results = await _userDetailsRepository.GetAllUserDetailsData();
            if (results == null)
            {
                return NotFound(results);
            }
            return Ok(results);
        }

        // GET: api/UserDetails
        [HttpGet("sp")]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUserDetailsSP()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4000");

            var results = await _userDetailsRepository.GetAllUserDetailsSPData();
            if (results == null)
            {
                return NotFound(results);
            }
            return Ok(results);
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserDetails(long id)
        {         
            var userDetails = await _userDetailsRepository.GetUserDetailsData(id);

            if (userDetails == null)
            {
                return NotFound();
            }

            return Ok(userDetails);
        }

        // GET: api/UserDetails/5
        [HttpGet("check/email")]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetUserDetailsByEmail(string email)
        {            

            var results = await _userDetailsRepository.GetUserDetailsDataByEmail(email);

            if (results == null)
            {
                return NotFound(results);
            }
            return Ok(results);
        }

        [HttpPut("delete/{id}")]
        public async Task<ActionResult<int>> DeleteUser(long id)
        {            
            var results = await _userDetailsRepository.deleteUserData(id);

            if (results != 1)
            {
                return NotFound("This user was not found on our systems.");
            }
            return Ok(results);
        }


        [HttpPost("addrecord")]
        public async Task<ActionResult<int>> AddUser(String fName, String lName, String email)
        {            
            var results = await _userDetailsRepository.addUserData(fName, lName, email);

            if (results != 1)
            {
                return StatusCode(409, $"User already exists with email address: {email}");
            }
            return Ok(results);
        }

        //private bool UserDetailsExists(long id)
        //{
        //    return _context.UserDetails.Any(e => e.UserId == id);
        //}
    }
}
