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

namespace TattleTrail.Test.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_getmonitorsasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            Id = fixture.Create<Guid>();
            _repository = Mock.Of<IMonitorRepository<MonitorProcess>>(x => x.GetAsync(Id.ToString()) == Task.FromResult(new MonitorProcess { Id = Id}));
            _controller = new Builder().WithMonitorRepository(_repository).Build();
        };

        Because of = async () => 
           result = await _controller.GetMonitorAsync(Id);

        It should_call_get_monitor_by_id = () => 
            Mock.Get(_repository).Verify(x => x.GetAsync(Id.ToString()), Times.Once);

        It should_return_valid_result = () =>
            result.ShouldBeOfExactType(typeof(OkObjectResult));


        static MonitorsController _controller;
        static Guid Id;
        static IActionResult result;
        static IMonitorRepository<MonitorProcess> _repository;
    }
}
