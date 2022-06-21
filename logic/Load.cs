using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudyDiary
{
    class Load
    {
        public static List<Topic> LoadAll()
        {
            List<Topic> buffer = new List<Topic>();

            foreach (string item in File.ReadAllLines(Environment.CurrentDirectory + @"\topics\topic.txt").ToList())
            {
                Topic dsTopic = JsonConvert.DeserializeObject<Topic>(item);
                buffer.Add(dsTopic);
            }
            return buffer;
        }
    }
}
