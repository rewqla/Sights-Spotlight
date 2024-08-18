using FluentValidation;
using StoreBLL.Interfaces;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class CountryValidation : AbstractValidator<Country>
{
    private readonly ICountryRepository _countryRepository;

    public CountryValidation(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;

        RuleFor(x => x.Name)
            .MinimumLength(3)
            .WithMessage("The name must be greater than 3");

        RuleFor(x => x)
               .MustAsync(ValidateName)
               .WithName("Name")
               .WithMessage("Name is not unique");

        RuleFor(x => x.MainImageURL)
            .NotEmpty()
            .WithMessage("The MainImageURL must be not empty"); ;
    }

    private async Task<bool> ValidateName(Country country, CancellationToken cancellationToken)
    {
        if (country.Id != 0)
        {
            var existingCountry = await _countryRepository.FindById(country.Id);

            if (existingCountry.Name == country.Name)
            {
                return true;
            }

            var existingCountryByName = (await _countryRepository.GetAll())
                .FirstOrDefault(x => x.Name == country.Name);

            return existingCountryByName is null;
        }

        var newCountryByName = (await _countryRepository.GetAll())
            .FirstOrDefault(x => x.Name == country.Name);

        return newCountryByName is null;
    }
}