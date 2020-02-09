using System;

namespace Message
{
    using System;

    [Serializable]
    public class Message
    {
        public string Name { get; set; }

        public Message(string name)
        {
            Name = name;
        }
    }
}
