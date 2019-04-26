namespace Minerva.Shared.Mappers
{
    public interface IMapper<TEntity, TModel>
    {
        TEntity ToEntity(TModel model);
        TModel ToModel(TEntity entity);
    }
}