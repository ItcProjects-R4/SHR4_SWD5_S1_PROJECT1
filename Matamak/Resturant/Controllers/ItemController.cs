using Core.DTO;
using Core.IReprosatory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Resturant.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v1/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepo itemRepo;

        public ItemController(IItemRepo itemRepo)
        {
            this.itemRepo = itemRepo;
        }

        [HttpGet("getAllItem")]
        [HttpGet]
        public IActionResult GetAllItem()
        {
            var items = itemRepo.GetAllItems();
            if (items == null || items.Count == 0)
            {
                return NotFound("No items found.");
            }

            return Ok(items);
        }

        [HttpGet("getItemById/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetItem([FromRoute] int id)
        {
            var item = itemRepo.GetItemById(id);
            if (item == null)
            {
                return NotFound("Item not found.");
            }

            return Ok(item);
        }

        [HttpGet("sortItems")]
        public IActionResult GetItemsByCountryAndCategory([FromQuery] int? countryId, [FromQuery] int? categoryId)
        {
            var items = itemRepo.GetItensByCountryAndCategory(countryId, categoryId);
            if (items == null || items.Count == 0)
            {
                return NotFound("No items found for the specified country and category.");
            }

            return Ok(items);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string term)
        {
            var items = itemRepo.GetAllItems()
                .Where(i =>
                    (!string.IsNullOrWhiteSpace(i.Name) && i.Name.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(i.Description) && i.Description.Contains(term, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return Ok(items);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addItem")]
        public IActionResult AddItem([FromBody] ItemD item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                itemRepo.AddItem(item);
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
            var existingItem = itemRepo.GetItemById(id);
            if (existingItem == null)
            {
                return NotFound("Item not found.");
            }

            itemRepo.UpdateItem(item, id);
            return Ok("Item updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("removeItem/{id}")]
        [HttpDelete("removeItem")]
        public IActionResult RemoveItem([FromRoute] int id)
        {
            var existingItem = itemRepo.GetItemById(id);
            if (existingItem == null)
            {
                return NotFound("Item not found.");
            }

            itemRepo.RemoveItem(id);
            return Ok("Item removed successfully.");
        }
    }
}
