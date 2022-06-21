using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace StudyDiary
{
    public class Save
    {

        public static void SaveAll(List<Topic> list)
        {
            string topicPath = Environment.CurrentDirectory + @"\topics\topic.txt";
            List<string> topics = new List<string>();
            foreach (Topic topic in list)
            {
                topics.Add(JsonConvert.SerializeObject(topic));
            }

            if (!Directory.Exists(@".\topics")) Directory.CreateDirectory("topics");
            File.WriteAllLines(topicPath, topics);
        }
    }
}

