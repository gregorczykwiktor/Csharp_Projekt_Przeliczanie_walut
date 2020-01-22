using System;
using Xunit;
using kantor;
using System.Linq;

namespace Currency.Tests
{
    public class CurrencyServiceTests
    {
        private readonly CurrencyService service;

        public CurrencyServiceTests()
        {
            service = new CurrencyService();
        }

        [Fact]
        public void Validation_Success()
        {
            var curr = new Currency1("EUR");

            service.AddCurrency(curr);

            var existCurrency = service.CurrList.FirstOrDefault(w => w.Sname == curr.Sname);

            Assert.NotNull(existCurrency);
        }

        [Theory]
        [InlineData("dddddD")]
        [InlineData("asd")]
        [InlineData(null)]
        [InlineData("test")]
        public void Validation_Fail(string name)
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                var ncurr = new Currency1(name);

                service.AddCurrency(ncurr);
            });
        }
    }
}
