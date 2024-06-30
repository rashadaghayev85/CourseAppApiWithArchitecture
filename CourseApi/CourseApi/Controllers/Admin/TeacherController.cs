using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Students;
using Service.DTOs.Teachers;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseApi.Controllers.Admin
{
   
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;
        private readonly IGroupService _groupService;

        public TeacherController(ITeacherService teacherService,
                              ILogger<TeacherController> logger,
                              IGroupService groupService)
        {
            _teacherService = teacherService;
            _logger = logger;
            _groupService = groupService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get All method is working");

            return Ok(await _teacherService.GetAllWithAsync());

        }
        [HttpGet]
        public async Task<IActionResult> SearchByNameOrSurname([FromQuery] string nameOrSurname)
        {
            _logger.LogInformation("Search method is working");
            var data = await _teacherService.GetByNameOrSurname(nameOrSurname);
            return Ok(data);

        }

        [HttpGet]
        public async Task<IActionResult> SortByName([FromQuery] string message)
        {
            _logger.LogInformation("Sort method is working");
            var data = await _teacherService.SortByName(message);
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> SortByAge([FromQuery] string message)
        {
            _logger.LogInformation("Sort method is working");
            var data = await _teacherService.SortByAge(message);
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> SortBySalary([FromQuery] string message)
        {
            _logger.LogInformation("Sort method is working");
            var data = await _teacherService.SortBySalary(message);
            return Ok(data);

        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] TeacherCreateDto request)
        {

            var data = await _groupService.GetById(request.GroupId);
            foreach (var item in data)
            {


                await _teacherService.CreateAsync(request);

                return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });


            }

            return BadRequest(new { response = "Student not created becasue group is full" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _teacherService.GetByIdAsync(id));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();
            await _teacherService.DeleteAsync((int)id);

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] TeacherEditDto request)
        {
            await _teacherService.EditAsync(id, request);
            return Ok();
        }
    }
}
