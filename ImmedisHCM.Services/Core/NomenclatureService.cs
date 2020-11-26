using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Data.Infrastructure;
using ImmedisHCM.Services.Models.Core;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public class NomenclatureService : INomenclatureService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public NomenclatureService(IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CityServiceModel>> GetCities()
        {
            var model = await _unitOfWork.GetRepository<City>()
                                         .GetAsync(orderBy: x => x.OrderBy(x => x.Name));

            return _mapper.Map<List<CityServiceModel>>(model);
        }

        public async Task<List<CityServiceModel>> GetCitiesByCountryId(Guid countryId)
        {
            var model = await _unitOfWork.GetRepository<City>()
                                         .GetAsync(filter: x => x.Country.Id == countryId, orderBy: x => x.OrderBy(x => x.Name));

            return _mapper.Map<List<CityServiceModel>>(model);
        }

        public async Task<CityServiceModel> GetCity(Guid id)
        {
            var city = await _unitOfWork.GetRepository<City>()
                                        .GetByIdAsync(id);

            return _mapper.Map<CityServiceModel>(city);
        }

        public async Task<List<CountryServiceModel>> GetCountries()
        {
            var model = await _unitOfWork.GetRepository<Country>()
                                         .GetAsync(orderBy: x => x.OrderBy(x => x.Name));

            return _mapper.Map<List<CountryServiceModel>>(model);
        }

        public async Task<List<CountryServiceModel>> GetCountriesWithCities()
        {
            var model = await _unitOfWork.GetRepository<Country>()
                                         .GetAsync(orderBy: x => x.OrderBy(x => x.Name), fetch: x => x.FetchMany(x => x.Cities));

            return _mapper.Map<List<CountryServiceModel>>(model);
        }

        public async Task<CountryServiceModel> GetCountry(Guid id)
        {
            var model = await _unitOfWork.GetRepository<Country>()
                                         .GetSingleAsync(x => x.Id == id,
                                          x => x.FetchMany(prop => prop.Cities));

            return _mapper.Map<CountryServiceModel>(model);
        }

        public async Task<List<CurrencyServiceModel>> GetCurrencies()
        {
            var model = await _unitOfWork.GetRepository<Currency>()
                                         .GetAsync(orderBy: x => x.OrderBy(x => x.Name));

            return _mapper.Map<List<CurrencyServiceModel>>(model);
        }

        public async Task<List<CurrencyServiceModel>> GetCurrenciesWithSalaries()
        {
            var model = await _unitOfWork.GetRepository<Currency>()
                                         .GetAsync(orderBy: x => x.OrderBy(x => x.Name), fetch: x => x.FetchMany(x => x.Salaries));

            return _mapper.Map<List<CurrencyServiceModel>>(model);
        }

        public async Task<CurrencyServiceModel> GetCurrency(int id)
        {
            var model = await _unitOfWork.GetRepository<Currency>()
                                         .GetByIdAsync(id);

            return _mapper.Map<CurrencyServiceModel>(model);
        }

        public async Task<SalaryTypeServiceModel> GetSalaryType(int id)
        {
            var model = await _unitOfWork.GetRepository<SalaryType>()
                                         .GetByIdAsync(id);

            return _mapper.Map<SalaryTypeServiceModel>(model);
        }

        public async Task<List<SalaryTypeServiceModel>> GetSalaryTypes()
        {
            var model = await _unitOfWork.GetRepository<SalaryType>()
                                         .GetAsync();

            return _mapper.Map<List<SalaryTypeServiceModel>>(model);
        }

        public async Task<List<SalaryTypeServiceModel>> GetSalaryTypesWithSalaries()
        {
            var model = await _unitOfWork.GetRepository<SalaryType>()
                                         .GetAsync(fetch: x => x.FetchMany(x => x.Salaries));

            return _mapper.Map<List<SalaryTypeServiceModel>>(model);
        }
    }
}
