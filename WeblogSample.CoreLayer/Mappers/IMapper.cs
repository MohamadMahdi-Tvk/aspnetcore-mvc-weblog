namespace WeblogSample.Service.Mappers;

public interface IMapper<TEntity, TDto>
{
    TDto ToDto(TEntity entity);
}
