using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_getmonitorstatus_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            monitorProcess = Mock.Of<MonitorProcess>(x => x.Id == id);
            checkIn = Mock.Of<CheckIn>(x => x.MonitorId == id);
            checkInRepository = Mock.Of<ICheckInRepository<CheckIn>>(x => x.GetAllAsync() == Task.FromResult(new HashSet<CheckIn> { checkIn }));
            monitorRepository = Mock.Of<IMonitorRepository<MonitorProcess>>(x => x.GetAsync(id.ToString()) == Task.FromResult(monitorProcess));
            
            controller = new Builder().WithCheckInRepository(checkInRepository)
                                      .WithMonitorRepository(monitorRepository)
                                      .Build();
        };
        Because of = async () => 
            result = await controller.GetMonitorStatusAsync(id);


        It should_call_get_monitor_async_once = () => 
                Mock.Get(monitorRepository).Verify(x => x.GetAsync(id.ToString()), Times.Once);

        It should_get_all_check_ins_once = () =>
                Mock.Get(checkInRepository).Verify(x => x.GetAllAsync(), Times.Once);

        It should_contain_ok_result = () =>
                result.ShouldBeOfExactType(typeof(OkResult));

        static ICheckInRepository<CheckIn> checkInRepository;
        static IMonitorRepository<MonitorProcess> monitorRepository;
        static Guid id;
        static CheckIn checkIn;
        static MonitorProcess monitorProcess;
        static MonitorsController controller;
        static IActionResult result;
    }
}
