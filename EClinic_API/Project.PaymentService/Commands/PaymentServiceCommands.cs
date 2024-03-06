using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.PaymentService.Model;

namespace Project.PaymentService.Commands
{
    public record CreatePaymentCommand(IQueryCollection QueryParameters, Data.PaymentService PaymentService) : IRequest<string>;
    public record CreateRefundCommand(RefundModel RefundModel, string ipAddress) : IRequest<ObjectResult>;
    public record CreateRefundAutoCommand(RefundModel RefundModel, string ipAddress) : IRequest<ObjectResult>;
}