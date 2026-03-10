using FluentValidation;
using WebAppGeneratorId.Dto;

namespace WebAppGeneratorId.Validations
{
    public class PersonValidator : AbstractValidator<PersonInput>
    {
        public PersonValidator()
        {
            RuleFor(p=>p.Name).NotEmpty();
            RuleFor(p=>p.LastName).NotEmpty();
            RuleFor(p=>p.Adresse).NotEmpty();   
            RuleFor(p=>p.Birthday).LessThanOrEqualTo(DateTime.UtcNow);

        }

    }
}
