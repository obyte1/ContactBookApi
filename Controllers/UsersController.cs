using contactBook.Core.IConfiguration;
using contactBook.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contactBook.Controllers
{
    [ApiController]
   // [Route("[controller]")]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //intializing the logger class
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork

            )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //Create User
        [HttpPost()]
        public async Task<IActionResult> CreateUser(User user)
        {
            //check for the validity of the model state
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { user.Id }, user);
            }
            //return BadRequest();
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult>GetItem(Guid Id)
        {
            var user = await _unitOfWork.Users.GetById(Id);
            if(user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();
           
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User user)
        {
            if(id != user.Id)
                return BadRequest();
            await _unitOfWork.Users.UpdateUser(user);
            await _unitOfWork.CompleteAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Users.GetById(id);
            if(item == null)
                return BadRequest();
            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(id);

        }

    }
}
