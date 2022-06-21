using System;

namespace StudyDiary
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool inProgress { get; set; } = false;
        public DateTime CompletionDate { get; set; }
        public Task Tasks { get; set; } = new Task();
    }
}