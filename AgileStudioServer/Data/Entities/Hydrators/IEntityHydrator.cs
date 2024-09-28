namespace AgileStudioServer.Data.Entities.Hydrators
{
    public interface IEntityHydrator<TModel, TEntity>
    {
        public TEntity Hydrate(TModel model, TEntity? entity);
    }
}
