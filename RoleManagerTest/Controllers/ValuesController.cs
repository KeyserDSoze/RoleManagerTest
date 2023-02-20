using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryFramework;
using RoleManagerTest.Domain;

namespace RoleManagerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRepository<ServiceRegistry, string> _serviceRepository;
        private readonly IRepository<RoleForUser, string> _repository;
        private const string DefaultKey = "default";
        public ValuesController(IRepository<ServiceRegistry, string> serviceRepository,
            IRepository<RoleForUser, string> repository)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
        }
        [HttpGet]
        [Route("/{key:alpha}")]
        public async Task<IActionResult> DoSomethingAsync([FromRoute] string key)
        {
            if (key != null)
            {
                var serviceRegistry = await _serviceRepository.GetAsync(DefaultKey).NoContext();
                var service = serviceRegistry.Services.FirstOrDefault(x => x.Id == key);
                if (service != null)
                {
                    return Ok(service);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("/{serviceId:alpha}")]
        [Authorize]
        public async Task<IActionResult> GetRolesAsync([FromRoute] string serviceId)
        {
            var userId = HttpContext.User.Identity?.Name;
            if (userId != null)
            {
                var user = await _repository.GetAsync(userId).NoContext();
                if (user != null)
                {
                    var roles = user.Roles.Where(x => x.ServiceId == serviceId).ToList();
                    return Ok(roles);
                }
            }
            return BadRequest();
        }
    }
}
