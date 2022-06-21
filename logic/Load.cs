using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebStudyDiary.Models;

namespace StudyDiary
{
    class Load
    {
        public static IQueryable<Topic> GetTopics()
        {
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                var topicsFromDb = db.Topics.Select(x => x);
                Console.WriteLine(topicsFromDb.Select(x => x.TopicId == 2));
                return topicsFromDb;
            }
        }

        

        //public static List<Topic> LoadAll()
        //{
        //    List<Topic> buffer = new List<Topic>();

        //    foreach (string item in File.ReadAllLines(Environment.CurrentDirectory + @"\topics\topic.txt").ToList())
        //    {
        //        Topic dsTopic = JsonConvert.DeserializeObject<Topic>(item);
        //        buffer.Add(dsTopic);
        //    }
        //    return buffer;
        //}
    }
}
