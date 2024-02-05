using AgileStudioServer;
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
        public void BacklogItemTypeId_ForProjectId_IsValid()
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
        public void BacklogItemTypeId_ForProjectId_IsInvalid()
        {
            var project = _Fixtures.CreateProject();
            var otherBacklogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var backlogItemTypeInvalid = _Fixtures.CreateBacklogItemType(
                    backlogItemTypeSchema: otherBacklogItemTypeSchema);
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
    }
}
