﻿using AgileStudioServer.API.Dtos;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public class BacklogItemDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemDataProvider _DataProvider;

        public BacklogItemDataProviderTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            BacklogItemDataProvider backlogItemDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = backlogItemDataProvider;
        }

        [Fact]
        public void CreateBacklogItem_WithPostDto_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var workflowState = _Fixtures.CreateWorkflowState();
            var sprint = _Fixtures.CreateSprint(project: project);
            var release = _Fixtures.CreateRelease(project: project);
            var dto = new BacklogItemPostDto("Test Backlog Item", project.ID, backlogItemType.ID, workflowState.ID, sprint.ID, release.ID);

            var apiResource = _DataProvider.Create(dto);

            Assert.IsType<BacklogItemApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItem_ById_ReturnsApiResource()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            var apiResource = _DataProvider.Get(backlogItem.ID);

            Assert.IsType<BacklogItemApiResource>(apiResource);
        }

        [Fact]
        public void ListForProject_ById_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                backlogItemTypeSchema: project.BacklogItemTypeSchema);

            List<BacklogItem> backlogItems = new() {
                _Fixtures.CreateBacklogItem(
                    title: "Test Backlog Item 1",
                    project: project,
                    backlogItemType: backlogItemType),
                _Fixtures.CreateBacklogItem(
                    title: "Test Backlog Item 2",
                    project: project,
                    backlogItemType: backlogItemType)
            };

            var apiResources = _DataProvider.ListForProjectId(project.ID);

            Assert.IsType<List<BacklogItemApiResource>>(apiResources);
            Assert.Equal(backlogItems.Count, apiResources.Count);
        }

        [Fact]
        public void UpdateBacklogItem_WithValidDto_ReturnsApiResource()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();
            var title = $"{backlogItem.Title} Updated";
            var dto = new BacklogItemPatchDto(backlogItem.ID, title, backlogItem.WorkflowState.ID);

            var apiResource = _DataProvider.Update(backlogItem.ID, dto);

            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItem_WithValidId_ReturnsTrue()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            _DataProvider.Delete(backlogItem.ID);

            var result = _DataProvider.Get(backlogItem.ID);
            Assert.Null(result);
        }
    }
}
