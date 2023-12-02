namespace AutoService.Models.ViewModel
{
    public class CarServiceViewModel
    {
        public Car Car { get; set; }
        public ServiceHeader ServiceHeader { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
        public List<ServiceType>   ServiceTypes  { get; set; }
        public List<ServicesShopingCart> ServiceShopping  { get; set; }
    }
}
