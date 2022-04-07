using BlazorTestApp.Models.Address;
using BlazorTestApp.Tests.Unit.Helpers;
using BlazorTestApp.UI.Pages;
using Bunit;
using FluentAssertions;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace BlazorTestApp.Tests.Unit.Pages
{
    public class AddressComponentForUSATests
    {
        [Test]
        public void Show_US_Address_Correct()
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

        [Test]
        public void Show_Empty_Address_When_NotFound_Or_Not_USA()
        {
            var addressResponse = new IpAddressData
            {
                City = "Tbilisi",
                Country = "GE",
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
                component.Find($"#Country").GetAttribute("value").Should().BeNull();
                component.Find($"#ZipCode").GetAttribute("value").Should().BeNull();
                component.Find($"#City").GetAttribute("value").Should().BeNull();
            });
        }
    }
}