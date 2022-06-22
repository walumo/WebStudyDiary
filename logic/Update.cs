using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStudyDiary.Models;

namespace StudyDiary
{
    static class Update
    {
        public static void Topics()
        {
            IQueryable<Topic> topics;

            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                topics = db.Topics.Select(x => x);
                List<int> tIndex = (from topic in topics select topic.TopicId).ToList();
                string input;

                while (true)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("UPDATE:");
                    Console.BackgroundColor = ConsoleColor.Black;

                    foreach (Topic topic in topics)
                    {
                        Console.WriteLine(topic.TopicId + ". " + topic.TopicTitle.ToUpper());
                    }
                    Console.Write("\nEnter topic number to update (leave blank to return): ");

                    input = Console.ReadLine();

                    if (String.IsNullOrWhiteSpace(input)
                        || !int.TryParse(input, out int res1)
                        || !tIndex.Contains(Convert.ToInt32(input)))
                    {
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    break;
                }

                int index = Convert.ToInt32(input);

                Topic topicToUpdate = db.Topics.Where(x => x.TopicId == index).First();

                if (!String.IsNullOrWhiteSpace(input) && int.TryParse(input, out int res2))
                {

                    Load.Refresh(index);
                    Console.Write("Update topic title: ");
                    string title = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(title)) topicToUpdate.TopicTitle = title;
                    db.SaveChanges();

                    Load.Refresh(index);
                    Console.Write("Update topic description: ");
                    string description = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(description)) topicToUpdate.TopicDescription = description;
                    db.SaveChanges();

                    Load.Refresh(index);
                    Console.Write("Update estimated time to master: ");
                    string toMaster = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(toMaster)) topicToUpdate.TopicEstimatedTimeToMaster = Convert.ToDouble(toMaster);
                    db.SaveChanges();

                    Load.Refresh(index);
                    Console.Write("Update source used: ");
                    string source = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(source)) topicToUpdate.TopicSource = source;
                    db.SaveChanges();

                    while (true)
                    {
                        try
                        {
                            Load.Refresh(index);
                            Console.Write("Update date for completion (dd.mm.yyyy): ");
                            string completionDate = Console.ReadLine();
                            if (!String.IsNullOrWhiteSpace(completionDate))
                            {
                                try
                                {
                                    Load.Refresh(index);
                                    string[] dtParser = new string[3];
                                    dtParser = completionDate.Split('.');
                                    topicToUpdate.TopicCompletionDate = new DateTime(Convert.ToInt32(dtParser[2]), Convert.ToInt32(dtParser[1]), Convert.ToInt32(dtParser[0]));
                                    db.SaveChanges();
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    throw;
                                }
                            }
                            else break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Date must be formatted dd.mm.yyyy, or you can leave the input blank.");
                            Console.ReadKey();
                        }
                    }

                    while (true)
                    {
                        try
                        {
                            Load.Refresh(index);
                            Console.Write("Update time for completion (hh:mm): ");
                            string completionTime = Console.ReadLine();
                            if (!String.IsNullOrWhiteSpace(completionTime))
                            {
                                try
                                {
                                    Load.Refresh(index);
                                    string[] dtParser = new string[2];
                                    dtParser = completionTime.Split(':');
                                    topicToUpdate.TopicCompletionDate = topicToUpdate.TopicCompletionDate.AddHours(Convert.ToInt32(dtParser[0])).AddMinutes(Convert.ToInt32(dtParser[1]));
                                    db.SaveChanges();
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    throw;
                                }
                            }
                            else break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Time must be formatted as HH:MM");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }
        public static void Tasks()
        {
            IQueryable<Topic> topics;
            string input;

            while (true)
            {
                using (StudyDiaryContext db = new StudyDiaryContext())
                {
                    topics = db.Topics.Select(x => x);

                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("SELECT TASK:");
                    Console.BackgroundColor = ConsoleColor.Black;

                    foreach (Topic topic in topics)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("From topic: {0}", topic.TopicTitle.ToUpper());
                        Console.BackgroundColor = ConsoleColor.Black;
                        GetTasks(topic.TopicId);
                        Console.Write(Environment.NewLine);
                    }
                }
                Console.WriteLine("'remove (ID)' to remove task,");
                Console.WriteLine("'update (ID)' to update task");
                Console.Write("Enter commands (leave blank to return): ");

                input = Console.ReadLine().Trim().ToLower();
                List<string> commands = new List<string>();
                bool commandsValid = false;

                using (StudyDiaryContext db = new StudyDiaryContext())
                {
                    List<int> tIndex = (from task in db.Tasks select task.TaskId).ToList();
                    if (String.IsNullOrWhiteSpace(input)) return;

                    if ((input.Contains("remove") || input.Contains("update")) 
                        && input.Contains(" ")
                        && int.TryParse(input[7].ToString(),out int x1))
                    {
                        commands = input.Split(' ').ToList();
                        commandsValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        continue;
                    }

                    if (!tIndex.Contains(Convert.ToInt32(commands[1]))) { Console.WriteLine("Invalid input!"); Console.ReadKey(); continue; }

                    if (commandsValid
                        && (commands[0] == "remove" || commands[0] == "update")
                        && int.TryParse(commands[1], out int r1))
                    {
                        if (commands[0] == "remove")
                        {
                            Task taskInfo = db.Tasks.Where(x => x.TaskId == Convert.ToInt32(commands[1])).First();
                            RemoveTask(Convert.ToInt32(commands[1]));
                            Console.WriteLine("Removed task {0}. '{1}'", taskInfo.TaskId, taskInfo.TaskTitle);
                            Console.ReadKey();
                        }
                        else if (commands[0] == "update")
                        {
                            //updateTask!!
                        }

                    }

                } 
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
                    foreach (Task task in tasksFromDb)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("{0}. {1}", task.TaskId, task.TaskTitle.ToUpper());
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
                if (notesFromDb.Count() > 0)
                {
                    foreach (Note note in notesFromDb)
                    {
                        Console.WriteLine("- {0}", note.Note1);
                    }
                }
            }
        }
        private static void RemoveTask(int taskIndex)
        {
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                Task taskToDelete = db.Tasks.Where(x => x.TaskId == taskIndex).First();
                DeleteNotes(taskToDelete.TaskId);
                db.Entry(taskToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        private static void DeleteNotes(int taskId)
        {
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                foreach (Note note in db.Notes.Where(x => x.TaskId == taskId))
                {
                    db.Entry(note).State = EntityState.Deleted;
                }
                db.SaveChanges();
            }
        }
    }
}
