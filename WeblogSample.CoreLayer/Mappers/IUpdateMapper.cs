namespace WeblogSample.Service.Mappers;

public interface IUpdateMapper<TUpdateDto, TEntity>
{
    void UpdateEntity(TUpdateDto dto, TEntity entity);
}