using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.InfrastructureTests.ExtensionsTests.MonitorModelProcessExtensionsTests {
    [Subject(typeof(MonitorProcessModelExtensions))]
    public class when_getting_result_as_monitor_with_checkin_url_json {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitorProcess = fixture.Create<MonitorProcess>();
            var httpContext = Mock.Of<HttpContext>(x => x.Request == Mock.Of<HttpRequest>());
            Mock.Get(httpContext.Request).Setup(x => x.Host).Returns(fixture.Create<HostString>());
            Mock.Get(httpContext.Request).Setup(x => x.Scheme).Returns(fixture.Create<String>());
            Mock.Get(httpContext.Request).Setup(x => x.Path).Returns("/test");
            request = httpContext.Request;
            hostString = request.Host;
            scheme = request.Scheme;
            path = request.Path;
            checkInUrl = scheme + "://" + hostString + path + "/" + monitorProcess.Id.ToString() + "/checkin";
            expectedResult = JObject.FromObject(new { monitorid = monitorProcess.Id, checkinurl = checkInUrl });
        };

        Because of = () => 
            result = monitorProcess.GetResultJson(hostString, scheme, path);

        It should_return_correct_json_result = () => 
            result.ToString().ShouldEqual(expectedResult.ToString());

        static MonitorProcess monitorProcess;
        static JObject result;
        static HttpRequest request;
        static HostString hostString;
        static String scheme;
        static PathString path;
        static String checkInUrl;
        static JObject expectedResult;
    }
}
