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
    public class when_reportprocessstatus_cant_find_monitor_with_methioned_id {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            monitor = Mock.Of<MonitorProcess>();
            _repository = Mock.Of<IRepository>(x => x.GetMonitorAsync(id) == Task.FromResult(monitor));
            _controller = new Builder().WithRepository(_repository).Build();
        };

        Because of = async () =>
            result = await _controller.ReportProcessStatus(id);

        It should_call_get_monitor_async = () =>
            Mock.Get(_repository).Verify(x => x.GetMonitorAsync(id), Times.Once);

        It should_return_not_found_result = () =>
            result.ShouldBeOfExactType(typeof(NotFoundResult));


        static MonitorsController _controller;
        static IActionResult result;
        static MonitorProcess monitor;
        static Guid id;
        static IRepository _repository;
    }
}
