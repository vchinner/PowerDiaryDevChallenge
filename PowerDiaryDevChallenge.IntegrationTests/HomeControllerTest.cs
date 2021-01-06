using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PowerDiaryDevChallenge.Controllers;
using PowerDiaryDevChallenge.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerDiaryDevChallenge.IntegrationTests
{
    public class HomeControllerTest
    {
        private HomeController _target;
        private Mock<IChatAggregateService> mockService;


        [SetUp]
        public void Setup()
        {
            mockService = new Mock<IChatAggregateService>();
            mockService.Setup(x => x.GetChatEntriesByAggregateLevel(Chat.AggregationLevels.Seconds)).Returns(new List<KeyValuePair<DateTime, List<Chat.ChatAggregate>>>
            {
                new KeyValuePair<DateTime, List<Chat.ChatAggregate>>(new DateTime(2021, 1, 1, 12, 0, 0,0), new List<Chat.ChatAggregate>{
                new Chat.ChatAggregate(){ Count = 1, EventType = Chat.EventTypes.enter_the_room, Entries = new List<string>{ "Bob enters the room" }
                }})
            });
        }

        [Test]
        public void Index_ShouldReturn_View()
        {
            _target = new HomeController(mockService.Object);

            Assert.IsInstanceOf<ActionResult>(_target.Index());
        }
    }
}
