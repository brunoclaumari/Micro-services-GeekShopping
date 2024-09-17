using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Data.VOs;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Base;
using GeekShopping.ProductAPI.Repositories;

namespace GeekShopping.ProductAPI.Services
{
    public class ProductService
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IRepository repo, IMapper mapper) 
        { 
            _repo = repo;
            _mapper = mapper;
            //_mapper = new MappingConfig().RegisterMaps().CreateMapper();
        }

        public async Task<List<ProductVO>> FindAll()
        {
            List<ProductVO> listaRetornoVO = new List<ProductVO>();
            var retorno = await _repo.FindAllProducts();
            if (retorno != null)
            {
                listaRetornoVO = _mapper.Map<List<ProductVO>>(retorno);
                //retorno.ForEach(r =>
                //{
                //    var vo = _mapper.Map<ProductVO>(r);
                //    listaRetornoVO.Add(vo);
                //});
            }

            return listaRetornoVO;
        }

        public async Task<ProductVO> FindProductById(long id)
        {
            var product = await _repo.FindProductById(id);

            return _mapper.Map<ProductVO>(product);
        }

        public async Task<GeneralReturnVO> Create(ProductVO productVO)
        {
            GeneralReturnVO retorno = new GeneralReturnVO();
            Product prod = _mapper.Map<Product>(productVO);

            _repo.Create(prod);
            bool success = await _repo.SaveChangesAsync();
            if(success)
            {
                productVO.Id = prod.Id;
                retorno.ResponseVO = productVO;                
            }
            else
            {
                retorno.Errors.Add("Ocorreu um erro ao criar o Produto. Verifique os dados.");
            }
            
            return retorno;
        }

        public async Task<GeneralReturnVO> Update(int id, ProductVO productVO)
        {
            GeneralReturnVO retorno = new GeneralReturnVO();
            Product? product = await _repo.FindProductById(id);
            if(product != null)
            {
                Product newProduct = _mapper.Map<Product>(productVO);
                _repo.Update(newProduct);
                bool success = await _repo.SaveChangesAsync();
                if (success)
                {
                    retorno.ResponseVO = productVO;
                }
                else
                {
                    retorno.Errors.Add("Ocorreu um erro ao atualizar o Produto. Verifique os dados.");
                }
            }
            else
            {
                retorno.Errors.Add($"Produto com id: {id} não existe");
            }

            return retorno;
        }

        public async Task<GeneralReturnVO> Delete(int id)
        {
            GeneralReturnVO retorno = new GeneralReturnVO();

            try
            {
                Product? product = await _repo.FindProductById(id);
                if (product != null)
                {
                    //Product newProduct = _mapper.Map<Product>(productVO);
                    _repo.Delete(product);
                    bool success = await _repo.SaveChangesAsync();
                    if (!success)
                    {
                        retorno.Errors.Add("Não foi possível apagar o Produto.");
                    }                    
                }
                else
                {
                    retorno.Errors.Add($"Produto com id: {id} não existe");
                }

            }
            catch (Exception e)
            {
                retorno.Errors.Add($"Ocorreu um erro inesperado ao tentar apagar o produto id: {id}");
            }

            return retorno;
        }
    }
}
