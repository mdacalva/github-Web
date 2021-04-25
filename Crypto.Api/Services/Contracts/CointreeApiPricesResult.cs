using System;

namespace Crypto.Api.Services.Contracts
{
    public class CointreeApiPricesResult
    {
        public string sell { get; set; }
        public string buy { get; set; }
        public decimal ask { get; set; }
        public decimal bid { get; set; }
        public decimal rate { get; set; }
        public decimal spotRate { get; set; }
        public string market { get; set; }
        public DateTime timestamp { get; set; }
        public string rateType { get; set; }
        public string rateSteps { get; set; }
    }
}
