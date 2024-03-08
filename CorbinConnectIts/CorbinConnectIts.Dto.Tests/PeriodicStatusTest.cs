// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.CorbinConnectIts.Dto;
using FluentAssertions;
using System.Text.Json;

namespace CorbinConnectIts.Dto.Tests
{
    public class PeriodicStatusTest
    {
        [Fact]
        public void Deserialize()
        {
            var stream = GetType().Assembly.GetManifestResourceStream("CorbinConnectIts.Dto.Tests.PeriodicStatus.json");
            var tobj = JsonSerializer.Deserialize<PeriodicStatus>(stream ?? Stream.Null);
            tobj.Should().NotBeNull();
            tobj.Timestamp.Should().Be("2023-01-09T14:01:37.804-05:00");
            tobj.Customer.Should().Be("Econolite");
            tobj.Region.Should().Be("Testing");
            tobj.SerialNumber.Should().Be("123456");
            tobj.StatusIntervalSec.Should().Be(30);
            tobj.Subtype.Should().Be("connectubuntu18");
            tobj.Location.Should().Be("bw_office");
            tobj.Latitude.Should().Be("123.4");
            tobj.Longitude.Should().Be("567.8");
            tobj.Elevation.Should().Be("9.0");
            tobj.SystemStatus.Should().Be("NORMAL");
            tobj.SensorPollingIntervalSec.Should().Be(0);
            tobj.SensorPollingStatus.Should().BeTrue();
            tobj.Sensors.Should().BeNull();
            tobj.Digitaloutput1.Should().Be("UNKNOWN");
            tobj.Digitaloutput2.Should().Be("UNKNOWN");
            tobj.Version.Should().Be("1.1.65.20230104");
            tobj.LoggingLevel.Should().Be("DEBUG");
            tobj.ScriptStatus.Count.Should().Be(0);
            tobj.AnalogInput1.Should().Be(0.0);
            tobj.AnalogInput2.Should().Be(0.0);
            tobj.AdditionalProperties.Count.Should().Be(0);
        }
    }
}
