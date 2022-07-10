using AutoFixture;
using AutoFixture.AutoMoq;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Paginator;
using Core.Paginator.Parameters;
using Core.ViewModels;
using Core.ViewModels.ProcedureViewModels;
using Core.ViewModels.SpecializationViewModels;
using Core.ViewModels.User;
using Moq;
using WebApi.AutoMapper.Interface;
using WebApi.Controllers;

namespace WebApi.Test.Fixtures
{
    public class SpecializationControllerFixture
    {
        public SpecializationControllerFixture()
        {
            var fixture =
                new Fixture().Customize(new AutoMoqCustomization());

            TestParameters = GenerateParameters();
            ExpectedViewModelSpecializations = GenerateViewModelSpecializations();
            ExpectedSpecializations = GenerateSpecializations();
            ExpectedSpecialization = GenerateSpecialization();
            ExpectedSpecializationViewModel = GenerateSpecializationViewModel();
            ExpectedEmployees = GenerateEmployees();
            ExpectedEmployeesViewModel = GenerateEmployeesViewModel();
            ExpectedProcedures = GenerateProcedures();
            ExpectedProceduresViewModel = GenerateProceduresViewModel();    

            MockSpecializationService = fixture.Freeze<Mock<ISpecializationService>>();

            MockMapperSpecialization = fixture.Freeze<Mock<IViewModelMapper<SpecializationViewModel, Specialization>>>();

            MockMapperSpecializationViewModel = fixture.Freeze<Mock<IViewModelMapper<Specialization, SpecializationViewModel>>>();

            MockMapperListProcedureViewModel = fixture.Freeze<Mock<IEnumerableViewModelMapper<IEnumerable<Procedure>, IEnumerable<ProcedureReadViewModel>>>>();

            MockMapperPagedList = fixture.Freeze<Mock<IViewModelMapper<PagedList<Specialization>, PagedReadViewModel<SpecializationViewModel>>>>();

            MockMapperUserList = fixture.Freeze<Mock<IEnumerableViewModelMapper<IEnumerable<User>, IEnumerable<UserReadViewModel>>>>();

            MockController = new SpecializationController(
                    MockSpecializationService.Object,
                    MockMapperSpecialization.Object,
                    MockMapperSpecializationViewModel.Object,
                    MockMapperListProcedureViewModel.Object,
                    MockMapperPagedList.Object,
                    MockMapperUserList.Object
                );
        }

        public SpecializationController MockController { get; }
        public Mock<ISpecializationService> MockSpecializationService { get; }
        public Mock<IViewModelMapper<SpecializationViewModel, Specialization>> MockMapperSpecialization { get; }
        public Mock<IViewModelMapper<Specialization, SpecializationViewModel>> MockMapperSpecializationViewModel { get; }
        public Mock<IEnumerableViewModelMapper<IEnumerable<Procedure>, IEnumerable<ProcedureReadViewModel>>> MockMapperListProcedureViewModel { get; }
        public Mock<IViewModelMapper<PagedList<Specialization>, PagedReadViewModel<SpecializationViewModel>>> MockMapperPagedList { get; }
        public Mock<IEnumerableViewModelMapper<IEnumerable<User>, IEnumerable<UserReadViewModel>>> MockMapperUserList { get; }

        public SpecializationParameters TestParameters { get; set; }
        public PagedReadViewModel<SpecializationViewModel> ExpectedViewModelSpecializations { get; set; }
        public PagedList<Specialization> ExpectedSpecializations { get; set; }
        public Specialization ExpectedSpecialization { get; set; }
        public SpecializationViewModel ExpectedSpecializationViewModel { get; set; }
        public IEnumerable<User> ExpectedEmployees { get; set; }
        public IEnumerable<UserReadViewModel> ExpectedEmployeesViewModel { get; set; }
        public IEnumerable<Procedure> ExpectedProcedures { get; set; }
        public IEnumerable<ProcedureReadViewModel> ExpectedProceduresViewModel { get; set; }

        private PagedReadViewModel<SpecializationViewModel> GenerateViewModelSpecializations()
        {
            var specializations = new List<SpecializationViewModel>
            {
                new SpecializationViewModel() {Id = 0, Name = "surgeon"},
                new SpecializationViewModel() {Id = 1, Name = "worker"}
            };

            return new PagedReadViewModel<SpecializationViewModel>
            {
                CurrentPage = 1,
                TotalPages = 1,
                PageSize = 4,
                TotalCount = specializations.Count,
                HasPrevious = false,
                HasNext = false,
                Entities = specializations
            };
        }
        private SpecializationParameters GenerateParameters()
        {
            return new SpecializationParameters
            {
                PageNumber = 1,
                PageSize = 4
            };
        }

        private PagedList<Specialization> GenerateSpecializations()
        {
            var specializations = new List<Specialization>
            {
                new Specialization() {Id = 0, Name = "surgeon"},
                new Specialization() {Id = 1, Name = "worker"}
            };

            return new PagedList<Specialization>(specializations, specializations.Count, 1, 4);
        }

        private Specialization GenerateSpecialization()
        {
            return new Specialization
            {
                Id = 2,
                Name = "surgeon"
            };
        }

        private SpecializationViewModel GenerateSpecializationViewModel()
        {
            return new SpecializationViewModel
            {
                Id = 2,
                Name = "surgeon"
            };
        }

        private IEnumerable<User> GenerateEmployees()
        {
            return new List<User>
            {
                new User
                {
                    FirstName = "Karen",
                    LastName = "Errgghh",
                    Email = "kk220@gmail.com"
                },
                new User
                {
                    FirstName = "Kujo",
                    LastName = "Sasuw",
                    Email = "gH220@gmail.com"
                }
            };
        }

        private IEnumerable<UserReadViewModel> GenerateEmployeesViewModel()
        {
            return new List<UserReadViewModel>
            {
                new UserReadViewModel
                {
                    FirstName = "Karen",
                    LastName = "Errgghh",
                    Email = "kk220@gmail.com"
                },
                new UserReadViewModel
                {
                    FirstName = "Kujo",
                    LastName = "Sasuw",
                    Email = "gH220@gmail.com"
                }
            };
        }

        private IEnumerable<Procedure> GenerateProcedures()
        {
            return new List<Procedure>
            {
                new Procedure()
                {
                    Id = 2,
                    Name = "ears cleaning",
                    Description = "ears cleaning description",
                    DurationInMinutes = 35,
                    
                },
                new Procedure()
                {
                    Id = 4,
                    Name = "exorcism",
                    Description = "get all evil spirits out",
                    DurationInMinutes = 60,
                    Cost = 1000
                }
            };
        }

        private IEnumerable<ProcedureReadViewModel> GenerateProceduresViewModel()
        {
            return new List<ProcedureReadViewModel>
            {
                new ProcedureReadViewModel
                {
                    Id = 2,
                    Name = "ears cleaning",
                    Description = "ears cleaning description",
                    DurationInMinutes = 35,

                },
                new ProcedureReadViewModel
                {
                    Id = 4,
                    Name = "exorcism",
                    Description = "get all evil spirits out",
                    DurationInMinutes = 60,
                    Cost = 1000
                }
            };
        }
    }
}
