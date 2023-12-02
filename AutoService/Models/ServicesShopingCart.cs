namespace AutoService.Models
{
    public class ServicesShopingCart
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int ServiceTypeId { get; set; }
        public virtual Car Car { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
