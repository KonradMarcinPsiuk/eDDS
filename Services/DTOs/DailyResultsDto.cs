using System.Text.Json.Serialization;

namespace DTOs;

public class DailyResultsDto
{
    public int Id { get; set; }
    public int ProductionLineId { get; set; }
    public ProductionLineDto ProductionLine { get; set; } = null!;
    public decimal? OutputDay { get; set; }
    public decimal? OutputEve { get; set; }
    public decimal? OutputNight { get; set; }
    [JsonIgnore]
    public decimal? TotalOutput => (OutputDay ?? 0) + (OutputEve ?? 0 ) + (OutputNight ?? 0);
    public decimal? StaffingDay { get; set; }
    public decimal? StaffingEve { get; set; }
    public decimal? StaffingNight { get; set; }
    [JsonIgnore]
    public decimal? TotalStaffing => (StaffingDay ?? 0) + (StaffingEve ?? 0) + (StaffingNight ?? 0);
    public decimal? PrDay { get; set; }
    public decimal? PrEve { get; set; }
    public decimal? PrNight { get; set; }
    [JsonIgnore]
    public string? TotalPr => WeighedResultCalculationToString((PrDay ?? 0), (PrEve ??0 ), (PrNight?? 0),true);
    [JsonIgnore]
    public string TotalPrClass
    {
        get
        {
            var v= WeighedResultCalculation(PrDay, PrEve, PrNight);
            if (v == null || ProductionLine.TargetPr==null) return "";
            return v >= ProductionLine.TargetPr ? "positive" : "negative";
        }
    }

    public decimal? UpdtDay { get; set; }
    public decimal? UpdtEve { get; set; }
    public decimal? UpdtNight { get; set; }
    [JsonIgnore]
    public string? TotalUpdt => WeighedResultCalculationToString(UpdtDay ?? 0, UpdtEve??0, UpdtNight ??0,true);
    [JsonIgnore]
    public string TotalUpdtClass
    {
        get
        {
            var v= WeighedResultCalculation(UpdtDay??0, UpdtEve??0, UpdtNight??0);
            if (v == null || ProductionLine.TargetUpdt==null) return "";
            return v > ProductionLine.TargetUpdt ? "negative" : "positive";
        }
    }
    public decimal? PdtDay { get; set; }
    public decimal? PdtEve { get; set; }
    public decimal? PdtNight { get; set; }
    [JsonIgnore]
    public string? TotalPdt => WeighedResultCalculationToString(PdtDay??0, PdtEve??0, PdtNight??0,true);
    [JsonIgnore]
    public string TotalPdtClass
    {
        get
        {
            var v= WeighedResultCalculation(PdtDay??0, PdtEve??0, PdtNight??0);
            if (v == null || ProductionLine.TargetPdt==null) return "";
            return v > ProductionLine.TargetPdt ? "negative" : "positive";
        }
    }
    public decimal? CoDay { get; set; }
    public decimal? CoEve { get; set; }
    public decimal? CoNight { get; set; }
    [JsonIgnore]
    public string? TotalCo => WeighedResultCalculationToString(CoDay??0, CoEve??0, CoNight??0,true);
    [JsonIgnore]
    public string TotalCoClass
    {
        get
        {
            var v= WeighedResultCalculation(CoDay??0, CoEve??0, CoNight??0);
            if (v == null || ProductionLine.TargetCo==null) return "";
            return v > ProductionLine.TargetCo ? "negative" : "positive";
        }
    }
    public int? StopsDay { get; set; }
    public int? StopsEve { get; set; }
    public int? StopsNight { get; set; }
    [JsonIgnore]
    public int? TotalStops => (StopsDay ?? 0) + (StopsEve ?? 0) + (StopsNight ?? 0);
    
    public decimal? WasteDay { get; set; }
    public decimal? WasteEve { get; set; }
    public decimal? WasteNight { get; set; }
    [JsonIgnore]
    public decimal? TotalWaste => (WasteDay ?? 0) + (WasteEve ?? 0) + (WasteNight ?? 0);
    [JsonIgnore]
    public string TotalWasteClass
    {
        get
        {
            if (TotalWaste == null || ProductionLine.TargetWaste==null) return "";
            return TotalWaste > ProductionLine.TargetWaste ? "negative" : "positive";
        }
    }
    public DateTime Date { get; set; }
    public string? CommentDay { get; set; }
    public string? CommentEve { get; set; }
    public string? CommentNight { get; set; }

    private string WeighedResultCalculationToString(decimal? day, decimal? eve, decimal? night, bool isPercent)
    {
        if (TotalStaffing != null)
        {
          
            var sum = WeighedResultCalculation( day,  eve,  night);
            if (sum == null) return "No data";
            string per = isPercent ? " %" : "";
            return Math.Round((decimal) sum, 2).ToString()+per;
        }

        return null;
    }

    private decimal? WeighedResultCalculation(decimal? day, decimal? eve, decimal? night)
    {
        if (TotalStaffing != null)
        {
            var sDay = StaffingDay ?? 0;
            var sEve = StaffingEve ?? 0;
            var sNight = StaffingNight ?? 0;
            var wDay = sDay>0 ? sDay / TotalStaffing : 0;
            var wEve = sEve > 0 ? sEve / TotalStaffing : 0;
            var wNight = sNight>0 ? sNight/TotalStaffing : 0;
            var weighedDay = day * wDay;
            var weighedEve = eve * wEve;
            var weighedNight = night * wNight;
            var sum = weighedDay + weighedEve + weighedNight;
            if (sum == null) return null;
            return Math.Round((decimal) sum, 2);
        }
        return null;
    }
}