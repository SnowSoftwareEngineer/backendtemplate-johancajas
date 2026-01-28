using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Back_EndAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public class TestItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private static readonly List<TestItem> Items = new()
        {
            new TestItem { Id = 1, Name = "Item 1" },
            new TestItem { Id = 2, Name = "Item 2" }
        };

        [HttpGet("items")]
        public ActionResult<List<TestItem>> GetItems()
        {
            return Ok(Items);
        }

        [HttpPost("items")]
        public ActionResult<TestItem> CreateItem([FromBody] TestItem item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest("Name is required.");
            }

            var nextId = Items.Any() ? Items.Max(i => i.Id) + 1 : 1;
            var toAdd = new TestItem { Id = nextId, Name = item.Name };
            Items.Add(toAdd);
            return CreatedAtAction(nameof(GetItems), new { id = toAdd.Id }, toAdd);
        }

        [HttpPut("items/{id}")]
        public ActionResult UpdateItem(int id, [FromBody] TestItem item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest("Name is required.");
            }

            var existing = Items.FirstOrDefault(i => i.Id == id);
            if (existing == null) return NotFound();

            existing.Name = item.Name;
            return NoContent();
        }

        [HttpDelete("items/{id}")]
        public ActionResult DeleteItem(int id)
        {
            var existing = Items.FirstOrDefault(i => i.Id == id);
            if (existing == null) return NotFound();

            Items.Remove(existing);
            return NoContent();
        }
    }
}
