using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DTOs
{
    public class DailyTriggerQuestionDto
    {
        private int _type;

        [JsonInclude]
        public int Id { get; private set; }
        [Required]
        public string Question { get; set; } = null!;
        public string? Hint { get; set; }
       
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select field")]
        public int Field { get;  set; }


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select Type")]
        public int Type
        {
            get => _type;
            set
            {
                _type = value;
                TargetInt = 1;
            }
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select Target")]
        public int TargetInt { get;  set; }

        public List<ProductionLineDailyTriggerQuestionDto> ProductionLineDailyTriggerQuestions { get; set; }

        [JsonIgnore]
        public string TypeDescription => Types[_type];
        [JsonIgnore]

        public string TargetDescription => Targets[TargetInt];
        [JsonIgnore]

        public string FieldDescription => Fields[Field];
   

        public void AddNewLine(ProductionLineDailyTriggerQuestionDto link)
        {
            ProductionLineDailyTriggerQuestions.Add(link);
        }

        [JsonIgnore]
        public Dictionary<int, string> Types = new()
        {
            {1, "Low/Medium/High"},{2,"Yes/No"},{3,"Text/Numeric"}
        };

        [JsonIgnore]
        public Dictionary<int, string> Fields = new()
        {
            {1, "Safety"}, {2, "Quality"}, {3, "Other"}
        };

        [JsonIgnore]
        public Dictionary<int, string> Targets
        {
            get
            {
                return _type switch
                {
                    1 => new Dictionary<int, string>()
                    {
                        {1, "Low"}, {2, "Medium"}, {3, "High"}
                    },

                    2 => new Dictionary<int, string>()
                    {
                        {1, "Yes"}, {2, "No"}
                    },

                    _ => new Dictionary<int, string>()
                    {
                        {1,"N/A"}
                    }


                };
            }
        }

        public void CreateNew()
        {
            ProductionLineDailyTriggerQuestions = new List<ProductionLineDailyTriggerQuestionDto>();
        }

    }
}
