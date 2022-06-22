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
    }
}
