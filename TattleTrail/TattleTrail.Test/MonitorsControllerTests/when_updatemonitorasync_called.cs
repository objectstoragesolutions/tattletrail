﻿using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_updatemonitorasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitorDetails = fixture.Create<MonitorDetails>();
            oldDetails = fixture.Create<MonitorDetails>();
            monitorId = fixture.Create<Guid>();
            monitorProcess = new MonitorProcess { Id = monitorId, MonitorDetails = oldDetails };
            monitorProcess.MonitorDetails.UpdateMonitorDetails(monitorDetails);
            updatedMonitor = monitorProcess;
            _repository = Mock.Of<IMonitorRepository<MonitorProcess>>( x => x.GetAsync(monitorId.ToString()) == Task.FromResult(monitorProcess));
            _controller = new Builder().WithMonitorRepository(_repository).Build();
            
        };
        Because of = async () =>
            result = await _controller.UpdateMonitorAsync(monitorId, monitorDetails);

        It should_get_monitor_from_repository_once = () =>
            Mock.Get(_repository).Verify(x => x.GetAsync(monitorId.ToString()), Times.Once);

        It should_update_monitor_with_new_data = () =>
            Mock.Get(_repository).Verify(x => x.CreateAsync(updatedMonitor), Times.Once);

        It should_return_expected_result = () =>
            result.ShouldBeOfExactType(typeof(NoContentResult));


        static MonitorsController _controller;
        static MonitorDetails monitorDetails;
        static MonitorDetails oldDetails;
        static MonitorProcess updatedMonitor;
        static MonitorProcess monitorProcess;
        static Guid monitorId;
        static IMonitorRepository<MonitorProcess> _repository;
        static IActionResult result;
    }
}
