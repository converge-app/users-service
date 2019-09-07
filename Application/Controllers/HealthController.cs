using Application.Database;
using Application.Models;
using Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        private readonly IDatabaseContext dbContext;
        private readonly IModelRepository modelRepository;

        public HealthController(IDatabaseContext dbContext, IModelRepository modelRepository)
        {
            this.dbContext = dbContext;
            this.modelRepository = modelRepository;
        }

        // GET api/health/ping
        [Produces("application/json")]
        [HttpGet("ping")]
        public ActionResult Ping()
        {
            var model = new Model();

            var modelOut = modelRepository.Create(model);

            return Ok(modelOut);
        }
    }
}