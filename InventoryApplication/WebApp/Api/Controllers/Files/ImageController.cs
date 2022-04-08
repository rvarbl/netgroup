﻿using App.DAL.EF;
using App.Domain.Inventory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Dto;

namespace WebApp.Api.Controllers.Files;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ImageController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ImageController> _logger;
    private readonly ApplicationUnitOfWork _unitOfWork;

    public ImageController(IConfiguration configuration, ILogger<ImageController> logger,
        ApplicationUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    // GET: api/Image
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Get([FromBody] ImageDto dto)
    {
        _logger.LogWarning(dto.Path);
        if (!System.IO.File.Exists(dto.Path)) return BadRequest();

        var b = System.IO.File.ReadAllBytes(dto.Path); // You can use your own method over here.         
        return File(b, "image/png");
    }

    //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] ImageDto dto)
    {
        //validate file/image and the entity
        //add file to appointed path
        //if successful, add image attribute to the entity with the attribute value of file path.
        if (dto.Image == null || dto.StorageItemId == null)
        {
            return BadRequest();
        }

        var size = dto.Image.Length;
        if (size is < 1 or > 5242880) //5MB
        {
            return BadRequest();
        }

        var user = _userManager.Users.FirstOrDefault(x => x.Email == User.Identity!.Name);
        if (user == null)
        {
            return BadRequest();
        }

        var path = _configuration["StoredFilesPath"] + user.Id;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fullPath = path + "/" + Path.GetRandomFileName() + ".png";
        await using var stream = System.IO.File.Create(fullPath);

        await dto.Image.CopyToAsync(stream);

        if (System.IO.File.Exists(fullPath))
        {
            var attribute = _unitOfWork.Attributes.FirstOrDefault(x => x.AttributeName == "Image");
            var item = _unitOfWork.StorageItems
                .FirstOrDefaultAsync(x => x.Storage.ApplicationUserId == user.Id && x.Id == dto.StorageItemId)
                .Result;

            if (attribute == null)
            {
                return BadRequest("no storage");
            }

            if (item == null)
            {
                return BadRequest("no item");
            }

            var itemAttribute = new AttributeInItem()
            {
                ItemAttribute = attribute,
                StorageItem = item,
                AttributeValue = fullPath
            };
            _unitOfWork.AttributesInItem.Add(itemAttribute);
            await _unitOfWork.SaveChangesAsync();


            return Ok();
        }


        return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] ImageDto dto)
    {
        if (System.IO.File.Exists(dto.Path))
        {
            System.IO.File.Delete(dto.Path);
        }

        if (!System.IO.File.Exists(dto.Path))
        {
            return Ok();
        }

        return Problem();
    }
}