using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using University.API.Code.Models.v1_0;
using University.Application.Commands.CreateStudent;

namespace University.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(CreateStudentContract model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }

            var studentId = await _mediator.Send(new CreateStudentCommand(model.Gender, model.LastName, model.FirstName, model.MiddleName, model.UniqueName));

            return Ok(studentId);
        }
    }
}
