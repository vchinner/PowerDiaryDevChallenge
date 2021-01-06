using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PowerDiaryDevChallenge.Chat
{
    /// <summary>
    /// Chat event types
    /// </summary>
    public enum EventTypes
    {
        [Description("enter-the-room")]
        enter_the_room = 1,
        [Description("leave-the-room")]
        leave_the_room,
        comment,
        [Description("high-five-another-user")]
        high_five_another_user,

    }
}
