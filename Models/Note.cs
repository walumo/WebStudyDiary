using System;
using System.Collections.Generic;

#nullable disable

namespace WebStudyDiary.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public string NoteString { get; set; }
        public int TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}
