using PowerDiaryDevChallenge.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PowerDiaryDevChallenge.Data
{
    public class ChatEntry
    {
        [Key]
        public int ChatEntryId { get; private set; }
        [Required]
        public EventTypes EventType { get; private set; }
        [Required]
        public DateTime TimeStamp { get; private set; }
        [Required]
        [StringLength(50)]
        public string Username { get; private set; }
        public string Comment { get; private set; }


        /// <summary>
        /// Formatted entry based on the event type
        /// </summary>
        [NotMapped]
        public string FormattedEntry
        {
            get
            {
                switch (EventType)
                {
                    case EventTypes.enter_the_room:
                        return $"{Username} enters the room";
                    case EventTypes.leave_the_room:
                        return $"{Username} leaves";
                    case EventTypes.comment:
                        return $"{Username} comments: \"{Comment}\"";
                    case EventTypes.high_five_another_user:
                        return $"{Username} high-fives {Comment}";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// Used by entity framework
        /// </summary>
        private ChatEntry() { }


        /// <summary>
        /// Creates a new ChatEntry with primary key
        /// </summary>
        /// <param name="chatEntryId">Primary Key -  eg: 1</param>
        /// <param name="eventType">Event Type</param>
        /// <param name="timeStamp">Time Stamp</param>
        /// <param name="username">Username</param>
        /// <param name="comment">Comment - optional</param>
        public ChatEntry(int chatEntryId, EventTypes eventType, DateTime timeStamp, string username, string comment = "")
        {
            ChatEntryId = chatEntryId;
            EventType = eventType;
            TimeStamp = timeStamp;
            Username = username;
            Comment = comment;
        }

        // <summary>
        /// Creates a new ChatEntry without primary key
        /// </summary>
        /// <param name="eventType">Event Type</param>
        /// <param name="timeStamp">Time Stamp</param>
        /// <param name="username">Username</param>
        /// <param name="comment">Comment - optional</param>
        public ChatEntry(EventTypes eventType, DateTime timeStamp, string username, string comment = "")
        {
            EventType = eventType;
            TimeStamp = timeStamp;
            Username = username;
            Comment = comment;
        }
    }
}
