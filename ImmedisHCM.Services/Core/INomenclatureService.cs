using ImmedisHCM.Services.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public interface INomenclatureService
    {
        Task<List<CountryServiceModel>> GetCountries();
        Task<List<CountryServiceModel>> GetCountriesWithCities();
        Task<CountryServiceModel> GetCountry(Guid id);
        Task<List<CityServiceModel>> GetCitiesByCountryId(Guid countryId);
        Task<List<CityServiceModel>> GetCities();
        Task<CityServiceModel> GetCity(Guid id);
        Task<List<CurrencyServiceModel>> GetCurrencies();
        Task<List<CurrencyServiceModel>> GetCurrenciesWithSalaries();
        Task<CurrencyServiceModel> GetCurrency(int id);
        Task<List<SalaryTypeServiceModel>> GetSalaryTypes();
        Task<SalaryTypeServiceModel> GetSalaryType(int id);
        Task<List<SalaryTypeServiceModel>> GetSalaryTypesWithSalaries();

    }
}
