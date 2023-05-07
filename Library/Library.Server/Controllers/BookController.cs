﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Domain;
using Library.Server.Dto;

namespace Library.Server.Controllers;

/// <summary>
/// Book controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly LibraryDbContext _context;
    /// <summary>
    /// Used to store map's object
    /// </summary>
    private readonly IMapper _mapper;
    /// <summary>
    /// Book controller's constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public BookController(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /// <summary>
    /// Return list of all books
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookGetDto>>> GetBooks()
    {
        if (_context.Books == null)
        {
            return NotFound();
        }
        return await _mapper.ProjectTo<BookGetDto>(_context.Books).ToListAsync();
    }
    /// <summary>
    /// Return info about book by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookGetDto>> GetBook(int id)
    {
        if (_context.Books == null)
        {
            return NotFound();
        }
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return _mapper.Map<BookGetDto>(book);
    }
    /// <summary>
    /// Add a new book
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<BookGetDto>> PostBook(BookPostDto book)
    {
        if (_context.Books == null)
        {
            return Problem("Entity set 'LibraryDbContext.Books'  is null.");
        }
        var mappedBook = _mapper.Map<Book>(book);

        _context.Books.Add(mappedBook);
        await _context.SaveChangesAsync();

        return CreatedAtAction("PostBook", new { id = mappedBook.Id }, _mapper.Map<BookGetDto>(mappedBook));
    }
    /// <summary>
    /// Сhange info of selected book
    /// </summary>
    /// <param name="id"></param>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, BookPostDto book)
    {
        if (_context.Books == null)
        {
            return NotFound();
        }
        var bookToModify = await _context.Books.FindAsync(id);
        if (bookToModify == null)
        {
            return NotFound();
        }

        _mapper.Map(book, bookToModify);

        await _context.SaveChangesAsync();

        return NoContent();
    }
    /// <summary>
    /// Delete book by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        if (_context.Books == null)
        {
            return NotFound();
        }
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}