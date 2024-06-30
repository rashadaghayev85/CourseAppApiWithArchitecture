using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Students;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseApi.Controllers.Admin
{

    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        private readonly IGroupService _groupService; 

        public StudentController(IStudentService studentService,
                              ILogger<StudentController> logger,
                              IGroupService groupService)
        {
            _studentService = studentService;
            _logger = logger;
            _groupService = groupService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get All method is working");

            return Ok(await _studentService.GetAllWithAsync());

        }
        [HttpGet]
        public async Task<IActionResult> SearchByNameOrSurname([FromQuery] string nameOrSurname)
        {
            _logger.LogInformation("Search method is working");
            var data = await _studentService.GetByNameOrSurname(nameOrSurname);
            return Ok(data);

        }
        [HttpGet]
        public async Task<IActionResult> FilterBy([FromQuery] string? name,string? surname,int? age)
        {
            _logger.LogInformation("Filter method is working");
            var data = await _studentService.FilterBy(name,surname,age);
            return Ok(data);

        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] StudentCreateDto request)
        {

           var data=await _groupService.GetById(request.GroupId);
           foreach (var item in data)
            {

           
            await _studentService.CreateAsync(request);
                  
                    return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });

           
            }

            return BadRequest(new {response="Student not created becasue group is full"});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _studentService.GetByIdAsync(id));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();
            await _studentService.DeleteAsync((int)id);

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] StudentEditDto request)
        {
            await _studentService.EditAsync(id, request);
            return Ok();
        }
       


    }
}
