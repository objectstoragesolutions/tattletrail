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
    public class when_getmonitorstatus_but_no_monitor_with_mentioned_id_exists {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            monitorProcess = Mock.Of<MonitorProcess>(x => x.Id == Guid.Empty);
            monitorRepository = Mock.Of<IMonitorRepository<MonitorProcess>>(x => x.GetAsync(id.ToString()) == Task.FromResult(monitorProcess));

            controller = new Builder().WithMonitorRepository(monitorRepository).Build();
        };

        Because of = async () =>
                result = await controller.GetMonitorStatusAsync(id);

        It should_call_get_monitor_async_once = () =>
                Mock.Get(monitorRepository).Verify(x => x.GetAsync(id.ToString()), Times.Once);

        It should_return_not_found_result = () =>
                result.ShouldBeOfExactType(typeof(NotFoundObjectResult));

        static IMonitorRepository<MonitorProcess> monitorRepository;
        static Guid id;
        static MonitorProcess monitorProcess;
        static MonitorsController controller;
        static IActionResult result;
    }
}
