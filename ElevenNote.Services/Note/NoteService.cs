using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

    public interface NoteService: INoteService
    {
        private readonly int _userId;
        private readonly ElevenNoteDbContext _dbContext;
        public NoteService(IHttpContextAccessor httpContextAccessor)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
                throw new Exception ("Attempted to build NoteService withouyt User Id claim.");
                
            _dbContext = dbContext;    
        }

        public async Task<IEnumerable<NoteListItem>> GetAllNotesAsync()
        {
            var notes = await _dbContext.Notes
                .Where(entity => entity.OwnerId == _userId)
                .Select(entity => new NoteListItem
                {
                Id = entity.Id,
                Title = entity.Title,
                CreatedUTC = entity.CreatedUTC
                })
            .ToListAsync();
            return notes;
        }
    }