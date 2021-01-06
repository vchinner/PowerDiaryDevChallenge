using PowerDiaryDevChallenge.Chat;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PowerDiaryDevChallenge.UnitTests
{
    public class ChatAggregateTests
    {
        [Theory]
        [InlineData(1, EventTypes.comment, "1 comment")]
        [InlineData(2, EventTypes.comment, "2 comments")]
        [InlineData(1, EventTypes.leave_the_room, "1 left")]
        [InlineData(3, EventTypes.leave_the_room, "3 left")]
        [InlineData(1, EventTypes.enter_the_room, "1 person entered")]
        [InlineData(3, EventTypes.enter_the_room, "3 people entered")]
        [InlineData(1, EventTypes.high_five_another_user, "1 person high-fived other people")]
        [InlineData(2, EventTypes.high_five_another_user, "2 people high-fived other people")]

        public void ToString_ShouldReturn_FormattedString(int count, EventTypes eventType, string expected)
        {
            var model = new ChatAggregate() { Count = count, EventType = eventType };
            string actual = model.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_ShouldFail_InvalidOrUnspcified_EventType()
        {
            var model = new ChatAggregate();
            Action actual = () => model.ToString();
            Assert.Throws<InvalidOperationException>(actual);
        }

        [Fact]
        public void Entries_ShouldNot_EqualNull()
        {
            var model = new ChatAggregate();
            var actual = model.Entries;
            Assert.NotNull(actual);
        }
    }
}
