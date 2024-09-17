namespace GeekShopping.ProductAPI.Data.VOs
{
    public class GeneralReturnVO
    {

        public GeneralReturnVO() { }
        
        public List<string> Errors { get; set; } = new List<string>();

        public BaseVO? ResponseVO {  get; set; }
    }
}
