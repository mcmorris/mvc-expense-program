using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddBankingApp.ExchangeRates
{
    using OpenExchangeRates;

    public interface IStockExchangeUpdater
    {
        ExchangeRateData GetLatestRates(string @base, string[] symbols);
    }
}
