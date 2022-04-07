using AngleSharp.Html.Dom;
using BlazorTestApp.Core.Services;
using BlazorTestApp.Model.Address;
using BlazorTestApp.Pages;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;


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
                Country = "USA",
                Postal = "1012"
            };

            using var ctx = new Bunit.TestContext();

            var addressServiceMock = new Mock<IAddressService>();

            addressServiceMock
                .Setup(x => x.FetchCurrentAddressAsync())
                .ReturnsAsync(addressResponse);

            ctx.Services.AddScoped(x => addressServiceMock.Object);

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