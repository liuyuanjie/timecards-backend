using System;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Timecards.Application.Extensions;

namespace Timecards.Application.Command.Timecards
{
    public class AddTimecardsCommandValidator : AbstractValidator<AddTimecardsCommand>
    {
        private readonly ILogger<AddTimecardsCommandValidator> _logger;

        public AddTimecardsCommandValidator(ILogger<AddTimecardsCommandValidator> logger)
        {
            _logger = logger;
            RuleForEach(c => c.Timecardses).SetValidator(c => new AddTimecardsValidator());
        }

        private class AddTimecardsValidator: AbstractValidator<AddTimecards>
        {
            public AddTimecardsValidator()
            {
                RuleFor(c => c.UserId).NotEmpty();
                RuleFor(c => c.ProjectId).NotEmpty();
                RuleFor(c => c.TimecardsDate).NotEmpty().Must(s => (DateTime.Now - s).Days <= 31);
                RuleForEach(c => c.Items).SetValidator(c =>
                {
                    var firstDayOfWeek = c.TimecardsDate.GetFirstDayOfWeek();
                    return new AddTimecardsItemValidator(firstDayOfWeek);
                });   
            }
        }

        private class AddTimecardsItemValidator : AbstractValidator<AddTimecardsItem>
        {
            public AddTimecardsItemValidator(DateTime timecardsDate)
            {
                RuleFor(c => c.Hour).GreaterThanOrEqualTo(0).LessThanOrEqualTo(24);
                // RuleFor(c => c.WorkDay).NotEmpty().InclusiveBetween(timecardsDate, timecardsDate.AddDays(7));
            }
        }
    }
}