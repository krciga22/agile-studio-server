
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class UserSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.User) &&
                to == typeof(UserSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(UserSummaryDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.User)
            {
                var model = (Application.Models.User)from;
                dto = new UserSummaryDto(model.ID, model.FirstName, model.LastName);
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
            if (to is not UserSummaryDto)
            {
                throw new Exception("Unsupported to");
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
