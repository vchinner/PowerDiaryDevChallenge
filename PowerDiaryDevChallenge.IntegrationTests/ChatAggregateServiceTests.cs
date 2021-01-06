using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PowerDiaryDevChallenge.Chat;
using PowerDiaryDevChallenge.Data;
using PowerDiaryDevChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerDiaryDevChallenge.IntegrationTests
{
    public class ChatAggregateServiceTests
    {
        private ChatAggregateService _target;
        private Context mockContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "Context.Test")
                .Options;

            mockContext = new Context(options);

            //clear first
            mockContext.ChatEntries.RemoveRange(mockContext.ChatEntries);
            mockContext.SaveChanges();

            //seed
            mockContext.ChatEntries.AddRange(
                new ChatEntry(EventTypes.enter_the_room, new DateTime(2021, 1, 1, 17, 1, 22), "Bob"),
                new ChatEntry(EventTypes.enter_the_room, new DateTime(2021, 1, 1, 17, 5, 35), "Kate"),
                new ChatEntry(EventTypes.comment, new DateTime(2021, 1, 1, 17, 15, 8), "Bob", "Hey, Kate - high five?"),
                new ChatEntry(EventTypes.high_five_another_user, new DateTime(2021, 1, 1, 17, 17, 3), "Kate", "Bob"),
                new ChatEntry(EventTypes.leave_the_room, new DateTime(2021, 1, 1, 17, 18, 0), "Bob"),
                new ChatEntry(EventTypes.comment, new DateTime(2021, 1, 1, 17, 20, 0), "Kate", "Oh, typical"),
                new ChatEntry(EventTypes.leave_the_room, new DateTime(2021, 1, 1, 21, 0, 0), "Kate"),
                new ChatEntry(EventTypes.enter_the_room, new DateTime(2021, 1, 1, 21, 0, 10), "Kate")
                );

            mockContext.SaveChanges();

        }

        [TestCase(AggregationLevels.Seconds)]
        [TestCase(AggregationLevels.Minutes)]
        [TestCase(AggregationLevels.Hours)]
        [TestCase(AggregationLevels.Days)]
        public void GetChatEntriesByAggregateLevel_ShouldNotThrow_Exception(AggregationLevels aggregationLevel)
        {
            _target = new ChatAggregateService(mockContext);

            Assert.DoesNotThrow(() => _target.GetChatEntriesByAggregateLevel(aggregationLevel));
        }

        [TestCase(AggregationLevels.Seconds, 8)]
        [TestCase(AggregationLevels.Minutes, 7)]
        [TestCase(AggregationLevels.Hours, 2)]
        [TestCase(AggregationLevels.Days, 1)]
        public void GetChatEntriesByAggregateLevel_ShouldReturn_ExpectedCounts(AggregationLevels aggregationLevel, int expected)
        {
            _target = new ChatAggregateService(mockContext);

            var actual = _target.GetChatEntriesByAggregateLevel(aggregationLevel).Count;

            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void TearDown()
        {
            mockContext.Dispose();
        }
    }
}
