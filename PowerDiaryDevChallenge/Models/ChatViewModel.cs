using PowerDiaryDevChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerDiaryDevChallenge.Models
{
    public class ChatViewModel
    {
        public List<KeyValuePair<DateTime, List<Chat.ChatAggregate>>> ChatEntries { get; set; }
        public string SelectedAgrregationLevel { get; set; } = Enum.GetNames(typeof(PowerDiaryDevChallenge.Chat.AggregationLevels)).ToList().First();
    }
}
