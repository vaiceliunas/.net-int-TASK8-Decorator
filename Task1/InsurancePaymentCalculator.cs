﻿using System;

namespace Calculator.Task1
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
}
