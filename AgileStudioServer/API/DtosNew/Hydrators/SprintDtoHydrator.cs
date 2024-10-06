﻿
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class SprintDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.Sprint) &&
                to == typeof(SprintDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(SprintDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.Sprint && referenceHydrator != null)
            {
                var model = (Application.Models.Sprint)from;

                var projectSummaryDto = (ProjectSummaryDto)referenceHydrator.Hydrate(
                    model.Project, typeof(ProjectSummaryDto), maxDepth, depth
                );

                dto = new SprintDto(model.ID, model.SprintNumber, projectSummaryDto, model.CreatedOn);
                Hydrate(model, dto, maxDepth, depth, referenceHydrator);
            }

            if (dto == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return dto;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (to is not SprintDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (SprintDto)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.Sprint)
            {
                var model = (Application.Models.Sprint)from;
                dto.ID = model.ID;
                dto.SprintNumber = model.SprintNumber;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;
                dto.StartDate = model.StartDate;
                dto.EndDate = model.EndDate;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Project = (ProjectSummaryDto)referenceHydrator.Hydrate(
                        model.Project, typeof(ProjectSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedBy != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedBy, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
