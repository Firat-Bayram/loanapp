using System;
using System.Threading.Tasks;
using Loan.Core;
using Loan.Domain;
using MassTransit;

namespace Loan.Service.WebApi.PortAdapters.EventConsumers
{
    public class LoanApplicationEventConsumer :
        IConsumer<Loan.Domain.Events.V1.LoanApplicationAccepted>,
        IConsumer<Loan.Domain.Events.V1.LoanApplicationRejected>
    {
        private readonly IEmailSender emailSender;

        public LoanApplicationEventConsumer(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }
        public Task Consume(ConsumeContext<Events.V1.LoanApplicationAccepted> context)
        {
            emailSender.Send("","","");
            Console.WriteLine($"Loan Application is accepted. Application Number is {context.Message.LoanApplicationId}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<Events.V1.LoanApplicationRejected> context)
        {
            emailSender.Send("","","");
            Console.WriteLine($"Loan Application is rejected. Application Number is {context.Message.LoanApplicationId}");
            return Task.CompletedTask;
        }
    }
}