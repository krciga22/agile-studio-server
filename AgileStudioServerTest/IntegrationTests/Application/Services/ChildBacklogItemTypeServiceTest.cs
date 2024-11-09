using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class ChildBacklogItemTypeServiceTest : AbstractServiceTest
    {
        private readonly ChildBacklogItemTypeService _childBacklogItemTypeService;

        public ChildBacklogItemTypeServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            ChildBacklogItemTypeService childBacklogItemTypeService) : base(dbContext, fixtures)
        {
            _childBacklogItemTypeService = childBacklogItemTypeService;
        }

        [Fact]
        public void Create_ReturnsChildBacklogItemType()
        {
            BacklogItemType backlogItemTypeStory = _Fixtures.CreateBacklogItemType("Story");
            BacklogItemType backlogItemTypeTask = _Fixtures.CreateBacklogItemType("Task");
            BacklogItemTypeSchema schema = _Fixtures.CreateBacklogItemTypeSchema();
            ChildBacklogItemType childBacklogItemType = new(
                backlogItemTypeTask.ID, backlogItemTypeStory.ID, schema.ID);

            childBacklogItemType = _childBacklogItemTypeService.Create(childBacklogItemType);

            Assert.NotNull(childBacklogItemType);
            Assert.True(childBacklogItemType.ID > 0);
        }

        [Fact]
        public void Get_ReturnsChildBacklogItemType()
        {
            var childBacklogItemType = _Fixtures.CreateChildBacklogItemType();

            var returnedChildBacklogItemType = _childBacklogItemTypeService.Get(childBacklogItemType.ID);

            Assert.NotNull(returnedChildBacklogItemType);
            Assert.Equal(childBacklogItemType.ID, returnedChildBacklogItemType.ID);
        }

        [Fact]
        public void GetByParentTypeId_ReturnsChildBacklogItemTypes()
        {
            var parentBacklogItemType = _Fixtures.CreateBacklogItemType("Parent Type");

            var childBacklogItemTypes = new List<ChildBacklogItemType>
            {
                _Fixtures.CreateChildBacklogItemType(
                    parentType: parentBacklogItemType
                ),
                _Fixtures.CreateChildBacklogItemType(
                    parentType: parentBacklogItemType
                )
            };

            List<ChildBacklogItemType> returnedChildBacklogItemTypes = _childBacklogItemTypeService
                .GetByParentTypeId(parentBacklogItemType.ID);

            Assert.Equal(childBacklogItemTypes.Count, returnedChildBacklogItemTypes.Count);
        }

        [Fact]
        public void GetByChildTypeId_ReturnsChildBacklogItemTypes()
        {
            var childBacklogItemType = _Fixtures.CreateBacklogItemType("Child Type");

            var childBacklogItemTypes = new List<ChildBacklogItemType>
            {
                _Fixtures.CreateChildBacklogItemType(
                    childType: childBacklogItemType
                ),
                _Fixtures.CreateChildBacklogItemType(
                    childType: childBacklogItemType
                )
            };

            List<ChildBacklogItemType> returnedChildBacklogItemTypes = _childBacklogItemTypeService
                .GetByChildTypeId(childBacklogItemType.ID);

            Assert.Equal(childBacklogItemTypes.Count, returnedChildBacklogItemTypes.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedChildBacklogItemType()
        {
            var childBacklogItemType = _Fixtures.CreateChildBacklogItemType();
            var backlogItemType = _Fixtures.CreateBacklogItemType("Updated Type");

            childBacklogItemType.ChildTypeID = backlogItemType.ID;
            childBacklogItemType = _childBacklogItemTypeService.Update(childBacklogItemType);

            Assert.NotNull(childBacklogItemType);
            Assert.Equal(backlogItemType.ID, childBacklogItemType.ChildTypeID);
        }

        [Fact]
        public void Delete_DeletesChildBacklogItemType()
        {
            var childBacklogItemType = _Fixtures.CreateChildBacklogItemType();

            _childBacklogItemTypeService.Delete(childBacklogItemType);

            childBacklogItemType = _childBacklogItemTypeService.Get(childBacklogItemType.ID);
            Assert.Null(childBacklogItemType);
        }
    }
}
