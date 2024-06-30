using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Groups;
using Service.DTOs.Students;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseApi.Controllers.Admin
{
   
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IGroupService groupService,
                              ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get All method is working");
            var data = await _groupService.GetAllWithAsync();
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            _logger.LogInformation("Search method is working");
            var data = await _groupService.GetByName(name);
            return Ok(data);

        }

        [HttpGet]
        public async Task<IActionResult> SortByName([FromQuery] string message)
        {
            _logger.LogInformation("Sort method is working");
            var data = await _groupService.SortByName(message);
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> SortByCapacity([FromQuery] string message)
        {
            _logger.LogInformation("Sort method is working");
            var data = await _groupService.SortByCapacity(message);
            return Ok(data);

        }
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] GroupCreateDto request)
        {
            
            await _groupService.CreateAsync(request);
            
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _groupService.GetByIdAsync(id));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();
            await _groupService.DeleteAsync((int)id);

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] GroupEditDto request)
        {
            await _groupService.EditAsync(id, request);
            return Ok();
        }

    }
}
