using System;
using System.Collections.Generic;
using System.Linq;
using WebStudyDiary.Models;

namespace StudyDiary
{
    static class Diary
    {
        public static void ShowTopics(IQueryable<Topic> list)
        {

            //Console.Clear();
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            //Console.WriteLine("YOUR TOPICS:");
            //Console.BackgroundColor = ConsoleColor.Black;

            //if (list.Count() < 1) Console.WriteLine("No topics in your list.\n");

            //foreach (Topic topic in list)
            //{
            //    Console.ForegroundColor = ConsoleColor.Blue;
            //    Console.WriteLine("Topic number: {0}", topic.TopicId);
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.WriteLine("****************");
            //    Console.Write($"Topic: "); 
            //    Console.ForegroundColor = ConsoleColor.Blue;
            //    Console.WriteLine(topic.TopicTitle.ToUpper());
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.WriteLine($"To master (hours): {topic.TopicEstimatedTimeToMaster}");
            //    Console.WriteLine($"Date to be completed: {topic.TopicCompletionDate}");
            //    Console.WriteLine("Time until completion: {0}",(topic.TopicCompletionDate-DateTime.Now)); 
            //    Console.WriteLine("Hours spent: {0}", topic.TopicTimeSpent);
            //    Console.WriteLine("----------------");
            //    Console.WriteLine("Description: {0}\n", topic.TopicDescription);
                
                //if (topic.Tasks.NotesList.Count() > 0)
                //{
                //    Console.WriteLine("Tasks: \n");
                //    Console.ForegroundColor = ConsoleColor.DarkCyan;
                //    Console.Write(topic.Tasks.Title.ToUpper());
                //    Console.WriteLine(" || Priority: {0}", topic.Tasks.PriorityProperty);
                //    Console.ForegroundColor = ConsoleColor.White;
                //    foreach (string note in topic.Tasks.Notes)
                //    {
                //        Console.WriteLine("- {0}", note);
                //    }
                //}
                
            //    Console.WriteLine("\nSource(s) used: {0}\n", topic.TopicSource);
            //}
            //Console.Write("Press enter to continue...");
            //Console.ReadKey();
        }

        //public static void ShowTopics(Topic topic)
        //{
        //    Console.Clear();
        //    Console.BackgroundColor = ConsoleColor.DarkBlue;
        //    Console.WriteLine("YOUR TOPICS:");
        //    Console.BackgroundColor = ConsoleColor.Black;

        //    Console.ForegroundColor = ConsoleColor.Blue;
        //    Console.WriteLine("Topic number: {0}", topic.Id);
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.WriteLine("****************");
        //    Console.Write($"Topic: "); 
        //    Console.ForegroundColor = ConsoleColor.Blue;
        //    Console.WriteLine(topic.Title.ToUpper());
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.WriteLine($"To master (hours): {topic.EstimatedTimeToMaster}");
        //    Console.WriteLine($"Date to be completed: {topic.CompletionDate}");
        //    Console.WriteLine("Time until completion: {0} days, {1} hours, {2} minutes",
        //        (topic.CompletionDate - DateTime.Now).Days,
        //        (topic.CompletionDate - DateTime.Now).Hours,
        //        (topic.CompletionDate - DateTime.Now).Minutes);
        //    Console.WriteLine("Hours spent: {0}", topic.TimeSpent);
        //    Console.WriteLine("----------------");
        //    Console.WriteLine("Description: {0}\n", topic.Description);

        //    Console.WriteLine("Tasks: \n");
        //    Console.ForegroundColor = ConsoleColor.DarkCyan;
        //    Console.Write(topic.Tasks.Title.ToUpper());
        //    Console.WriteLine(" || Priority: {0}", topic.Tasks.PriorityProperty);
        //    Console.ForegroundColor = ConsoleColor.White;
        //    foreach (string note in topic.Tasks.Notes)
        //    {
        //        Console.WriteLine("- {0}", note);
        //    }
        //    Console.WriteLine("\nSource(s) used: {0}\n", topic.Source);

        //    Console.Write("Press enter to continue...");
        //    Console.ReadKey();

        //}
        public static void NewTopic()
        {
            Topic buffer = new Topic();
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                Console.Clear();
                Console.Write("Give a title for topic: ");
                buffer.TopicTitle = Console.ReadLine();

                Console.Write("Give a description for topic: ");
                buffer.TopicDescription = Console.ReadLine();

                while (true)
                {
                    Console.Write("Enter estimated time to master: ");
                    string str = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(str) || !Double.TryParse(str, out double result)) { buffer.TopicEstimatedTimeToMaster = 1; break; }
                    buffer.TopicEstimatedTimeToMaster = Convert.ToDouble(str);
                    break;
                }

                Console.Write("Enter source used if any (if not, press Enter): ");
                buffer.TopicSource = Console.ReadLine();
                buffer.TopicInProgress = false;

                while (true)
                {
                    try
                    {
                        Console.Write("Enter date for completion (dd.mm.yyyy): ");
                        string str = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(str)) { buffer.TopicCompletionDate = new DateTime(DateTime.Now.Year + 1, 1, 1); break; }
                        else
                        {
                            try
                            {
                                string[] dtParser = new string[3];
                                dtParser = str.Split('.');
                                buffer.TopicCompletionDate = new DateTime(Convert.ToInt32(dtParser[2]), Convert.ToInt32(dtParser[1]), Convert.ToInt32(dtParser[0]));
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                throw;
                            }
                        }
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
                        Console.Write("Enter time for completion (hh:mm): ");
                        string str = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(str)) { buffer.TopicCompletionDate = buffer.TopicCompletionDate.AddHours(12); break; }
                        else
                        {
                            try
                            {
                                string[] dtParser = new string[2];
                                dtParser = str.Split(':');
                                buffer.TopicCompletionDate = buffer.TopicCompletionDate.AddHours(Convert.ToDouble(dtParser[0])).AddMinutes(Convert.ToDouble(dtParser[1]));
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                throw;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Time must be formatted as HH:MM, or you can leave the input blank.");
                        Console.ReadKey();
                    }
                }
                Console.Write("Press enter to continue...");
                Console.ReadKey();
                Console.Clear();
                db.Topics.Add(buffer);
                db.SaveChanges();
            }
        }
        // Add tasks
        //public static Task NewTask(int taskCount)
        //{
        //    Task buffer = new Task();
        //    buffer.Id = taskCount + 1;

        //    Console.Clear();
        //    Console.Write("Give a title for task: ");
        //    buffer.Title = Console.ReadLine();

        //    while (true)
        //    {
        //        try
        //        {
        //            Console.Write("Priority for this task(1=high, 2=medium, 3=low); ");
        //            string str = Console.ReadLine();
        //            if (!String.IsNullOrWhiteSpace(str) || int.TryParse(str, out int result)) { buffer.PriorityProperty = (TaskPriority)Convert.ToInt32(str) - 1; break; }
        //            else buffer.PriorityProperty = TaskPriority.Low;
        //            break;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            Console.WriteLine("Something went wrong, try again");
        //        }
        //    }

        //    buffer.Done = false;

        //    while (true)
        //    {
        //        try
        //        {
        //            Console.Write("Enter notes for this task (blank note to move on): ");
        //            string str = Console.ReadLine();
        //            if (String.IsNullOrWhiteSpace(str)) break;
        //            else buffer.NotesList.Add(str);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            Console.WriteLine("Something went wrong, try again");
        //            Console.ReadKey();
        //        }
        //    }

        //    while (true)
        //    {
        //        try
        //        {
        //            Console.Write("Enter date for completion (dd.mm.yyyy): ");
        //            string str = Console.ReadLine();
        //            if (String.IsNullOrWhiteSpace(str)) { buffer.Deadline = new DateTime(DateTime.Now.Year + 1, 1, 1); break; }
        //            else
        //            {
        //                string[] dtParser = new string[3];
        //                dtParser = str.Split('.');
        //                buffer.Deadline = new DateTime(Convert.ToInt32(dtParser[2]), Convert.ToInt32(dtParser[1]), Convert.ToInt32(dtParser[0]));
        //            }
        //            break;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            Console.WriteLine("Something went wrong, try again");
        //            Console.ReadKey();
        //        }
        //    }
        //    Console.Write("Press enter to continue...");
        //    Console.ReadKey();
        //    Console.Clear();
        //}
    }
}