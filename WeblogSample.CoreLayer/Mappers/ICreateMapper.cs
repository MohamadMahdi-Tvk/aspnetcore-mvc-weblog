namespace WeblogSample.Service.Mappers;

public interface ICreateMapper<TCreateDto, TEntity>
{
    TEntity ToEntity(TCreateDto dto);
}
