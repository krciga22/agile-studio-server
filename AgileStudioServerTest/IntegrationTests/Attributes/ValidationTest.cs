﻿using AgileStudioServer;
using AgileStudioServer.Attributes.Validation;
using AgileStudioServer.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServerTest.IntegrationTests.Attributes
{
    public class ValidationTest : DBTest
    {
        private readonly Fixtures _Fixtures;

        private readonly IServiceProvider? _ServiceProvider;

        public ValidationTest(
            DBContext dbContext, 
            Fixtures fixtures, 
            IServiceProvider? serviceProvider) : base(dbContext)
        {
            _Fixtures = fixtures;
            _ServiceProvider = serviceProvider;
        }

        [Fact]
        public void PostBacklogItem_WithBacklogItemTypeFromSameProjectsSchema_IsValid()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            var attribute = new ValidBacklogItemTypeForBacklogItemPostDto();
            var backlogItem = new BacklogItemPostDto("Valid Backlog Item", project.ID, backlogItemType.ID);

            var result = attribute.GetValidationResult(backlogItem, CreateValidationContext(backlogItem));

            Assert.Null(result);
        }

        [Fact]
        public void PostBacklogItem_WithBacklogItemTypeFromDifferentProjectsSchema_IsInvalid()
        {
            var project = _Fixtures.CreateProject();
            var otherBacklogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var backlogItemTypeInvalid = _Fixtures.CreateBacklogItemType(
                    backlogItemTypeSchema: otherBacklogItemTypeSchema);
            var attribute = new ValidBacklogItemTypeForBacklogItemPostDto();
            var backlogItem = new BacklogItemPostDto("Invalid Backlog Item", project.ID, backlogItemTypeInvalid.ID);

            var result = attribute.GetValidationResult(backlogItem, CreateValidationContext(backlogItem));

            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PostBacklogItem_WithSprintForSameProject_IsValid()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            var sprint = _Fixtures.CreateSprint(project: project);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item", 
                projectId: project.ID,
                backlogItemTypeId: backlogItemType.ID,
                sprintId: sprint.ID);

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));

            Assert.Null(result);
        }

        [Fact]
        public void PostBacklogItem_WithSprintForDifferentProject_IsInvalid()
        {
            var project1 = _Fixtures.CreateProject();
            var project2 = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                    backlogItemTypeSchema: project1.BacklogItemTypeSchema);
            var sprint = _Fixtures.CreateSprint(project: project2);

            var backlogItemPostDto = new BacklogItemPostDto(
                title: "Test Backlog Item", 
                projectId: project1.ID,
                backlogItemTypeId: backlogItemType.ID,
                sprintId: sprint.ID);

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPostDto, CreateValidationContext(backlogItemPostDto));
            
            Assert.IsType<ValidationResult>(result);
        }

        [Fact]
        public void PatchBacklogItem_WithSprintForSameProject_IsValid()
        {
            var project = _Fixtures.CreateProject();
            var sprint = _Fixtures.CreateSprint(project: project);
            var backlogItem = _Fixtures.CreateBacklogItem(
                project: project,
                sprint: sprint
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                sprintId: sprint.ID);

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.Null(result);
        }

        [Fact]
        public void PatchBacklogItem_WithSprintForDifferentProject_IsInvalid()
        {
            var project1 = _Fixtures.CreateProject();
            var project2 = _Fixtures.CreateProject();
            var sprint1 = _Fixtures.CreateSprint(project: project1);
            var sprint2 = _Fixtures.CreateSprint(project: project2);
            var backlogItem = _Fixtures.CreateBacklogItem(
                project: project1,
                sprint: sprint1
            );

            var backlogItemPatchDto = new BacklogItemPatchDto(
                id: backlogItem.ID,
                title: "Test Backlog Item",
                sprintId: sprint2.ID);

            var attribute = new ValidSprintForBacklogItem();
            var result = attribute.GetValidationResult(backlogItemPatchDto, CreateValidationContext(backlogItemPatchDto));

            Assert.IsType<ValidationResult>(result);
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            if (_ServiceProvider is null)
            {
                throw new Exception("Service Provider is null");
            }

            return new ValidationContext(
                instance: instance,
                serviceProvider: _ServiceProvider,
                items: null
            );
        }
    }
}
