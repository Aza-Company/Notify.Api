using FluentValidation;
using NotifyHub.Application.Features.Commands.Sms;

namespace NotifyHub.Application.Features.Validations;

public class SendSmsCommandValidator : AbstractValidator<SendSmsCommand>
{
    public SendSmsCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Текст не может быть пустым")
            .MaximumLength(500);

        RuleFor(x => x.Recipients)
            .NotEmpty().WithMessage("Нужен хотя бы один получатель");

        RuleForEach(x => x.Recipients)
            .NotEmpty().WithMessage("Номер не может быть пустым")
            .Matches(@"^\+\d{10,15}$").WithMessage("Неверный формат номера");
    }
}
