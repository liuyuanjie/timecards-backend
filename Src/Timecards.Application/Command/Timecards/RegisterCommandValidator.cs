using System;
using FluentValidation;

namespace Timecards.Application.Command.Timecards
{
    public class AddTimecardsCommandValidator : AbstractValidator<AddTimecardsCommand>
    {
        public AddTimecardsCommandValidator()
        {
            RuleFor(c => c.AccountId).NotEmpty();
            RuleFor(c => c.GetTimecards).SetValidator(new AddTimecardsValidator());
        }
    }

    public class AddTimecardsValidator : AbstractValidator<AddTimecards>
    {
        public AddTimecardsValidator()
        {
            RuleFor(c => c.ProjectId).NotEmpty();
            RuleFor(c => c.TimecardsDate).NotEmpty().Must(s => (DateTime.Now - s).Days <= 31);
            RuleForEach(c => c.Items).SetValidator(c => new AddTimecardsItemValidator(c.TimecardsDate));
        }
    }

    public class AddTimecardsItemValidator : AbstractValidator<AddTimecardsItem>
    {
        public AddTimecardsItemValidator(DateTime startDate)
        {
            RuleFor(c => c.Hour).GreaterThanOrEqualTo(0).LessThanOrEqualTo(24);
            RuleFor(c => c.WorkDay).NotEmpty().InclusiveBetween(startDate, startDate.AddDays(7));
        }
    }
}