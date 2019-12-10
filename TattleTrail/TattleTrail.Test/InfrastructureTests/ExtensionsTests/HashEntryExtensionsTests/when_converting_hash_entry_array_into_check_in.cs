using AutoFixture;
using Machine.Specifications;
using StackExchange.Redis;
using System;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.InfrastructureTests.ExtensionsTests.HashEntryExtensionsTests {
    [Subject(typeof(HashEntryExtensions))]
    public class when_converting_hash_entry_array_into_check_in {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            expectedCheckInId = fixture.Create<Guid>();
            checkinKey = "checkinid:" + expectedCheckInId;
            expectedMonitorId = fixture.Create<Guid>();
            var monitorId = new HashEntry(nameof(CheckIn.MonitorId), expectedMonitorId.ToString());
            hashEntry = new HashEntry[] { monitorId };

        };
        Because of = () => 
            result = hashEntry.AsCheckInProcess(checkinKey);


        It should_contain_correctly_cropped_check_in_id = () => 
            result.CheckInId.ShouldEqual(expectedCheckInId);

        It should_contain_correct_monitor_id = () =>
            result.MonitorId.ShouldEqual(expectedMonitorId);


        static RedisKey checkinKey;
        static Guid expectedCheckInId;
        static Guid expectedMonitorId;
        static HashEntry[] hashEntry;
        static CheckIn result;
    }
}
