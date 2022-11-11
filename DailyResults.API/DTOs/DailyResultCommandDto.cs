namespace DailyResults.API.DTOs
{
    public class DailyResultCommandDto
    {
        public int Id { get; set; }
        public int ProductionLineId { get; set; }
        public decimal? OutputDay { get; set; }
        public decimal? OutputEve { get; set; }
        public decimal? OutputNight { get; set; }
        public decimal? StaffingDay { get; set; }
        public decimal? StaffingEve { get; set; }
        public decimal? StaffingNight { get; set; }
        public decimal? PrDay { get; set; }
        public decimal? PrEve { get; set; }
        public decimal? PrNight { get; set; }
        public decimal? UpdtDay { get; set; }
        public decimal? UpdtEve { get; set; }
        public decimal? UpdtNight { get; set; }
        public decimal? PdtDay { get; set; }
        public decimal? PdtEve { get; set; }
        public decimal? PdtNight { get; set; }
        public decimal? CoDay { get; set; }
        public decimal? CoEve { get; set; }
        public decimal? CoNight { get; set; }
        public int? StopsDay { get; set; }
        public int? StopsEve { get; set; }
        public int? StopsNight { get; set; }
        public decimal? WasteDay { get; set; }
        public decimal? WasteEve { get; set; }
        public decimal? WasteNight { get; set; }
        public DateTime Date { get; set; }
        public string? CommentDay { get; set; }
        public string? CommentEve { get; set; }
        public string? CommentNight { get; set; }


    }
}
