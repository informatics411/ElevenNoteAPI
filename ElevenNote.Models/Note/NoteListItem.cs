using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class NoteListItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
    }