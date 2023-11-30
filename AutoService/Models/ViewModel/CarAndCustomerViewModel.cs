namespace AutoService.Models.ViewModel
{
    public class CarAndCustomerViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
