namespace AutoService.Models.ViewModel
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public PaginingInfo PaginingInfo { get; set; }
    }
}
