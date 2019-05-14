// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Peaky.Client;
using Xunit;
using Xunit.Abstractions;

namespace MLS.Integration.Tests
{
    public class IntegrationTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private static readonly TimeSpan HttpClientTimeout = TimeSpan.FromMinutes(1);
        private const int RetryCount = 10;
        private readonly PeakyClient _peakyClient = new PeakyClient(new HttpClient(){Timeout = HttpClientTimeout});

        public IntegrationTests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Theory]
        [ClassData(typeof(AgentTestsDiscovery))]
        [ClassData(typeof(LanguageServicesTestsDiscovery))]
        public async Task The_peaky_test_passes(Uri url)
        {
            var attempts = 0;
            TestResult result;
            do
            {
                await Task.Delay(10000 * attempts);
                try
                {
                    result = await _peakyClient.GetResultFor(url);
                    if (result.Passed)
                    {
                        break;
                    }
                    else
                    {
                        _output.WriteLine($"Failed attempt with outcome {result.Content}");
                        result = null;
                    }
                }
                catch (Exception e)
                {
                    _output.WriteLine($"Failed attempt with exception {e}");
                    result = null;
                }
                finally
                {
                    attempts++;
                }

            } while (attempts < RetryCount);

            result.Should().NotBeNull("All attempts failed");

            _output.WriteLine(result?.Content);

            result?.Passed.Should().BeTrue();
        }

        public void Dispose() => _peakyClient.Dispose();
    }
}
