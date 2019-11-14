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
    public class when_getmonitorstatus_but_no_checkins_found {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            monitorDetails = Mock.Of<MonitorDetails>(x => x.Subscribers == fixture.Create<String[]>());
            monitorProcess = Mock.Of<MonitorProcess>(x => x.Id == id && x.MonitorDetails == monitorDetails);
            checkIn = Mock.Of<CheckIn>(x => x.MonitorId == fixture.Create<Guid>());
            checkInRepository = Mock.Of<ICheckInRepository<CheckIn>>(x => x.GetAllAsync() == Task.FromResult(new HashSet<CheckIn> { checkIn }));
            monitorRepository = Mock.Of<IMonitorRepository<MonitorProcess>>(x => x.GetAsync(id.ToString()) == Task.FromResult(monitorProcess));
            emailService = Mock.Of<IEmailService>();

            controller = new Builder().WithCheckInRepository(checkInRepository)
                                      .WithMonitorRepository(monitorRepository)
                                      .WithEmailService(emailService).Build();
        };
        Because of = async () =>
                result = await controller.GetMonitorStatusAsync(id);


        It should_call_get_monitor_async_once = () =>
                Mock.Get(monitorRepository).Verify(x => x.GetAsync(id.ToString()), Times.Once);

        It should_get_all_check_ins_once = () =>
                Mock.Get(checkInRepository).Verify(x => x.GetAllAsync(), Times.Once);

        It should_call_email_service_once = () =>
                Mock.Get(emailService).Verify(x => x.SendEmailAsync(monitorProcess.MonitorDetails.Subscribers, Moq.It.IsAny<String>(), Moq.It.IsAny<String>()), Times.Once);
        
        It should_return_not_found_result = () =>
                result.ShouldBeOfExactType(typeof(NotFoundResult));

        static ICheckInRepository<CheckIn> checkInRepository;
        static IMonitorRepository<MonitorProcess> monitorRepository;
        static IEmailService emailService;
        static Guid id;
        static CheckIn checkIn;
        static MonitorProcess monitorProcess;
        static MonitorDetails monitorDetails;
        static MonitorsController controller;
        static IActionResult result;
    }
}
