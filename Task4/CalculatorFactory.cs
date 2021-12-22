using System;

namespace Calculator.Task4
{
    public class CalculatorFactory : ICalculatorFactory
    {
        private ICurrencyService currencyService;
        private ITripRepository tripRepository;
        private ILogger logger;

        public CalculatorFactory(
            ICurrencyService currencyService,
            ITripRepository tripRepository,
            ILogger logger)
        {
            this.currencyService = currencyService;
            this.tripRepository = tripRepository;
            this.logger = logger;
        }

        public ICalculator CreateCalculator()
        {
            return new InsurancePaymentCalculator(currencyService, tripRepository);
        }

        public ICalculator CreateCalculator(bool withLogging, bool withCaching, bool withRounding)
        {
            var result = CreateCalculator();

            if (withCaching)
                result = new CachedPaymentDecorator(result);
            if (withRounding)
                result = new RoundingCalculatorDecorator(result);
            if (withLogging)
                result = new LoggingCalculatorDecorator(result, logger);
            return result;
        }
    }
}
