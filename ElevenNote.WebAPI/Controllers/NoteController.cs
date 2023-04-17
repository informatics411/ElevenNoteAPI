using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;
    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    //GET api/Note
[HttpGet]
public async Task<IActionResult> GetAllNotes()
{
        var notes = await _noteService.GetAllNotesAsync();
        return Ok(notes);
}

// POST api/Note
[HttpPost]
public async Task<IActionResult> CreateNote([FromBody] NoteCreate request)
{
    if (!ModelState.IsValid)
    return BadRequest(ModelState);

    if (await _noteService.CreateNoteAsync(request))
    return Ok("Note created successfully.");

    return BadRequest("Note could not be created.")
}

//GET api/Note/5
[HttpGet("{noteId:int}")]
public async Task<IActionResult> GetNoteById([FromRoute] int noteId)
{
    var detail = await _noteService.GetNoteByIdAsync(noteId);

    ///Similar to our service method, we're using a ternary to determine our return type
    //If the reutrned value (detail) is not null, return it with a 200 OK
    //Otherwise return a NotFound() 404 Respone
    return detail is not null
        ? Ok(detail)
        : NotFound();
}

}

