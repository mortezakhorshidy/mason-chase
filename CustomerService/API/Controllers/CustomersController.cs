using Application.CommandHandlers;
using Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly CreateCustomerCommandHandler _createHandler;
        private readonly UpdateCustomerCommandHandler _updateHandler;
        private readonly DeleteCustomerCommandHandler _deleteHandler;

        public CustomersController(
            CreateCustomerCommandHandler createHandler,
            UpdateCustomerCommandHandler updateHandler,
            DeleteCustomerCommandHandler deleteHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var id = await _createHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerCommand command)
        {
            await _updateHandler.Handle(command with { CustomerId = id });
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteHandler.Handle(new DeleteCustomerCommand(id));
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok();
        }
    }
}
