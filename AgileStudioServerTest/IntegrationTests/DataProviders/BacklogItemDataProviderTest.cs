using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.DataProviders
{
    public class BacklogItemDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemDataProvider _dataProvider;

        public BacklogItemDataProviderTest(DBContext dbContext, BacklogItemDataProvider backlogItemDataProvider) : base(dbContext)
        {
            _dataProvider = backlogItemDataProvider;
        }

        [Fact]
        public void CreateBacklogItem_WithPostDto_ReturnsApiResource()
        {
            var project = CreateProject();
            var backlogItemType = CreateBacklogItemType();
            var dto = new BacklogItemPostDto("Test Backlog Item", project.ID, backlogItemType.ID);
            var apiResource = _dataProvider.Create(dto);
            Assert.IsType<BacklogItemApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItem_ById_ReturnsApiResource()
        {
            var backlogItem = CreateBacklogItem();

            var apiResource = _dataProvider.Get(backlogItem.ID);

            Assert.IsType<BacklogItemApiResource>(apiResource);
        }

        [Fact]
        public void ListForProject_ById_ReturnsApiResource()
        {
            var project = CreateProject();
            var backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);
            List<BacklogItem> backlogItems = new() {
                CreateBacklogItem(project, backlogItemType, "Test Backlog Item 1"),
                CreateBacklogItem(project, backlogItemType, "Test Backlog Item 2")
            };

            var apiResources = _dataProvider.ListForProjectId(project.ID);

            Assert.IsType<List<BacklogItemSubResource>>(apiResources);
            Assert.Equal(backlogItems.Count, apiResources.Count);
        }

        [Fact]
        public void UpdateBacklogItem_WithValidDto_ReturnsApiResource()
        {
            var backlogItem = CreateBacklogItem();

            var title = $"{backlogItem.Title} Updated";
            var dto = new BacklogItemPatchDto(title);

            var apiResource = _dataProvider.Update(backlogItem.ID, dto);
            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItem_WithValidId_ReturnsTrue()
        {
            var backlogItem = CreateBacklogItem();

            bool result = _dataProvider.Delete(backlogItem.ID);
            Assert.True(result);
        }

        private BacklogItem CreateBacklogItem(Project? project = null, BacklogItemType? backlogItemType = null, string title = "Test Backlog Item")
        {
            if(project is null)
            {
                project = CreateProject();
            }

            if(backlogItemType is null)
            {
                backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);
            }

            var backlogItem = new BacklogItem(title)
            {
                Project = project,
                BacklogItemType = backlogItemType
            };
            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }

        private BacklogItemType CreateBacklogItemType(BacklogItemTypeSchema? backlogItemTypeSchema = null, string title = "Test Backlog Item Type")
        {
            if (backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

            var backlogItemType = new BacklogItemType(title);
            backlogItemType.BacklogItemTypeSchema = backlogItemTypeSchema;
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        private Project CreateProject(BacklogItemTypeSchema? backlogItemTypeSchema = null, string title = "Test Project")
        {
            if (backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

            var project = new Project(title);
            project.BacklogItemTypeSchema = backlogItemTypeSchema;
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }
    }
}
