using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebStudyDiary.Models;

namespace StudyDiary
{
    static class Load
    {
        public static void GetTopics()
        {
            IQueryable<Topic> topicsFromDb = null;
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                Console.WriteLine("\nLoading...");
                topicsFromDb = db.Topics.Select(x => x);

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("YOUR TOPICS:");
                Console.BackgroundColor = ConsoleColor.Black;

                if (topicsFromDb.ToList().Count() < 1) Console.WriteLine("No topics in your list.\n");
                else
                {
                    foreach (Topic topic in topicsFromDb)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Topic number: {0}", topic.TopicId);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("****************");
                        Console.Write($"Topic: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(topic.TopicTitle.ToUpper());
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"To master (hours): {topic.TopicEstimatedTimeToMaster}");
                        Console.WriteLine($"Date to be completed: {topic.TopicCompletionDate}");
                        TimeSpan timeUntil = topic.TopicCompletionDate - DateTime.Now;
                        if (topic.TopicCompletionDate.CompareTo(DateTime.Now) < 0)
                        {
                            Console.Write("Time until completion: ");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("PAST DEADLINE");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.WriteLine("Time until completion: {0} days {1} hours", timeUntil.ToString("%d"), timeUntil.ToString(@"%h"));
                        }
                        Console.WriteLine("Hours spent: {0}", topic.TopicTimeSpent);
                        Console.WriteLine("----------------");
                        Console.WriteLine("Description: {0}\n", topic.TopicDescription);

                        GetTasks(topic.TopicId);

                        Console.WriteLine("\nSource(s) used: {0}\n", topic.TopicSource);
                    }
                }
                Console.Write("Press enter to continue...");
                Console.ReadKey();
            }
        }
        public static void GetTopics(int index)
        {
            IQueryable<Topic> topicsFromDb = null;
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                Console.WriteLine("Loading...");
                topicsFromDb = db.Topics.Where(topic => topic.TopicId == index);

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("YOUR TOPICS:");
                Console.BackgroundColor = ConsoleColor.Black;

                if (topicsFromDb.ToList().Count() < 1) Console.WriteLine($"No topics found for ID: {index}\n");
                else
                {
                    foreach (Topic topic in topicsFromDb)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Topic number: {0}", topic.TopicId);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("****************");
                        Console.Write($"Topic: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(topic.TopicTitle.ToUpper());
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"To master (hours): {topic.TopicEstimatedTimeToMaster}");
                        Console.WriteLine($"Date to be completed: {topic.TopicCompletionDate}");
                        TimeSpan timeUntil = topic.TopicCompletionDate - DateTime.Now;
                        if (topic.TopicCompletionDate.CompareTo(DateTime.Now) < 0)
                        {
                            Console.Write("Time until completion: ");
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("PAST DEADLINE");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.WriteLine("Time until completion: {0} days {1} hours", timeUntil.ToString("%d"), timeUntil.ToString(@"%h"));
                        }
                        Console.WriteLine("Hours spent: {0}", topic.TopicTimeSpent);
                        Console.WriteLine("----------------");
                        Console.WriteLine("Description: {0}\n", topic.TopicDescription);

                        GetTasks(topic.TopicId);

                        Console.WriteLine("\nSource(s) used: {0}\n", topic.TopicSource);
                    }
                }
                Console.Write("Press enter to continue...");
                Console.ReadKey();
            }
        }
        public static void GetTopics(string input)
        {
            List<Topic> topicsFromDb = null;
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                Console.WriteLine("Loading...");
                topicsFromDb = (from topic in db.Topics.AsEnumerable() where Regex.IsMatch(topic.TopicTitle, input, RegexOptions.IgnoreCase) select topic).ToList();

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("YOUR TOPICS:");
                Console.BackgroundColor = ConsoleColor.Black;

                if (topicsFromDb.Count() < 1) Console.WriteLine($"No topics found for: {input}");
                else
                {
                    foreach (Topic topic in topicsFromDb)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Topic number: {0}", topic.TopicId);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("****************");
                        Console.Write($"Topic: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(topic.TopicTitle.ToUpper());
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"To master (hours): {topic.TopicEstimatedTimeToMaster}");
                        Console.WriteLine($"Date to be completed: {topic.TopicCompletionDate}");
                        TimeSpan timeUntil = topic.TopicCompletionDate - DateTime.Now;
                        if(topic.TopicCompletionDate.CompareTo(DateTime.Now) < 0)
                        {
                            Console.Write("Time until completion: ");
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("PAST DEADLINE");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.WriteLine("Time until completion: {0} days {1} hours", timeUntil.ToString("%d"), timeUntil.ToString(@"%h"));
                        }
                        Console.WriteLine("Hours spent: {0}", topic.TopicTimeSpent);
                        Console.WriteLine("----------------");
                        Console.WriteLine("Description: {0}\n", topic.TopicDescription);

                        GetTasks(topic.TopicId);

                        Console.WriteLine("\nSource(s) used: {0}\n", topic.TopicSource);
                    }
                }
                Console.Write("Press enter to continue...");
                Console.ReadKey();
            }
        }
        private static void GetTasks(int index)
        {
            IQueryable<Task> tasksFromDb = null;
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                tasksFromDb = db.Tasks.Where(task => task.TopicId == index);

                if (tasksFromDb.Count() > 0)
                {
                    Console.WriteLine("Tasks: \n");

                    foreach (Task task in tasksFromDb)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(task.TaskTitle.ToUpper());
                        Console.WriteLine(" || Priority: {0}", task.TaskPriority);
                        Console.ForegroundColor = ConsoleColor.White;

                        GetNotes(task.TaskId);

                    }
                }

            }
        }

        private static void GetNotes(int index)
        {
            IQueryable<Note> notesFromDb = null;

            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                notesFromDb = db.Notes.Where(note => note.TaskId == index);
                if(notesFromDb.Count() > 0)
                {
                    foreach (Note note in notesFromDb)
                    {
                        Console.WriteLine("- {0}", note.Note1);
                    }
                }
            }
        }
    }
}


