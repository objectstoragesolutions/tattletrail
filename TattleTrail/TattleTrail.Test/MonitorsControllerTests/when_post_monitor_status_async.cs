using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_post_monitor_status_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            monitorDetails = Mock.Of<MonitorDetails>(x => x.Subscribers == fixture.Create<String[]>());
            monitorProcess = Mock.Of<MonitorProcess>(x => x.Id == id && x.MonitorDetails == monitorDetails);
            checkIn = Mock.Of<CheckIn>(x => x.MonitorId == fixture.Create<Guid>());
            checkInRepository = Mock.Of<ICheckInRepository<CheckIn>>(x => x.GetAllAsync() == Task.FromResult(new HashSet<CheckIn> { checkIn }));
            monitorRepository = Mock.Of<IMonitorRepository<MonitorProcess>>(x => x.GetAsync(id.ToString()) == Task.FromResult(monitorProcess) && 
                                x.CreateAsync(monitorProcess) == Task.FromResult(true));

            controller = new Builder().WithCheckInRepository(checkInRepository)
                                      .WithMonitorRepository(monitorRepository)
                                      .Build();
        };
        Because of = async () =>
                result = await controller.PostMonitorStatusAsync(id);


        It should_call_get_monitor_async_once = () =>
                Mock.Get(monitorRepository).Verify(x => x.GetAsync(id.ToString()), Times.Once);

        It should_create_new_check_in_once = () =>
                Mock.Get(checkInRepository).Verify(x => x.CreateAsync(id, monitorProcess.MonitorDetails.IntervalTime), Times.Once);

        It should_update_monitor_process_once = () =>
                Mock.Get(monitorRepository).Verify(x => x.CreateAsync(monitorProcess), Times.Once);

        It should_return_ok_result = () =>
                result.ShouldBeOfExactType(typeof(OkObjectResult));

        static ICheckInRepository<CheckIn> checkInRepository;
        static IMonitorRepository<MonitorProcess> monitorRepository;
        static Guid id;
        static CheckIn checkIn;
        static MonitorProcess monitorProcess;
        static MonitorDetails monitorDetails;
        static MonitorsController controller;
        static IActionResult result;
    }
}
