﻿using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_getallmonitors_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitors = fixture.Create<HashSet<MonitorProcess>>();
            _repository = Mock.Of<IRepository>(x => x.GetAllMonitors() == Task.FromResult(monitors));
            _controller = new Builder().WithRepository(_repository).Build();
        };

        Because of = async () =>
           result = await _controller.GetMonitorsAsync();

        It should_call_get_monitor_by_id = () =>
            Mock.Get(_repository).Verify(x => x.GetAllMonitors(), Times.Once);

        It should_return_valid_result = () =>
            result.ShouldBeOfExactType(typeof(OkObjectResult));


        static MonitorsController _controller;
        static IActionResult result;
        static HashSet<MonitorProcess> monitors;
        static IRepository _repository;
    }
}
