using FluentValidation;
using StoreBLL.DTO;
using StoreBLL.Interfaces;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CountryValidation : AbstractValidator<Country>
{
    private readonly ICountryRepository _countryRepository;

    public CountryValidation(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("The name must be greater than 3");

        RuleFor(x => x.Name)
            .MustAsync(ValidateName)
            .WithMessage("The name must be greater than 3");

        RuleFor(x => x.MainImageURL)
            .NotEmpty()
            .WithMessage("The MainImageURL must be not empty"); ;
    }

    private async Task<bool> ValidateName(string country, CancellationToken cancellationToken)
    {
        var existingCountry = (await _countryRepository.GetAll()).FirstOrDefault(x => x.Name == country);

        return existingCountry is null;
    }
}