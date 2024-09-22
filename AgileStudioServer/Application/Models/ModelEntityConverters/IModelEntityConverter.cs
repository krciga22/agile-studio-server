namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public interface IModelEntityConverter<TModel, TEntity>
    {
        public bool CanConvert(Type model, Type entity);
        public TModel ConvertToModel(TEntity entity);
        public TEntity ConvertToEntity(TModel model);
    }
}
