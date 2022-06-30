﻿using AutoFixture;
using AutoFixture.AutoMoq;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Models.Finance;
using Core.ViewModels.SalaryViewModel;
using Moq;
using WebApi.AutoMapper.Interface;
using WebApi.Controllers;

namespace WebApi.Test.Fixtures
{
    public class FinancialControllerFixture
    {
        public FinancialControllerFixture()
        {
            var fixture =
                new Fixture().Customize(new AutoMoqCustomization());

            MockFinancialService = fixture.Freeze<Mock<IFinancialService>>();
            MockUserService = fixture.Freeze<Mock<IUserService>>();
            MockSalaryViewModel = fixture.Freeze<Mock<IViewModelMapper<Salary, SalaryViewModel>>>();
            MockSalary = fixture.Freeze<Mock<IViewModelMapper<SalaryViewModel, Salary>>>();
            MockListSalaryViewModels = fixture.Freeze<Mock<IViewModelMapper<IEnumerable<Salary>, IEnumerable<SalaryViewModel>>>>();
            MockListEmployees = fixture.Freeze<Mock<IViewModelMapper<IEnumerable<User>, IEnumerable<EmployeeViewModel>>>>();
            MockDate = fixture.Freeze<Mock<IViewModelMapper<DateViewModel, Date>>>();
            MockFinancialStatementViewModel = fixture.Freeze<Mock<IViewModelMapper<FinancialStatementList, FinancialStatementViewModel>>>();

            MockFinancialController = new FinancialController(
                MockFinancialService.Object,
                MockUserService.Object,
                MockSalaryViewModel.Object,
                MockSalary.Object,
                MockListSalaryViewModels.Object,
                MockListEmployees.Object,
                MockDate.Object,
                MockFinancialStatementViewModel.Object);
        }

        public FinancialController  MockFinancialController{ get; }
        public Mock<IFinancialService> MockFinancialService { get; }
        public Mock<IUserService> MockUserService { get; }
        public Mock<IViewModelMapper<Salary, SalaryViewModel>> MockSalaryViewModel { get; }
        public Mock<IViewModelMapper<SalaryViewModel, Salary>> MockSalary { get; }
        public Mock<IViewModelMapper<IEnumerable<Salary>, IEnumerable<SalaryViewModel>>> MockListSalaryViewModels { get; }
        public Mock<IViewModelMapper<IEnumerable<User>, IEnumerable<EmployeeViewModel>>> MockListEmployees { get; }
        public Mock<IViewModelMapper<DateViewModel, Date>> MockDate { get; }
        public Mock<IViewModelMapper<FinancialStatementList, FinancialStatementViewModel>> MockFinancialStatementViewModel { get; }
    }
}
