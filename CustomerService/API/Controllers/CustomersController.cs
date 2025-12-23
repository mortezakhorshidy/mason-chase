using Application.CommandHandlers;
using Application.Commands;
using Application.ModelViews;
using Domain.Aggregates;
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
        private readonly GetAllCustomerQueryHandler _getAllHandler;

        public CustomersController(
            CreateCustomerCommandHandler createHandler,
            UpdateCustomerCommandHandler updateHandler,
            DeleteCustomerCommandHandler deleteHandler,
            GetAllCustomerQueryHandler getAllHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var Customer = await _createHandler.Handle(command);
            return Ok(new ResultModelView()
            {
                Result = ResultTypeEnum.Success,
                Model = Customer.DomainEvents.FirstOrDefault()
            });
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var AllCustomer = await _getAllHandler.Handle();
            return Ok(new ResultModelView()
            {
                Result = ResultTypeEnum.Success,
                Model = AllCustomer,
            });
        }
    }
}
