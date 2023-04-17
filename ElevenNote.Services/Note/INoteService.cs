using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    public interface INoteService
    {
        Task<IEnumerable<NoteListItem>> GetAllNotesAsync();
    }