using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.ExtensionsTests {
    [Subject(typeof(UserModelExtensions))]
    public class when_converting_user_model_to_hash_entry {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            var userName = fixture.Create<String>();
            user = Mock.Of<User>(
                x => x.Id == Guid.NewGuid() &&
                x.UserName == userName &&
                x.MonitorProcessId == new HashSet<Guid>());
        };

        Because of = () =>
            result = user.ConvertUserToHashEntry();

        It should_contain_userid_field_name = () =>
            result[0].Name.ShouldEqual((RedisValue) nameof(user.Id));

        It should_contain_userid_field_value = () =>
            result[0].Value.ShouldEqual((RedisValue) user.Id.ToString());

        It should_contain_username_field_name = () =>
            result[1].Name.ShouldEqual((RedisValue)nameof(user.UserName));

        It should_contain_username_field_value = () =>
            result[1].Value.ShouldEqual((RedisValue) user.UserName);

        It should_contain_processid_field_name = () =>
             result[2].Name.ShouldEqual((RedisValue) nameof(user.MonitorProcessId));

        It should_contain_processid_field_value = () =>
             result[2].Value.ShouldEqual((RedisValue) user.MonitorProcessId.ToString());

        static User user;
        static HashEntry[] result;
    }
}
