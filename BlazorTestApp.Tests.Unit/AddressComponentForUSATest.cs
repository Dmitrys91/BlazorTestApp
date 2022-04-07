using BlazorTestApp.Models.Address;
using BlazorTestApp.Tests.Unit;
using BlazorTestApp.UI.Pages;
using Bunit;
using FluentAssertions;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace BlazorApp.WebAssembly.Tests
{
    public class AddressComponentForUSATest
    {
        [Test]
        public void FetchResultTest()
        {
            var addressResponse = new IpAddressData
            {
                City = "New York",
                Country = "US",
                Postal = "1012"
            };

            using var ctx = new Bunit.TestContext();

            var mockedHttpClient = ctx.Services.AddMockHttpClient();

            mockedHttpClient
                .When("https://ipinfo.io")
                .RespondJson(addressResponse);

            var component = ctx.RenderComponent<AddressComponentForUSA>();

            component.WaitForAssertion(() =>
            {
                component.Find($"#Country").GetAttribute("value").Should().Be(addressResponse.Country);
                component.Find($"#ZipCode").GetAttribute("value").Should().Be(addressResponse.Postal);
                component.Find($"#City").GetAttribute("value").Should().Be(addressResponse.City);
            });
        }
    }
}