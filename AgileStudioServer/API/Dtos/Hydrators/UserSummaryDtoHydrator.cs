
using AgileStudioServer.API.Dtos.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class UserSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) || 
                from == typeof(Application.Models.User)
            ) && to == typeof(UserSummaryDto);
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

            Application.Models.User? model = null;
            if (from is int)
            {
                model = (Application.Models.User)referenceHydrator.Hydrate(
                    from, typeof(Application.Models.User), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Application.Models.User)
            {
                model = (Application.Models.User)from;
            }

            Object? dto = null;
            if (model != null)
            {
                dto = new UserSummaryDto(model.ID, model.FirstName, model.LastName);
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

            var dto = (UserSummaryDto)to;

            if (from is Application.Models.User)
            {
                var model = (Application.Models.User)from;
                dto.ID = model.ID;
                dto.FirstName = model.FirstName;
                dto.LastName = model.LastName;
            }
        }
    }
}
