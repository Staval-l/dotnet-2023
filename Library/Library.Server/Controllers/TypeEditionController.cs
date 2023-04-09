﻿using Library.Domain;
using Library.Server.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Library.Server.Controllers;
/// <summary>
/// TypeEdition controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TypeEditionController : ControllerBase
{
    /// <summary>
    /// Used to store logger
    /// </summary>
    private readonly ILogger<TypeEditionController> _logger;
    /// <summary>
    /// Used to store repository
    /// </summary>
    private readonly ILibraryRepository _librariesRepository;
    /// <summary>
    /// TypeEdition controller's constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="librariesRepository"></param>
    public TypeEditionController(ILogger<TypeEditionController> logger, ILibraryRepository librariesRepository)
    {
        _logger = logger;
        _librariesRepository = librariesRepository;
    }
    /// <summary>
    /// Return list of all types of books
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<TypeEdition> Get()
    {
        return _librariesRepository.BookTypes;
    }
    /// <summary>
    /// Return info about type by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<TypeEdition> Get(int id)
    {
        var bookType = _librariesRepository.BookTypes.FirstOrDefault(type => type.Id == id);
        if (bookType == null)
        {
            _logger.LogInformation("Not found book type: {id}", id);
            return NotFound();
        }
        else
        {
            return Ok(bookType);
        }
    }
}