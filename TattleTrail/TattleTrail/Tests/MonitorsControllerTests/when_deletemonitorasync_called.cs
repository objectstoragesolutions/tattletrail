using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_deletemonitorasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            _repository = Mock.Of<IRepository>();
            _controller = new Builder().WithRepository(_repository).Build();
        };

        Because of = async () =>
           result = await _controller.DeleteMonitorAsync(id);

        It should_call_delete_monitor_async = () =>
            Mock.Get(_repository).Verify(x => x.DeleteMonitorAsync(id), Times.Once);

        It should_return_valid_result = () =>
            result.ShouldBeOfExactType(typeof(OkResult));


        static MonitorsController _controller;
        static IActionResult result;
        static Guid id;
        static IRepository _repository;
    }
}
