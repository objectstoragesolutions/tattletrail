using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    [Ignore("Will rework soon")]
    public class when_getmonitorsasync_have_no_data_with_mentioned_id {

        Establish _context = () => {
            Fixture fixture = new Fixture();
            Id = fixture.Create<string>();
            _repository = Mock.Of<IRepository<MonitorModel>>(x => x.GetMonitorAsync(Id) == null);
            _controller = new Builder().WithRepository(_repository).Build();
        };

        Because of = () =>
            _result = _controller.GetMonitorAsync(Id);

        It should_return_not_found_result = () =>
            Mock.Get(_repository).Verify(x => x.GetMonitorAsync(Id), Times.Once);

        static MonitorsController _controller;
        static String Id;
        static IRepository<MonitorModel> _repository;
        static Task<IActionResult> _result;
    }
}
