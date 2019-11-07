using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_getmonitorsasync_get_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            Id = fixture.Create<string>();
            _repository = Mock.Of<IRepository<Monitor>>();
            _controller = new Builder().WithRepository(_repository).Build();
        };

        Because of = () => 
            _result = _controller.GetMonitorAsync(Id);

        It should_get_monitor_by_id = () => 
            Mock.Get(_repository).Verify(x => x.GetMonitorAsync(Id), Times.Once);

        static MonitorsController _controller;
        static String Id;
        static IRepository<Monitor> _repository;
        static Task<IActionResult> _result;
    }
}
