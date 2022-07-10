using Core.Entities;
using Core.Exceptions;
using Core.Paginator;
using Core.ViewModels;
using Core.ViewModels.ProcedureViewModels;
using Core.ViewModels.SpecializationViewModels;
using Core.ViewModels.User;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Test.Fixtures;

namespace WebApi.Test
{
    public class SpecializationControllerTest : IClassFixture<SpecializationControllerFixture>
    {

        private readonly SpecializationControllerFixture _fixture;

        public SpecializationControllerTest(SpecializationControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetSpecializationById_whenIdIsCorrect_thenStatusCodeOkReturned()
        {
            //  Arrange

            int specializationId = 2;

            _fixture.MockSpecializationService
                .Setup(service =>
                    service.GetSpecializationByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.ExpectedSpecialization);

            _fixture.MockMapperSpecializationViewModel
                .Setup(mapper =>
                    mapper.Map(It.IsAny<Specialization>()))
                .Returns(_fixture.ExpectedSpecializationViewModel);

            //  Act

            var result = await _fixture.MockController.GetSpecializationById(specializationId);

            //  Assert
            Assert.NotNull(result);
            Assert.IsType<SpecializationViewModel>(result);
        }

        [Fact]
        public async Task GetSpecializationById_whenIdIsIncorrect_thenStatusCodeErrorReturned()
        {
            //  Arrange
            int wrongId = 20;

            _fixture.MockSpecializationService
                .Setup(service =>
                    service.GetSpecializationByIdAsync
                        (It.Is<int>(id => wrongId == id)))
                .Throws<NotFoundException>();

            //  Act
            var result = _fixture.MockController.GetSpecializationById(wrongId);

            //  Assert
            Assert.NotNull(result);
            await Assert.ThrowsAsync<NotFoundException>(() => result);
        }


        [Fact]
        public async Task GetAllSpecializations_whenResultIsNotEmpty_thenStatusCodeOk()
        {
            _fixture.MockSpecializationService.Setup(service =>
                service.GetAllSpecializationsAsync(_fixture.TestParameters))
            .ReturnsAsync(_fixture.ExpectedSpecializations);

            _fixture.MockMapperPagedList.Setup(
                mapper
                    => mapper.Map(It.IsAny<PagedList<Specialization>>()))
                .Returns(_fixture.ExpectedViewModelSpecializations);

            var specializations = 
                await _fixture.MockController.GetSpecializations(_fixture.TestParameters);

            Assert.NotNull(specializations);
            Assert.NotEmpty(specializations.Entities);
        }

        [Fact]
        public async Task GetAllSpecializations_whenResultIsEmpty_thenStatusCodeOk()
        {
            PagedList<Specialization> emptyPagedList = new PagedList<Specialization>
                (new List<Specialization>(), 0, 1, 4);

            PagedReadViewModel<SpecializationViewModel> emptyViewModel = new PagedReadViewModel<SpecializationViewModel>
            {
                Entities = new List<SpecializationViewModel>(),
                CurrentPage = 1,
                TotalPages = 1,
                PageSize = 4,
                TotalCount = 0,
                HasPrevious = false,
                HasNext = false,
            };

            _fixture.MockSpecializationService.Setup(service =>
                service.GetAllSpecializationsAsync(_fixture.TestParameters))
            .ReturnsAsync(emptyPagedList);

            _fixture.MockMapperPagedList.Setup(
                mapper
                    => mapper.Map(It.IsAny<PagedList<Specialization>>()))
                .Returns(emptyViewModel);

            var specializations =
                await _fixture.MockController.GetSpecializations(_fixture.TestParameters);

            Assert.NotNull(specializations);
            Assert.Empty(specializations.Entities);
        }

        [Fact]
        public async Task AddSpecialization_whenDataIsCorrect_thenStatusCodeOk()
        {

            _fixture.MockMapperSpecialization.Setup(mapper =>
                mapper.Map(It.IsAny<SpecializationViewModel>()))
                    .Returns(_fixture.ExpectedSpecialization);

            _fixture.MockSpecializationService.Setup(service =>
                service.AddSpecializationAsync(It.IsAny<Specialization>()))
                    .ReturnsAsync(_fixture.ExpectedSpecialization)
                    .Verifiable();

            var result = 
                await _fixture.MockController.AddSpecialization(_fixture.ExpectedSpecializationViewModel);

            _fixture.MockSpecializationService.Verify(service =>
                service.AddSpecializationAsync(_fixture.ExpectedSpecialization), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<Specialization>(_fixture.ExpectedSpecialization);
        }

        [Fact]
        public async Task DeleteSpecialization_whenIdIsCorrect_ThenStatusCodeOk()
        {
            int specializationId = 2;

            _fixture.MockSpecializationService.Setup(service =>
                service.DeleteSpecializationAsync(It.IsAny<int>()))
            .Returns(Task.FromResult<object?>(null))
            .Verifiable();

            await _fixture.MockController.DeleteSpecialization(specializationId);

            _fixture.MockSpecializationService
                .Verify(m => m.DeleteSpecializationAsync(specializationId), Times.Once);
        }

        [Fact]
        public async Task DeleteSpecialization_whenIdIsInvalid_ThenStatusCodeError()
        {
            var specializationId = 40;

            _fixture.MockSpecializationService.Setup(service =>
                service.DeleteSpecializationAsync(specializationId))
            .Throws<NotFoundException>();

            var result =  _fixture.MockController.DeleteSpecialization(specializationId);

            await Assert.ThrowsAsync<NotFoundException>
                (() => result);
        }

        [Fact]
        public async Task UpdateSpecialization_whenSpecializationIsCorrect_thenStatusCodeOk()
        {
            int specializationId = 2;

            _fixture.MockMapperSpecialization.Setup(mapper =>
                mapper.Map(It.IsAny<SpecializationViewModel>()))
            .Returns(_fixture.ExpectedSpecialization);

            _fixture.MockSpecializationService.Setup( service =>
                service.UpdateSpecializationAsync(
                    It.IsAny<int>(), 
                    It.IsAny<Specialization>()))
            .Returns(Task.FromResult<object?>(null))
            .Verifiable();

            await _fixture.MockController.UpdateSpecialization(specializationId,_fixture.ExpectedSpecializationViewModel);

            _fixture.MockSpecializationService.Verify(service =>
                service.UpdateSpecializationAsync(specializationId, _fixture.ExpectedSpecialization), Times.Once);
        }

        [Fact]
        public async Task UpdateSpecialization_whenSpecializationNotFound_thenReturnStatusCodeError()
        {
            int specializationId = 90;

            _fixture.MockMapperSpecialization.Setup(mapper =>
                mapper.Map(It.IsAny<SpecializationViewModel>()))
            .Returns(_fixture.ExpectedSpecialization);

            _fixture.MockSpecializationService.Setup(service =>
                service.UpdateSpecializationAsync(specializationId, _fixture.ExpectedSpecialization))
            .Throws<NotFoundException>();

            var result = _fixture.MockController.UpdateSpecialization(specializationId, _fixture.ExpectedSpecializationViewModel);

            await Assert.ThrowsAsync<NotFoundException>
                (() => result);
        }

        [Fact]
        public async Task GetEmployees_whenEmployeesExist_thenReturnEmployees()
        {
            _fixture.MockSpecializationService.Setup(service =>
                service.GetEmployeesAsync())
            .ReturnsAsync(_fixture.ExpectedEmployees);

            _fixture.MockMapperUserList.Setup(mapper =>
                mapper.Map(It.IsAny<IEnumerable<User>>()))
            .Returns(_fixture.ExpectedEmployeesViewModel);

            var result = await _fixture.MockController.GetEmployees();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<UserReadViewModel>>(result);
        }

        [Fact]
        public async Task GetEmployees_whenEmployeesNotExist_thenReturnNothing()
        {
            IEnumerable<User> emptyEmployees = new List<User>();
            IEnumerable<UserReadViewModel> emptyEmployeesViewModels = new List<UserReadViewModel>();

            _fixture.MockSpecializationService.Setup(service =>
                service.GetEmployeesAsync())
            .ReturnsAsync(emptyEmployees);

            _fixture.MockMapperUserList.Setup(mapper =>
                mapper.Map(It.IsAny<IEnumerable<User>>()))
            .Returns(emptyEmployeesViewModels);

            var result = await _fixture.MockController.GetEmployees();

            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<IEnumerable<UserReadViewModel>>(result);
        }

        [Fact]
        public async Task GetSpecializationsProcedures_whenSpecializationExists()
        {
            int specializationId = 2;

            _fixture.MockSpecializationService.Setup(service =>
                service.GetSpecializationProcedures(It.Is<int>(id => specializationId == id)))
            .ReturnsAsync(_fixture.ExpectedProcedures);

            _fixture.MockMapperListProcedureViewModel.Setup(mapper =>
                mapper.Map(It.IsAny<IEnumerable<Procedure>>()))
            .Returns(_fixture.ExpectedProceduresViewModel);

            var result = await _fixture.MockController.GetSpecializationProcedures(specializationId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<ProcedureReadViewModel>>(result);
        }

        [Fact]
        public async Task RemoveProcedure_whenSpecializationExists_thenReturnNoContent()
        {
            int specializationId = 2;
            int procedureId = 2;

            var result = await _fixture.MockController.RemoveProcedureFromSpecialization(specializationId, procedureId);

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

            _fixture.MockSpecializationService.Verify(service => 
                service.RemoveProcedureFromSpecialization(specializationId, procedureId), Times.Once);
        }
    }
}
