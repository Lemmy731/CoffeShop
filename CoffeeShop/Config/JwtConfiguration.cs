namespace CoffeeShop.Config
{
    public class JwtConfiguration
    {
        public string ValidAudience { get; set; }       
        public string ValidIssuer { get; set; }     
        public string Secret { get; set; }      
    }
}
