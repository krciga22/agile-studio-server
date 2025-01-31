﻿
using AgileStudioServer.API.Dtos.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemTypeSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) || 
                from == typeof(Application.Models.BacklogItemType)
            ) && to == typeof(BacklogItemTypeSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            if (referenceHydrator == null)
            {
                throw new ReferenceHydratorRequiredException(this);
            }

            Application.Models.BacklogItemType? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (Application.Models.BacklogItemType)referenceHydrator.Hydrate(
                    from, typeof(Application.Models.BacklogItemType), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Application.Models.BacklogItemType)
            {
                model = (Application.Models.BacklogItemType)from;
            }

            Object? dto = null;
            if (model != null)
            {
                dto = new BacklogItemTypeSummaryDto(model.ID, model.Title, model.CreatedOn);
                Hydrate(model, dto, maxDepth, depth, referenceHydrator);
            }

            if (dto == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return dto;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var dto = (BacklogItemTypeSummaryDto)to;

            if (from is Application.Models.BacklogItemType)
            {
                var model = (Application.Models.BacklogItemType)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;
            }
        }
    }
}
