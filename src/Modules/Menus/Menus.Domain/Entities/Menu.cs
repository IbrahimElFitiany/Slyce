namespace Menus.Domain.Entities
{
    public class Menu
    {
        public Guid MenuCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
