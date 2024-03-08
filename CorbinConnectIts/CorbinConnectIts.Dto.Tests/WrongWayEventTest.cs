// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.CorbinConnectIts.Dto;
using FluentAssertions;
using System.Text.Json;

namespace CorbinConnectIts.Dto.Tests
{
    public class WrongWayEventTest
    {
        [Fact]
        public void Deserialize()
        {
            var stream = GetType().Assembly.GetManifestResourceStream("CorbinConnectIts.Dto.Tests.WrongWayEvent.json");
            var tobj = JsonSerializer.Deserialize<WrongWayEvent>(stream ?? Stream.Null);
            tobj.Should().NotBeNull();
            tobj.Timestamp.Should().Be("2023-01-09T14:51:55.921-05:00");
            tobj.Customer.Should().Be("Econolite");
            tobj.Region.Should().Be("Testing");
            tobj.Serial.Should().Be("123456");
            tobj.SensorParams.Count.Should().Be(4);
            tobj.SensorParams.Should().Contain(new KeyValuePair<string, string>("REMOTE_ADDR", "127.0.0.1"));
            tobj.SensorParams.Should().Contain(new KeyValuePair<string, string>("lat", "1234"));
            tobj.SensorParams.Should().Contain(new KeyValuePair<string, string>("lon", "5678"));
            tobj.SensorParams.Should().Contain(new KeyValuePair<string, string>("vehicle", "wrongway"));
            tobj.OperationType.Should().Be("Wrongway Alert");
            tobj.Reason.Should().Be("camera only wrongway");
            tobj.CustomContent.Should().Be("kafka activate");
            tobj.Attachments.Count.Should().Be(0);
            tobj.RadarData.Should().BeNull();
            tobj.AdditionalProperties.Count.Should().Be(0);
        }
    }
}
