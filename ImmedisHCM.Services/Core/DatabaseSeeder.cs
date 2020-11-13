using ImmedisHCM.Data.Entities;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Data.Infrastructure;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace ImmedisHCM.Services.Core
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<WebUser> _userManager;

        public DatabaseSeeder(IUnitOfWork unitOfWork, UserManager<WebUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public void Seed()
        {
            //WARNING: DO NOT CHANGE CALL ORDER
            int locationStartIndex = 0;
            SeedCountries();
            SeedCities();
            SeedCurrency();
            SeedScheduleType();
            SeedSalaryType();
            SeedCompanies();
            SeedJobs();
            SeedSalaries();
            SeedLocation();
            SeedEmergencyContacts(ref locationStartIndex);
            SeedDepartments(ref locationStartIndex);
            SeedEmployee(ref locationStartIndex);
            UpdateManagers();

        }

        private void SeedCountries()
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var countryRepo = _unitOfWork.GetRepository<Country>();

                var countryList = new List<Country>();
                if (!countryRepo.Entity.Any())
                {
                    countryList.Add(new Country { Name = "Bulgaria", ShortName = "BG" });
                    countryList.Add(new Country { Name = "Ireland", ShortName = "IR" });
                    countryList.Add(new Country { Name = "England", ShortName = "EN" });

                    countryRepo.AddRange(countryList);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedCities()
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var cityRepo = _unitOfWork.GetRepository<City>();
                var countryRepo = _unitOfWork.GetRepository<Country>();
                var countryList = countryRepo.Get();
                var cityList = new List<City>();
                if (!cityRepo.Entity.Any())
                {
                    var countryBulgaria = countryList.Single(x => x.Name == "Bulgaria");
                    cityList.Add(new City { Name = "Sofia", Country = countryBulgaria });
                    cityList.Add(new City { Name = "Varna", Country = countryBulgaria });
                    cityList.Add(new City { Name = "Burgas", Country = countryBulgaria });

                    var countryIreland = countryList.Single(x => x.Name == "Ireland");
                    cityList.Add(new City { Name = "Dublin", Country = countryIreland });
                    cityList.Add(new City { Name = "Cork", Country = countryIreland });
                    cityList.Add(new City { Name = "Galway", Country = countryIreland });

                    var countryEngland = countryList.Single(x => x.Name == "England");
                    cityList.Add(new City { Name = "Londol", Country = countryEngland });
                    cityList.Add(new City { Name = "Liverpool", Country = countryEngland });
                    cityList.Add(new City { Name = "Norwich", Country = countryEngland });

                    cityRepo.AddRange(cityList);

                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedCurrency()
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var currencyRepo = _unitOfWork.GetRepository<Currency>();

                if (!currencyRepo.Entity.Any())
                {
                    var currencyList = new List<Currency>
                    {
                        new Currency { Name = "BGN" },
                        new Currency { Name = "GBP" },
                        new Currency { Name = "EUR" }
                    };

                    currencyRepo.AddRange(currencyList);
                }

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedScheduleType()
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var scheduleTypeRepo = _unitOfWork.GetRepository<ScheduleType>();

                if (!scheduleTypeRepo.Entity.Any())
                {
                    var scheduleTypeList = new List<ScheduleType>();

                    var date = DateTime.UtcNow;
                    scheduleTypeList.Add(new ScheduleType { Name = "Morning Shift", Hours = 8, StartTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0) });
                    scheduleTypeList.Add(new ScheduleType { Name = "Evening Shift", Hours = 8, StartTime = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0) });
                    scheduleTypeList.Add(new ScheduleType { Name = "Night Shift", Hours = 8, StartTime = new DateTime(date.Year, date.Month, date.Day, 2, 0, 0) });

                    scheduleTypeRepo.AddRange(scheduleTypeList);
                }

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedSalaryType()
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var salaryTypeRepo = _unitOfWork.GetRepository<SalaryType>();

                if (!salaryTypeRepo.Entity.Any())
                {
                    var salaryTypeList = new List<SalaryType>
                    {
                        new SalaryType { Name = "Weely" },
                        new SalaryType { Name = "Hourly" },
                        new SalaryType { Name = "Monthly" }
                    };

                    salaryTypeRepo.AddRange(salaryTypeList);
                }

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedCompanies()
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var companyRepo = _unitOfWork.GetRepository<Company>();

                if (!companyRepo.Entity.Any())
                {
                    var companyList = new List<Company>
                    {
                        new Company { Name = "Ubisoft", LegalName = "Ubisoft LLC" },
                        new Company { Name = "Activision", LegalName = "Activision LLC" },
                        new Company { Name = "Bullfrog", LegalName = "Bullfrog LLC" }
                    };

                    companyRepo.AddRange(companyList);
                }

                _unitOfWork.Commit();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedJobs()
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var jobsRepo = _unitOfWork.GetRepository<Job>();
                var scheduleList = _unitOfWork.GetRepository<ScheduleType>().Get();

                if (!jobsRepo.Entity.Any() && scheduleList.Any())
                {
                    var jobsList = new List<Job>();
                    var dayShift = scheduleList.Single(x => x.Name == "Morning Shift");
                    jobsList.Add(new Job { Name = "Programmer", MinimalSalary = 900M, MaximumSalary = 1500M, ScheduleType = dayShift });
                    jobsList.Add(new Job { Name = "IT Specialist", MinimalSalary = 800M, MaximumSalary = 1600M, ScheduleType = dayShift });
                    jobsList.Add(new Job { Name = "Security", MinimalSalary = 900M, MaximumSalary = 1500M, ScheduleType = dayShift });

                    jobsRepo.AddRange(jobsList);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedLocation()
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var locationRepo = _unitOfWork.GetRepository<Location>();
                var cityList = _unitOfWork.GetRepository<City>().Get();

                if (!locationRepo.Entity.Any())
                {
                    string[] lines = ReadLinesFromFile("Addresses.txt");

                    if (lines.Length != 63)
                        throw new Exception("Number of addresses should be 63");

                    var postalCodes = new List<string>(64);
                    var addressLines = new List<string>(64);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        int postalCodeIndex = lines[i].IndexOf(" ");
                        postalCodes.Add(lines[i].Substring(0, postalCodeIndex));
                        addressLines.Add(lines[i][postalCodeIndex..]);
                    }


                    Random rnd = new Random();
                    var locationList = new List<Location>();
                    int cityCount = cityList.Count;

                    for (int i = 0; i < lines.Length; i++)
                        locationList.Add(new Location { AddressLine1 = addressLines[i], PostalCode = postalCodes[i], City = cityList[rnd.Next(0, cityCount)], });

                    locationRepo.AddRange(locationList);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedEmergencyContacts(ref int locationStartIndex)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var emergencyContactRepo = _unitOfWork.GetRepository<EmergencyContact>();
                var locationList = _unitOfWork.GetRepository<Location>().Get();

                if (!emergencyContactRepo.Entity.Any())
                {

                    string[] randomNames = ReadLinesFromFile("RandomNames.txt");
                    string[] randomPhoneNumbers = ReadLinesFromFile("RandomECPhoneNumbers.txt");


                    var firstNames = new List<string>();
                    var lastName = new List<string>();

                    for (int i = 0; i < randomNames.Length; i++)
                    {
                        int whitespaceIndex = randomNames[i].IndexOf(" ");
                        firstNames.Add(randomNames[i].Substring(0, whitespaceIndex));
                        lastName.Add(randomNames[i][whitespaceIndex..]);
                    }

                    var emergencyContactList = new List<EmergencyContact>();
                    for (int i = 0; i < 27; i++)
                    {
                        emergencyContactList.Add(new EmergencyContact { FirstName = firstNames[i], LastName = lastName[i], PhoneNumber = randomPhoneNumbers[i], Location = locationList[locationStartIndex] });
                        locationStartIndex++;
                    }

                    emergencyContactRepo.AddRange(emergencyContactList);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedDepartments(ref int locationStartIndex)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var departmentRepo = _unitOfWork.GetRepository<Department>();
                var companyList = _unitOfWork.GetRepository<Company>().Get();
                var locationList = _unitOfWork.GetRepository<Location>().Get();

                if (!departmentRepo.Entity.Any())
                {
                    var departmentList = new List<Department>
                    {
                        new Department { Name = "IT", Location = locationList[locationStartIndex++], Company = companyList[0] },
                        new Department { Name = "Financial", Location = locationList[locationStartIndex++], Company = companyList[0] },
                        new Department { Name = "Software", Location = locationList[locationStartIndex++], Company = companyList[0] },

                        new Department { Name = "IT", Location = locationList[locationStartIndex++], Company = companyList[1] },
                        new Department { Name = "Security", Location = locationList[locationStartIndex++], Company = companyList[1] },
                        new Department { Name = "HR", Location = locationList[locationStartIndex++], Company = companyList[1] },

                        new Department { Name = "IDK", Location = locationList[locationStartIndex++], Company = companyList[2] },
                        new Department { Name = "Management", Location = locationList[locationStartIndex++], Company = companyList[2] },
                        new Department { Name = "IT", Location = locationList[locationStartIndex++], Company = companyList[2] }
                    };

                    departmentRepo.AddRange(departmentList);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedSalaries()
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var salaryRepo = _unitOfWork.GetRepository<Salary>();
                var salaryTypeList = _unitOfWork.GetRepository<SalaryType>().Get();
                var currencyList = _unitOfWork.GetRepository<Currency>().Get();

                if (!salaryRepo.Entity.Any())
                {
                    var salaryList = new List<Salary>();

                    Random rnd = new Random();

                    int min = 800;
                    int max = 1600;

                    int typeCount = salaryTypeList.Count;
                    int currencyCount = currencyList.Count;

                    for (int i = 0; i < 27; i++)
                    {
                        salaryList.Add(new Salary { Amount = rnd.Next(min, max), SalaryType = salaryTypeList[rnd.Next(0, typeCount)], Currency = currencyList[rnd.Next(0, currencyCount)] });
                    }
                    salaryRepo.AddRange(salaryList);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void SeedEmployee(ref int locationStartIndex)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var employeeRepo = _unitOfWork.GetRepository<Employee>();
                var jobList = _unitOfWork.GetRepository<Job>().Get();
                var departmentList = _unitOfWork.GetRepository<Department>().Get();
                var salaryList = _unitOfWork.GetRepository<Salary>().Get();
                var locationList = _unitOfWork.GetRepository<Location>().Get();
                var emergencyContactList = _unitOfWork.GetRepository<EmergencyContact>().Get();

                if (!employeeRepo.Entity.Any())
                {
                    string[] emails = ReadLinesFromFile("RandomEmails.txt");
                    string[] randomNames = ReadLinesFromFile("RandomNames.txt");
                    string[] randomEmpPhoneNumbers = ReadLinesFromFile("RandomEmpPhoneNumbers.txt");
                    string password = "P@ssw0rd";
                    var employeeList = new List<Employee>();
                    var firstNames = new List<string>();
                    var lastName = new List<string>();

                    for (int i = 0; i < randomNames.Length; i++)
                    {
                        int whitespaceIndex = randomNames[i].IndexOf(" ");
                        firstNames.Add(randomNames[i].Substring(0, whitespaceIndex));
                        lastName.Add(randomNames[i][whitespaceIndex..]);
                    }

                    int departmentIndex = 0;
                    Random rnd = new Random();
                    for (int i = 0; i < 27; i += 3)
                    {
                        //3 per department -> 9 per company
                        var manager = new WebUser
                        {
                            Email = emails[i],
                            UserName = emails[i]
                        };
                        _userManager.CreateAsync(manager, password).Wait();

                        _userManager.AddToRoleAsync(manager, "Manager").Wait();

                        _userManager.CreateAsync(new WebUser { Email = emails[i + 1], UserName = emails[i + 1] }, password).Wait();
                        _userManager.CreateAsync(new WebUser { Email = emails[i + 2], UserName = emails[i + 2] }, password).Wait();

                        var department = departmentList[departmentIndex];
                        int randomNameIndex = rnd.Next(0, randomNames.Length);

                        employeeList.Add(new Employee
                        {
                            Email = emails[i],
                            PhoneNumber = randomEmpPhoneNumbers[i],
                            FirstName = firstNames[randomNameIndex],
                            LastName = lastName[randomNameIndex],
                            Department = department,
                            EmergencyContact = emergencyContactList[i],
                            HrId = Guid.NewGuid(),
                            Job = jobList[rnd.Next(0, jobList.Count)],
                            Location = locationList[locationStartIndex++],
                            Salary = salaryList[i],
                            HiredDate = DateTime.UtcNow
                        });

                        randomNameIndex = rnd.Next(0, randomNames.Length);
                        employeeList.Add(new Employee
                        {
                            Email = emails[i + 1],
                            PhoneNumber = randomEmpPhoneNumbers[i + 1],
                            FirstName = firstNames[randomNameIndex],
                            LastName = lastName[randomNameIndex],
                            Department = department,
                            EmergencyContact = emergencyContactList[i + 1],
                            HrId = Guid.NewGuid(),
                            Job = jobList[rnd.Next(0, jobList.Count)],
                            Location = locationList[locationStartIndex++],
                            Salary = salaryList[i + 1],
                            HiredDate = DateTime.UtcNow
                        });

                        randomNameIndex = rnd.Next(0, randomNames.Length);
                        employeeList.Add(new Employee
                        {
                            Email = emails[i + 2],
                            PhoneNumber = randomEmpPhoneNumbers[i + 2],
                            FirstName = firstNames[randomNameIndex],
                            LastName = lastName[randomNameIndex],
                            Department = department,
                            EmergencyContact = emergencyContactList[i + 2],
                            HrId = Guid.NewGuid(),
                            Job = jobList[rnd.Next(0, jobList.Count)],
                            Location = locationList[locationStartIndex++],
                            Salary = salaryList[i + 2],
                            HiredDate = DateTime.UtcNow
                        });

                        departmentIndex++;
                    }

                    employeeRepo.AddRange(employeeList);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private void UpdateManagers()
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var departmentRepo = _unitOfWork.GetRepository<Department>();
                var employeeRepo = _unitOfWork.GetRepository<Employee>();

                var managerUsers = _userManager.GetUsersInRoleAsync("Manager").Result
                                    .Select(x => x.Email)
                                    .ToList();

                if (managerUsers.Count > 0 && !employeeRepo.Entity.Any(x => x.Manager != null))
                {
                    var employees = employeeRepo.Get(fetch: x => x.Fetch(y => y.Department));
                    var managers = employees.Where(x => managerUsers.Any(y => x.Email.Contains(y))).ToList();
                    var departments = departmentRepo.Get();

                    for (int i = 0; i < departments.Count; i++)
                    {
                        departments[i].Manager = managers[i];
                        departmentRepo.Update(departments[i]);
                    }

                    int managerIndex = 0;
                    for (int i = 0; i < employees.Count; i += 3)
                    {
                        employees[i + 1].Manager = managers[managerIndex];
                        employeeRepo.Update(employees[i + 1]);
                        employees[i + 2].Manager = managers[managerIndex];
                        employeeRepo.Update(employees[i + 2]);
                        managerIndex++;
                    }
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private string[] ReadLinesFromFile(string filename)
        {
            return File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Core\SeedResources", filename));
        }
    }
}
