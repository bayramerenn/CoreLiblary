using FluentValidation;
using FluentValidationApp.Web.Models;
using System;

namespace FluentValidationApp.Web.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz";
        private string[] Numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>
            {
                var status = true;

                foreach (var item in Numbers)
                {
                    if (x == null)
                    {
                        status = false;
                        break;
                    }
                    if (x.Contains(item))
                    {
                        status = false;
                        break;
                    }
                }
                return status;
            }).WithMessage("Name alanı rakam içermelidir");

            RuleFor(x => x.Email).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress().WithMessage("Email alanı doğru formatta değildir");
            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(18, 60).WithMessage("Age alanı 18 - 60 yaş arası olamalıdır");

            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage)
                .Must(x =>
            {
                return DateTime.Now.AddYears(-18) >= x;
            }).WithMessage("Yaşınız 18 yaşından büyük olmalıdır");

            RuleFor(x => x.Gender).IsInEnum().WithMessage("{PropertyName} alanı Man = 1 and Women = 2 olmalıdır");

            RuleForEach(x => x.Addresses).NotNull().WithMessage("adres yok").SetValidator(new AddressValidator());
        }
    }
}