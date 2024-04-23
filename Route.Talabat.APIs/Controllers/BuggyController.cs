using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Infrastructure.GenericRepository.Data;

namespace Route.Talabat.APIs.Controllers
{

    public class BuggyController : _BaseController
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BuggyController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundError()
        {
            return NotFound();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            string s = null;
            int x =s.Length;
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequestError()
        {
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestError(int id)
        {
            return Ok();
        }

        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorizedError()
        {
            return Unauthorized();
        }


    }
}
