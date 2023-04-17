using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    public interface INoteService
    {
        Task<bool> CreateNoteAsync(NoteCreate request);
        Task<IEnumerable<NoteListItem>> GetAllNotesAsync();
        Task<NoteDetail> GetNoteByIdAsync(int noteID);
        Task<bool> UpdateNoteAsync(NoteUpdate request);

        Task<bool> DeleteNoteAsync(int NoteId);
    }