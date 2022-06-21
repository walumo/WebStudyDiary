using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebStudyDiary.Models;

namespace StudyDiary
{

    static class Program
    {
        static void Main(string[] args)
        {
            int option;

            while (true)
            {
                
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Study Diary v.1.0.0");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1) Enter new topic");
                Console.WriteLine("2) List your topics");
                Console.WriteLine("3) Add notes to topics");
                Console.WriteLine("4) Search");
                Console.WriteLine("5) Delete topics");
                Console.WriteLine("6) Update topics");
                Console.WriteLine("7) Clean expired topics");
                Console.WriteLine("8) Manage notes");
                Console.WriteLine("9) Save & exit\n");
                Console.Write("Your selection: ");

                try
                {
                    string getValue = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(getValue) || Convert.ToInt32(getValue) < 1 || Convert.ToInt32(getValue) > 9) continue;
                    option = Convert.ToInt32(getValue);
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input!");
                    Console.Write("Press enter to continue...");
                    Console.ReadKey();
                    continue;
                }



                switch (option)
                {
                    case 1:
                        //myTopics.Add(Diary.NewTopic(myTopics));
                        break;
                    case 2:
                        Load.GetTopics();
                        break;
                    case 3:
                        int topicIndex;
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("TOPICS: ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("---------");
                        Console.WriteLine("Choose a topic to add notes to: ");
                        //foreach (Topic topic in myTopics)
                        //{
                        //    if (topic.Tasks.Notes.Count < 1)
                        //        Console.WriteLine("{0}. {1}", topic.Id, topic.Title.ToUpper());
                        //}
                        //Console.Write("\nYour selection: ");
                        //try
                        //{
                        //    List<int> topicsWithoutTasks = (from topic in myTopics where topic.Tasks.Notes.Count() <= 0 select topic.Id).ToList();
                        //    topicIndex = Convert.ToInt32(Console.ReadLine());
                        //    if (topicsWithoutTasks.Contains(topicIndex))
                        //    {
                        //        myTopics[topicIndex - 1].Tasks = Diary.NewTask(myTopics[topicIndex - 1].Tasks.Notes.Count());
                        //    }
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.WriteLine(e.Message);
                        //    break;
                        //}
                        break;
                    case 4:
                        Console.Clear();
                        //Search.Topic(myTopics);
                        break;
                    case 5:
                        //Delete.Topic(myTopics);
                        break;
                    case 6:
                        //Update.Topics(myTopics);
                        break;
                    case 7:
                        //myTopics = Delete.CleanUp(myTopics);
                        break;
                    case 8:
                        //Update.Tasks(myTopics);
                        break;
                    case 9:
                        //Save.SaveAll(myTopics);
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}