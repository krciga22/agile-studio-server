
namespace AgileStudioServer.Application.Models.Hydrators
{
    /// <summary>
    /// Instantiates all model hydrators via dependency 
    /// injection, so that they can register themselves 
    /// with the HydratorRegistry.
    /// </summary>
    /// <seealso cref="HydratorRegistry"/>
    public class HydratorLoader
    {
        private BacklogItemHydrator _BacklogItemHydrator;
        private BacklogItemTypeHydrator _BacklogItemTypeHydrator;
        private BacklogItemTypeSchemaHydrator _BacklogItemTypeSchemaHydrator;
        private ProjectHydrator _ProjectHydrator;
        private ReleaseHydrator _ReleaseHydrator;
        private SprintHydrator _SprintHydrator;
        private UserHydrator _UserHydrator;
        private WorkflowHydrator _WorkflowHydrator;
        private WorkflowStateHydrator _WorkflowStateHydrator;

        public HydratorLoader(
            BacklogItemHydrator backlogItemHydrator,
            BacklogItemTypeHydrator backlogItemTypeHydrator,
            BacklogItemTypeSchemaHydrator backlogItemTypeSchemaHydrator,
            ProjectHydrator projectHydrator,
            ReleaseHydrator releaseHydrator,
            SprintHydrator sprintHydrator,
            UserHydrator userHydrator,
            WorkflowHydrator workflowHydrator,
            WorkflowStateHydrator workflowStateHydrator
        )
        {
            _BacklogItemHydrator = backlogItemHydrator;
            _BacklogItemTypeHydrator = backlogItemTypeHydrator;
            _BacklogItemTypeSchemaHydrator = backlogItemTypeSchemaHydrator;
            _ProjectHydrator = projectHydrator;
            _ReleaseHydrator = releaseHydrator;
            _SprintHydrator = sprintHydrator;
            _UserHydrator = userHydrator;
            _WorkflowHydrator = workflowHydrator;
            _WorkflowStateHydrator = workflowStateHydrator;
        }
    }
}
