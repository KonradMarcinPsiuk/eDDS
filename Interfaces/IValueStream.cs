namespace Interfaces
{
    public interface IValueStream
    {
        public int Id { get; set; }
        public string ValueStreamName { get; set; }
        public int? PlantId { get; set; }
       
    }
}
