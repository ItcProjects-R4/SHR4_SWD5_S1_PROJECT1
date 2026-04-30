using Core.DTO;
using Core.IReprosatory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepo _itemRepo;

        public ItemController(IItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [Authorize]
        [HttpGet("getAllItem")]
        public IActionResult GetAllItem() 
        {
            
                var items = _itemRepo.GetAllItems();
                if (items == null || items.Count == 0)
                {
                    return NotFound("No items found.");
            }
            return Ok(items);
           
        }

        [Authorize]
        [HttpGet("getItemById/{id}")]
        public IActionResult GetItem([FromRoute] int id) { 

            var item = _itemRepo.GetItemById(id);
            if (item == null)
            {
                return NotFound("Item not found.");
            }
            return Ok(item);
        }

        [Authorize]
        [HttpGet("sortItems")]
        public IActionResult GetItemsByCountryAndCategory([FromQuery] int? countryId, [FromQuery] int? categoryId)
        {
            var items = _itemRepo.GetItensByCountryAndCategory(countryId, categoryId);
            if (items == null || items.Count == 0)
            {
                return NotFound("No items found for the specified country and category.");
            }
            return Ok(items);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("addItem")]
        public IActionResult AddItem([FromBody] ItemD item)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try { 
                
                _itemRepo.AddItem(item);
                return Ok("Item added successfully.");
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
         
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("updateItem/{id}")]
        public IActionResult UpdateItem([FromBody] ItemD item, [FromRoute] int id)
        {
            var existingItem = _itemRepo.GetItemById(id);
            if (existingItem == null)
            {
                return NotFound("Item not found.");
            }
            _itemRepo.UpdateItem( item , id);
            return Ok("Item updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("removeItem")]
        public IActionResult RemoveItem([FromRoute] int id)
        {
            var existingItem = _itemRepo.GetItemById(id);
            if (existingItem == null)
            {
                return NotFound("Item not found.");
            }
            _itemRepo.RemoveItem(id);
            return Ok("Item removed successfully.");
        }

    }
}
