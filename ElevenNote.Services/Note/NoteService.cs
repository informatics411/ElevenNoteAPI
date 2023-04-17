using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> CreateNoteAsync(NoteCreate request)
        {
            var noteEntity = new NoteEntity
            {
                Title = request.Title,
                Content = request.Content,
                CreatedUTC = DateTimeOffset.Now,
                OwnerId = _userId
            };
            _dbContext.Notes.Add(noteEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<NoteDetail> GetNoteByIdAsync(int noteId)
        {
            //Find the first note that has the given Id and an OwnerId that matches the requesting UserId
            var NoteEntity = await _dbContext.Notes
                .FirstOrDefaultAsync(e =>
                    e.Id == noteId && e.OwnerId == _userId)
                );
            //If noteEntitiy is null then return null, otherwise initialize and return a new NoteDetail
            return NoteEntity is null ? null : new NoteDetail
            {
                Id = noteEntity.Id,
                Title = noteEntity.Title,
                Content = noteEntity.Content,
                CreatedUTC = noteEntity.CreatedUTC,
                ModifiedUTC = noteEntity.ModifiedUTC
            };      
        }
    }