using System;
using System.Collections.Generic;

namespace StudyDiary
{
    public enum TaskPriority { High, Medium, Low }
    public class Task
    {
        public List<string> NotesList = new List<string>();
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool Done { get; set; }

        public TaskPriority PriorityProperty { get; set; } = TaskPriority.Medium;
        internal List<string> Notes
        {
            get { return NotesList; }
            set { NotesList = value; }
        }
    }
}
