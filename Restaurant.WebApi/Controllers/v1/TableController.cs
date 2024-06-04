using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.ViewModels.Order;
using Restaurant.Core.Application.ViewModels.Table;
using Restaurant.WebApi.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurant.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TableController : BaseApiController
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("Create")]
        [SwaggerOperation(
            Summary = "Create a new table",
             Description = "Create a new table with details provided in the request body. \nThe status by default is available. The status could be one of the following values:\n\n" +
                  "- 1: Available\n" +
                  "- 2: InProcessOfService\n" +
                  "- 3: Served\n" 
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveTableViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveTableViewModel vm)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _tableService.Add(vm);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("Update/{id}")]
        [SwaggerOperation(
            Summary = "Update a table",
             Description = "Update a table with details provided in the request body."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTableViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateTableViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _tableService.UpdateByUpdateVm(vm, id);
                return Ok(vm);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Waiter,SuperAdmin")]
        [HttpGet("List")]
        [SwaggerOperation(
            Summary = "List all tables",
             Description = "Get all tables created."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tables = await _tableService.GetAll();

                if(tables == null || tables.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(tables);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Waiter,SuperAdmin")]
        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
            Summary = "Get a table",
             Description = "Get a table by its id."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var table = await _tableService.GetById(id);

                if(table == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(table);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Waiter,SuperAdmin")]
        [HttpGet("GetTableOrders")]
        [SwaggerOperation(
            Summary = "List of all orders in process from a table",
             Description = "Get all orders that are in process from the table."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTableOrders(int tableId)
        {
            try
            {
                var orders = await _tableService.GetTableOrder(tableId);

                if(orders == null || orders.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(orders);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Waiter,SuperAdmin")]
        [HttpPut("ChangeStatus")]
        [SwaggerOperation(
            Summary = "Change the status of a table",
             Description = "The status could be one of the following values:\n\n" +
                  "- 1: Available\n" +
                  "- 2: InProcessOfService\n" +
                  "- 3: Served\n"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromQuery]int id, [FromQuery]int status)
        {
            try
            {
                if(await _tableService.ChangeStatus(id, status))
                {
                    await _tableService.ChangeStatus(id, status);
                }
                
                return NoContent();
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
