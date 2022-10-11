using Microsoft.AspNetCore.Mvc;
using Catalog.Entities;
using System.Collections.Generic;
using Catalog.Repositories;
using System;
using Catalog.Dtos;
using System.Linq;
using System.Threading.Tasks;

// namespaces are similar to java packages.
namespace Catalog.Controllers
{
  // telling that the class is a controller with a Route "items".
  [ApiController]
  [Route("items")]
  public class ItemsController: ControllerBase {
    private readonly IItemsRepository repository;  
    
    // injected via Dependency injection.
    public ItemsController(IItemsRepository repository) {
      this.repository = repository;
    }

    // The following notation is an annotation in C#
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync() {
      var items = (await repository.GetItemsAsync()).Select((item) => item.AsDto());
      return items;
    }
    
    /*
    ActionResult is the return type of the functions Ok, NotFound and other Http methods.
    It takes a generic which tells that the method either returns ActionResult or the 
    Type passed as a generic
    */
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id) {
      var item = await repository.GetItemAsync(id);
      
      if (item is null) {
        return NotFound();
      }
      
      return Ok(item.AsDto());
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto receivedItem) {
      Item item = new() {
        Id = Guid.NewGuid(),
        Name = receivedItem.Name,
        Price = receivedItem.Price,
        CreateDate = DateTimeOffset.UtcNow,
      };
      await repository.CreateItemAsync(item);

      return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
    } 


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto receivedItem) {
      var existingItem = await repository.GetItemAsync(id);
      if (existingItem == null) {
        return NotFound();
      }

      Item updatedItem = existingItem with {
        Name = receivedItem.Name,
        Price = receivedItem.Price,
      };

      await repository.UpdateItemAsync(updatedItem);
      return NoContent();
    } 
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id) {
      var existingItem = await repository.GetItemAsync(id);

      if (existingItem is null) {
        return NotFound();
      }

      await repository.DeleteItemAsync(id);
      return NoContent();
    }
  }
}
