using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests
{
    [Subject(typeof(MonitorsController))]
    public class when_calling_get_monitors_async_api_endpoint {
        Establish _context = () => {
            _logger = Mock.Of<ILogger<MonitorsController>>();
            _repository = Mock.Of<IRepository<MonitorModel>>();
            Fixture fixture = new Fixture();
            Id = fixture.Create<string>();
            _controller = new Builder().Build();
        };

        Because of = () => {
             _result =  _controller.GetMonitorAsync(Id);
        };

        It should_get_monitor_by_id = () => {
            Mock.Get(_repository).Verify(x => x.GetMonitorAsync(Id), Times.Once);
        };

        static MonitorsController _controller;
        static String Id;
        static ILogger<MonitorsController> _logger;
        static IRepository<MonitorModel> _repository;
        static Task<IActionResult> _result;
    }
}
