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
                Console.WriteLine("3) Add tasks to topics");
                Console.WriteLine("4) Search");
                Console.WriteLine("5) Delete topics");
                Console.WriteLine("6) Update topics");
                Console.WriteLine("7) Clean expired topics");
                Console.WriteLine("8) Manage tasks");
                Console.WriteLine("9) Exit\n");
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
                        Diary.NewTopic();
                        break;
                    case 2:
                        Load.GetTopics();
                        break;
                    case 3:
                        Diary.NewTask();
                        break;
                    case 4:
                        Search.Topic();
                        break;
                    case 5:
                        Delete.Topic();
                        break;
                    case 6:
                        Update.Topics();
                        break;
                    case 7:
                        Delete.CleanUp();
                        break;
                    case 8:
                        Update.Tasks();
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}