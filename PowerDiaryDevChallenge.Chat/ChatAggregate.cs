using System;
using System.Collections.Generic;
using System.Text;

namespace PowerDiaryDevChallenge.Chat
{
    /// <summary>
    /// Grouping of chat entries by event type
    /// </summary>
    public class ChatAggregate
    {
        public int Count { get; set; }
        public EventTypes EventType { get; set; }
        public IEnumerable<string> Entries { get; set; } = new List<string>();

        /// <summary>
        /// Override for generating formatted string based in the count and event type
        /// </summary>
        /// <returns>Formatted String</returns>
        public override string ToString()
        {
            switch (EventType)
            {
                case EventTypes.comment:
                    return Count + " " + "comment" + (Count > 1 ? "s" : "");
                case EventTypes.enter_the_room:
                    return Count + " " + (Count > 1 ? "people" : "person") + " entered";
                case EventTypes.leave_the_room:
                    return Count + " " + "left";
                case EventTypes.high_five_another_user:
                    return Count + " " + (Count > 1 ? "people" : "person") + " high-fived other people";
                default:
                    throw new InvalidOperationException("Invalid EventType or EventType not specified");
            }

        }
    }
}
