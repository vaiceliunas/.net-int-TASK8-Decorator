using System;
using System.Collections.Generic;

namespace Calculator.Task3
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

    public class RoundingCalculatorDecorator : ICalculator
    {
        private ICalculator _calculator;
        public RoundingCalculatorDecorator(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            return Math.Round(_calculator.CalculatePayment(touristName), MidpointRounding.AwayFromZero);
        }
    }

    public class LoggingCalculatorDecorator : ICalculator
    {
        private ICalculator _calculator;
        private ILogger _logger;
        public LoggingCalculatorDecorator(ICalculator calculator, ILogger logger)
        {
            _calculator = calculator;
            _logger = logger;
        }

        public decimal CalculatePayment(string touristName)
        {
            _logger.Log("Start");
            var result = _calculator.CalculatePayment(touristName);
            _logger.Log("End");
            return result;
        }
    }

    public class CachedPaymentDecorator : ICalculator
    {
        private ICalculator _calculator;
        public CachedPaymentDecorator(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public decimal CalculatePayment(string touristName)
        {
            var cachedItem = ConsoleCache.GetItem(touristName);
            if (cachedItem != null)
                return cachedItem.Value;

            var result = _calculator.CalculatePayment(touristName);
            ConsoleCache.AddItem(new KeyValuePair<string, decimal>(touristName, result));
            return result;
        }
    }
}
