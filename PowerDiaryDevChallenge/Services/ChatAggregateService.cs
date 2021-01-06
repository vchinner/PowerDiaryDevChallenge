using PowerDiaryDevChallenge.Chat;
using PowerDiaryDevChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerDiaryDevChallenge.Services
{
    public interface IChatAggregateService
    {
        public List<KeyValuePair<DateTime, List<ChatAggregate>>> GetChatEntriesByAggregateLevel(AggregationLevels aggregationLevel);
    }

    public class ChatAggregateService : IChatAggregateService
    {
        private readonly Context db;

        public ChatAggregateService(Context context)
        {

            db = context;
        }

        /// <summary>
        /// Gets a list of chat entries based on the desired aggregation level
        /// </summary>
        /// <param name="aggregationLevel">Aggregation Level</param>
        /// <returns></returns>
        public List<KeyValuePair<DateTime, List<ChatAggregate>>> GetChatEntriesByAggregateLevel(AggregationLevels aggregationLevel)
        {
            //https://stackoverflow.com/a/38892166/2521237
            long offset = CalculateOffsetTicks(aggregationLevel);

            var result = db.ChatEntries.ToList().GroupBy(x => new
            {
                Date = x.TimeStamp.AddMilliseconds(-x.TimeStamp.Millisecond)
                  .AddTicks(-x.TimeStamp.Ticks % offset)
            })
            .Select(g => new KeyValuePair<DateTime, List<ChatAggregate>>(
                g.Key.Date,
                g.GroupBy(x=>x.EventType).Select(e => new ChatAggregate()
                {
                    EventType = e.Key,
                    Count = e.Count(),
                    Entries = e.Select(x => x.FormattedEntry)
                }).ToList()
                )).ToList();

            return result;
        }

        /// <summary>
        /// Gets the total ticks based on aggregation level
        /// </summary>
        /// <param name="aggregationLevel"></param>
        /// <returns>Ticks Offset</returns>
        private long CalculateOffsetTicks(AggregationLevels aggregationLevel)
        {
            switch (aggregationLevel)
            {
                case AggregationLevels.Minutes:
                    return (1 * 60) * TimeSpan.TicksPerSecond;
                case AggregationLevels.Hours:
                    return (60 * 60) * TimeSpan.TicksPerSecond;
                case AggregationLevels.Days:
                    return (60 * 60 * 24) * TimeSpan.TicksPerSecond;
                default:
                    return (1) * TimeSpan.TicksPerSecond;
            }
        }
    }
}
