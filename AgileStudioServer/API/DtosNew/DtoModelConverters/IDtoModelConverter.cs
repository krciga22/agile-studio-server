namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public interface IDtoModelConverter<TDto, TModel>
    {
        public bool CanConvert(Type dto, Type model);
        public TModel ConvertToModel(TDto dto);
        public TDto ConvertToDto(TModel model);
    }
}
