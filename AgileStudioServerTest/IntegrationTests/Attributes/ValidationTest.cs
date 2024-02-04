using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServerTest.IntegrationTests.Attributes
{
    public class ValidationTest : DBTest
    {
        private readonly IServiceProvider? _ServiceProvider;

        public ValidationTest(DBContext dbContext, IServiceProvider? serviceProvider) : base(dbContext)
        {
            this._ServiceProvider = serviceProvider;
        }

        [Fact]
        public void BacklogItemTypeId_ForProjectId_IsValid()
        {
            var project = CreateProject();
            var backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);

            var attribute = new ValidBacklogItemTypeForBacklogItemPostDto();
            var backlogItem = new BacklogItemPostDto("Valid Backlog Item", project.ID, backlogItemType.ID);
            var result = attribute.GetValidationResult(backlogItem, CreateValidationContext(backlogItem));
            Assert.Null(result);
        }

        [Fact]
        public void BacklogItemTypeId_ForProjectId_IsInvalid()
        {
            var project = CreateProject();

            var otherBacklogItemTypeSchema = CreateBacklogItemTypeSchema();
            var backlogItemTypeInvalid = CreateBacklogItemType(otherBacklogItemTypeSchema);

            var attribute = new ValidBacklogItemTypeForBacklogItemPostDto();
            var backlogItem = new BacklogItemPostDto("Invalid Backlog Item", project.ID, backlogItemTypeInvalid.ID);
            var result = attribute.GetValidationResult(backlogItem, CreateValidationContext(backlogItem));
            Assert.NotNull(result);
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

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            project.BacklogItemTypeSchema = CreateBacklogItemTypeSchema();
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        private BacklogItemType CreateBacklogItemType(BacklogItemTypeSchema? backlogItemTypeSchema = null, string title = "Test Backlog Item Type")
        {
            if (backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

            var backlogItemType = new BacklogItemType(title)
            {
                BacklogItemTypeSchema = backlogItemTypeSchema
            };

            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }
    }
}
