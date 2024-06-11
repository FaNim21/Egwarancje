using EgwarancjeDbLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarrantyController : ControllerBase
{
    private readonly LocalDatabaseContext _database;


    public WarrantyController(LocalDatabaseContext database)
    {
        _database = database;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<List<Warranty>>> GetAllWarranties()
    {
        List<Warranty> warranties = await _database.Warranties.ToListAsync();
        return Ok(warranties);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult<Warranty>> AddWarranty(CreateWarrantyDto warrantyDto)
    {
        Warranty warranty = new()
        {
            DateOfWarranty = warrantyDto.DateOfWarranty,
            Status = warrantyDto.Status,
            Comments = warrantyDto.Comments,
            OrderId = warrantyDto.OrderId,
        };

        _database.Warranties.Add(warranty);
        await _database.SaveChangesAsync();

        return Ok(warranty);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteWarranty(int id)
    {
        var dbWarranty = await _database.Warranties.FindAsync(id);
        if (dbWarranty is null) return BadRequest($"Warranty with {id} doesn't exists");

        _database.Warranties.Remove(dbWarranty);
        await _database.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("Spec/GetAll")]
    public async Task<ActionResult<List<WarrantySpec>>> GetAllWarrantySpecs()
    {
        List<WarrantySpec> WarrantySpecs = await _database.WarrantiesSpecs.ToListAsync();
        return Ok(WarrantySpecs);
    }

    [HttpPost]
    [Route("Spec/Add")]
    public async Task<ActionResult<WarrantySpec>> AddWarrantySpec(CreateWarrantySpecDto warrantySpecDto)
    {
        WarrantySpec warrantySpec = new()
        {
            WarrantyId = warrantySpecDto.WarrantyId,
            OrderSpecId = warrantySpecDto.OrderSpecId,
            Comments = warrantySpecDto.Comments
        };

        _database.WarrantiesSpecs.Add(warrantySpec);
        await _database.SaveChangesAsync();

        return Ok(warrantySpec);
    }

    [HttpDelete]
    [Route("Spec/Delete")]
    public async Task<IActionResult> DeleteWarrantySpec(int id)
    {
        var dbWarrantySpecs = await _database.WarrantiesSpecs.FindAsync(id);
        if (dbWarrantySpecs is null) return BadRequest($"Warranty spec with {id} doesn't exists");

        _database.WarrantiesSpecs.Remove(dbWarrantySpecs);
        await _database.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("Attachment/GetAll")]
    public async Task<ActionResult<List<Attachment>>> GetAllAttachments()
    {
        List<Attachment> attachments = await _database.Attachments.ToListAsync();
        return Ok(attachments);
    }

    [HttpPost]
    [Route("Attachment/Add")]
    public async Task<ActionResult<WarrantySpec>> AddAttachment(Attachment attachment)
    {
        _database.Attachments.Add(attachment);
        await _database.SaveChangesAsync();

        return Ok(attachment);
    }

    [HttpDelete]
    [Route("Attachment/Delete")]
    public async Task<IActionResult> DeleteAttachment(int id)
    {
        var dbAttachment = await _database.Attachments.FindAsync(id);
        if (dbAttachment is null) return BadRequest($"Attachment with {id} doesn't exists");

        _database.Attachments.Remove(dbAttachment);
        await _database.SaveChangesAsync();

        return Ok();
    }
}
