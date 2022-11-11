namespace Interfaces
{
    public interface ILevelNavigation
    {
        public IPlant? SelectedPlant { get; set; }
        public IValueStream? SelectedValueStream { get; set; }
        public IDepartment? SelectedDepartment { get; set; }
        public IProductionLine? SelectedProductionLine { get; set; }
        public ILineArea? SelectedArea { get; set; }
    }
}
