using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_deletemonitorasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            _repository = Mock.Of<IMonitorRepository<MonitorProcess>>();
            _controller = new Builder().WithMonitorRepository(_repository).Build();
        };

        Because of = async () =>
           result = await _controller.DeleteMonitorAsync(id);

        It should_call_delete_monitor_async = () =>
            Mock.Get(_repository).Verify(x => x.DeleteAsync(id), Times.Once);

        It should_return_valid_result = () =>
            result.ShouldBeOfExactType(typeof(OkResult));


        static MonitorsController _controller;
        static IActionResult result;
        static Guid id;
        static IMonitorRepository<MonitorProcess> _repository;
    }
}
