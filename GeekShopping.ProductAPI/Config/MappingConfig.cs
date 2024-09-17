using AutoMapper;
using GeekShopping.ProductAPI.Data.VOs;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Base;

namespace GeekShopping.ProductAPI.Config
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO,Product>().ReverseMap();
                config.CreateMap<BaseVO, BaseEntity>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
