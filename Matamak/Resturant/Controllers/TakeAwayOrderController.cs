using Core.DTO;
using Core.IReprosatory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Resturant.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v1/takeaway-orders")]
    [ApiController]
    public class TakeAwayOrderController : ControllerBase
    {
        private readonly ITakeAwayOrderRepo takeAwayOrderRepo;

        public TakeAwayOrderController(ITakeAwayOrderRepo takeAwayOrderRepo)
        {
            this.takeAwayOrderRepo = takeAwayOrderRepo;
        }

        [Authorize(Roles = "Admin,Cashier")]
        [HttpGet]
        [HttpGet("getAllTakeAwayOrders")]
        public IActionResult GetAll()
        {
            return Ok(takeAwayOrderRepo.GetAllTakeAwayOrders());
        }

        [Authorize(Roles = "Admin,Cashier,Customer")]
        [HttpGet("{id}")]
        [HttpGet("getTakeAwayOrder/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(takeAwayOrderRepo.GetTakeAwayOrder(id));
        }

        [Authorize(Roles = "Customer,Cashier")]
        [HttpPost]
        [HttpPost("addTakeAwayOrder")]
        public IActionResult Add([FromBody] TakeAwayD order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            takeAwayOrderRepo.AddTakeAwayOrder(order);
            return Ok("Takeaway order created successfully.");
        }

        [Authorize(Roles = "Customer,Cashier")]
        [HttpPut("{id}")]
        [HttpPut("updateTakeAwayOrder/{id}")]
        public IActionResult Update(int id, [FromBody] TakeAwayD order)
        {
            takeAwayOrderRepo.UpdateTakeAwayOrder(order, id);
            return Ok("Takeaway order updated successfully.");
        }

        [Authorize(Roles = "Admin,Cashier")]
        [HttpDelete("{id}")]
        [HttpDelete("removeTakeAwayOrder/{id}")]
        public IActionResult Delete(int id)
        {
            takeAwayOrderRepo.RemoveTakeAwayOrder(id);
            return Ok("Takeaway order removed successfully.");
        }
    }
}
