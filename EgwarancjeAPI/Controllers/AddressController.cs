using EgwarancjeAPI.Services.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMessagesService _messagesService;
        private readonly LocalDatabaseContext _database;

        public AddressController(IMessagesService messagesService, LocalDatabaseContext database)
        {
            _database = database;
            _messagesService = messagesService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task <ActionResult<List<Address>>> GetAllAdresses()
        {
            var addresses = await _database.Addresses.ToListAsync();

            return Ok(addresses);
        }

        [HttpPost]
        [Route("Add")]
        public async Task <ActionResult<Address>> AddNewAddress(Address address)
        {
            _database.Addresses.Add(address);
            await _database.SaveChangesAsync();

            return Ok(address);
        }

        [HttpPut]
        [Route("Update")]
        public async Task <IActionResult> UpdateAddress(Address address)
        {
            var dbAddress = await _database.Addresses.FindAsync(address.Id);
            if (dbAddress is null) return BadRequest("Address not found.");

            dbAddress.Street = address.Street;
            dbAddress.City = address.City;
            dbAddress.Number = address.Number;
            dbAddress.ZipCode = address.ZipCode;
            dbAddress.Country = address.Country;
            dbAddress.Province = address.Province;

            await _database.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<List<Address>>> DeleteAddress(int id)
        {
            var dbAddress = await _database.Addresses.FindAsync(id);
            if (dbAddress is null) return BadRequest("Address not found.");

            _database.Addresses.Remove(dbAddress);
            await _database.SaveChangesAsync();

            return Ok(await _database.Addresses.ToListAsync());
        }
    }
}
