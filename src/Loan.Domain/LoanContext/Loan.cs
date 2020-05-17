using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Loan:ValueObject<Loan>
    {
        public MonetaryAmount LoanAmount { get; }
        
        public int LoanNumberOfYears { get; set; }
        public Percent InterestRate { get; set; }

        protected Loan()
        {
        }

        public Loan(MonetaryAmount loanAmount,int loanNumberOfYears,Percent interestRate)
        {
            if (loanAmount==null)
                throw new ArgumentException("Loan amount cannot be null");
            if (interestRate==null)
                throw new ArgumentException("Interest rate cannot be null");
            if (loanAmount<=MonetaryAmount.Zero)
                throw new ArgumentException("Loan amount must be grated than 0");
            if (interestRate <= Percent.Zero)
                throw new ArgumentException("Interest rate must be higher than 0");
            if (loanNumberOfYears<=0)
                throw new ArgumentException("Loan number of years must be greater than 0");
            
            LoanAmount = loanAmount;
            LoanNumberOfYears = loanNumberOfYears;
            InterestRate = interestRate;
        }

        public MonetaryAmount MonthlyInstallment()
        {
            var totalInstallments = LoanNumberOfYears * 12;
            double rate = 0;
            for (int i = 1; i <= totalInstallments; i++)
            {
                rate += Math.Pow(1.0 + (double) InterestRate.Value / 100 / 12, -1 * i);
            }
            
            return new MonetaryAmount( this.LoanAmount.Amount / Convert.ToDecimal(rate));
        }

        public DateTime LastInstallmentsDate()
        {
            return SystemTime.Now().AddYears(this.LoanNumberOfYears);
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                LoanAmount,
                LoanNumberOfYears,
                InterestRate
            };
        }
    }
}