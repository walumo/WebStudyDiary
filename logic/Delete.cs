using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyDiary
{
    static class Delete
    {
        public static List<Topic> Topic(List<Topic> list)
        {
            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("DELETING:");
                Console.BackgroundColor = ConsoleColor.Black;

                foreach (Topic topic in list)
                {
                    topic.Id = list.IndexOf(topic) + 1;
                    Console.WriteLine(topic.Id + ". " + topic.Title.ToUpper());
                }
                Console.Write("\nChoose topic to delete (blank to return, 'all' to delete all topics): ");
                string input = Console.ReadLine();

                if (string.Equals(input, "all", StringComparison.OrdinalIgnoreCase)) { Delete.All(list); return list; }

                if (String.IsNullOrWhiteSpace(input)) return list;

                if (!String.IsNullOrWhiteSpace(input)
                    && !int.TryParse(input, out int result)
                    || Convert.ToInt32(input) < 0
                    || Convert.ToInt32(input) > list.Count())
                {
                    Console.WriteLine("Invalid input!");
                    Console.ReadKey();
                }
                else
                {
                    list.Remove(list[Convert.ToInt32(input) - 1]);
                }
            }
        }

        public static List<Topic> All(List<Topic> list)
        {
            list.Clear();
            return list;
        }

        public static List<Topic> CleanUp(List<Topic> list)
        {
            var buffer = list.Where(topic => topic.CompletionDate.CompareTo(DateTime.Now) > 0);

            if(list.Count()-buffer.Count() < 1)
            {
                Console.WriteLine("\nDid not find any topics with past deadlines.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"\nDeleted {list.Count()-buffer.Count()} topics with past deadline...");
                Console.ReadKey();
            }

            return buffer.ToList();
        }
    }
}
