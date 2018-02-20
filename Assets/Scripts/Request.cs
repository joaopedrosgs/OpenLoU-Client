using System.Collections.Generic;

namespace Assets.Scripts
{

    public class Request
    {
        public string Key;
        public AnswerTypes Type;
        public Dictionary<string, int> Data;

        public Request(AnswerTypes type, Dictionary<string, int> data)
        {
            Key = DataHolder.Key;
            Type = type;
            Data = data;
        }
    }
}