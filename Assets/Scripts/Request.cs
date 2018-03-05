using System.Collections.Generic;

namespace Assets.Scripts
{

    public class Request
    {
        public string Key;
        public RequestType Type;
        public Dictionary<string, int> Data;

        public Request(RequestType type, Dictionary<string, int> data)
        {
            Key = DataHolder.User.Key;
            Type = type;
            Data = data;
        }
    }
}