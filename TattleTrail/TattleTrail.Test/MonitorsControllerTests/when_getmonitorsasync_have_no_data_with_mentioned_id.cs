using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_getmonitorsasync_have_no_data_with_mentioned_id {

        Establish _context = () => {
            Fixture fixture = new Fixture();
            Id = fixture.Create<Guid>();
            _repository = Mock.Of<IMonitorRepository<MonitorProcess>>(x => x.GetAsync(Id.ToString()) == Task.FromResult(new MonitorProcess { Id = Guid.Empty }));
            _controller = new Builder().WithMonitorRepository(_repository).Build();
        };

        Because of = async () =>
            _result = await _controller.GetMonitorAsync(Id);

        It should_call_getmonitorasync_once = () =>
            Mock.Get(_repository).Verify(x => x.GetAsync(Id.ToString()), Times.Once);

        It should_return_not_found_result = () =>
            _result.ShouldBeOfExactType(typeof(NotFoundResult));

        static MonitorsController _controller;
        static Guid Id;
        static IMonitorRepository<MonitorProcess> _repository;
        static IActionResult _result;
    }
}
