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
    public class when_post_monitor_status_async_but_cant_find_monitor_process {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            monitorDetails = Mock.Of<MonitorDetails>(x => x.Subscribers == fixture.Create<String[]>());
            monitorProcess = Mock.Of<MonitorProcess>(x => x.Id == Guid.Empty && x.MonitorDetails == monitorDetails);
            monitorRepository = Mock.Of<IMonitorRepository<MonitorProcess>>(x => x.GetAsync(id.ToString()) == Task.FromResult(monitorProcess) &&
                                x.CreateAsync(monitorProcess) == Task.FromResult(false));

            controller = new Builder().WithMonitorRepository(monitorRepository).Build();
        };
        Because of = async () =>
                result = await controller.PostMonitorStatusAsync(id);


        It should_call_get_monitor_async_once = () =>
                Mock.Get(monitorRepository).Verify(x => x.GetAsync(id.ToString()), Times.Once);

        It should_return_not_found_result = () =>
                result.ShouldBeOfExactType(typeof(NotFoundObjectResult));

        static IMonitorRepository<MonitorProcess> monitorRepository;
        static Guid id;
        static MonitorProcess monitorProcess;
        static MonitorDetails monitorDetails;
        static MonitorsController controller;
        static IActionResult result;
    }
}
