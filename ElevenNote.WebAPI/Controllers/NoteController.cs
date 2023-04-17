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

}

