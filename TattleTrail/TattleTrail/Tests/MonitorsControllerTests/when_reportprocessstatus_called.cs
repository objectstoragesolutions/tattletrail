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
    public class when_reportprocessstatus_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            monitor = Mock.Of<MonitorProcess>(x => x.Id == id);
            _repository = Mock.Of<IRepository>(x => x.GetMonitorAsync(id) == Task.FromResult(monitor));
            _controller = new Builder().WithRepository(_repository).Build();
        };

        Because of = async () =>
            result = await _controller.GetMonitorStatus(id);

        It should_call_get_monitor_async = () =>
            Mock.Get(_repository).Verify(x => x.GetMonitorAsync(id), Times.Once);

        It should_return_valid_result = () =>
            result.ShouldBeOfExactType(typeof(OkObjectResult));


        static MonitorsController _controller;
        static IActionResult result;
        static MonitorProcess monitor;
        static Guid id;
        static IRepository _repository;
    }
}
