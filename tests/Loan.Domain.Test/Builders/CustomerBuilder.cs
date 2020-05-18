using Loan.Core;

namespace Loan.Domain.Test.Builders
{
    internal class CustomerBuilder
    {
        private Name name = new Name("Alper","Hankendi");
        private NationalIdentifier nationalIdentifier = new NationalIdentifier("11111111111");
        private int age;
        private MonetaryAmount income = new MonetaryAmount(20_000M);
        private Address address = new Address("Turkey","34840","İstanbul","Cumhuriyet Cad.");
        
        public CustomerBuilder WithIdentifier(string nationalId)
        {
            nationalIdentifier = new NationalIdentifier(nationalId);
            return this;
        }
        
        public CustomerBuilder WithName(string first, string last)
        {
            name = new Name(first,last);
            return this;
        }
        
        public CustomerBuilder WithAge(int age)
        {
            this.age = age;
            return this;
        }
        
        public CustomerBuilder WithIncome(decimal income)
        {
            this.income = new MonetaryAmount(income);
            return this;
        }
        
        public CustomerBuilder WithAddress(string country,string zip,string city,string street)
        {
            this.address = new Address(country,zip,city,street);
            return this;
        }

        public Customer Build()
        {
            return new Customer
            (
                nationalIdentifier,
                name,
                SystemTime.Now().AddYears(-1*age),
                income,
                address
            );
        }
        
    }
}