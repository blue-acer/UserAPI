using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [EnableCors("_allowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly User_DBContext _context;

        public UserDetailsController(User_DBContext context)
        {
            _context = context;
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUserDetails()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4000");
            var results = await _context.UserDetails.ToListAsync();
            return Ok(results);
        }

        // GET: api/UserDetails
        [HttpGet("sp")]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUserDetailsSP()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4000");

            var results = await _context.UserDetails.FromSqlRaw("sp_get_all_valid_records_aoife").ToListAsync();
            return Ok(results);
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserDetails(long id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4000");

            var userDetails = await _context.UserDetails.FindAsync(id);

            if (userDetails == null)
            {
                return NotFound();
            }

            return Ok(userDetails);
        }

        [HttpPut("delete/{id}")]
        public async Task<ActionResult<int>> DeleteUser(long id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4000");

            var results = await _context.Database.ExecuteSqlRawAsync($"sp_change_status_code_aoife {id}");
            
            return Ok(results);
        }



        [HttpPost("addrecord")]
        public async Task<ActionResult<int>> AddUser(String fName, String lName, String email)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4000");

            var results = await _context.Database.ExecuteSqlRawAsync($"sp_insert_new_record_aoife '{fName}', '{lName}', '{email}'");
            return Ok(results);
        }

        private bool UserDetailsExists(long id)
        {
            return _context.UserDetails.Any(e => e.UserId == id);
        }
    }
}
