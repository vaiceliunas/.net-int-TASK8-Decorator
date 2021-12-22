using System;
using System.Collections.Generic;

namespace Calculator.Task2
{
    public class InsurancePaymentCalculator : ICalculator
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;

        public InsurancePaymentCalculator(
            ICurrencyService currencyService,
            ITripRepository tripRepository)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
        }

        public decimal CalculatePayment(string touristName)
        {
            var currencyRate = currencyService.LoadCurrencyRate();
            var trip = tripRepository.LoadTrip(touristName);

            var result = (Constants.A * currencyRate * trip.FlyCost) +
                         (Constants.B * currencyRate * trip.AccomodationCost) +
                         (Constants.C * currencyRate * trip.ExcursionCost);
            return result;
        }
    }

    public class CachedInsurancePaymentCalculator : ICalculator
    {
        private ICalculator _calculator;
        public CachedInsurancePaymentCalculator(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            var cachedItem = ConsoleCache.GetItem(touristName);
            if (cachedItem != null)
                return cachedItem.Value;

            var result = _calculator.CalculatePayment(touristName);
            ConsoleCache.AddItem(new KeyValuePair<string, decimal>(touristName,result));
            return result;
        }
    }
}
