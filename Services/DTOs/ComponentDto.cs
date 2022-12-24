using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DataLayer.Models.Enums;

namespace DTOs
{
    public class ComponentDto
    {
        private string? _description = null!;
        public int Id { get; set; }

        public int? SapNumber { get; set; }
        public string? Manufacturer { get; set; } = null!;
        public string? PartNumber { get; set; } = null!;

        public string? Description
        {
            get => _description;
            set
            {
                _description = value; 
                foreach(var item in ComponentActions)
                    item.Description = value;
            }
        }

        public int Type { get; set; }
    
        //checks
        public bool PmRequired { get; set; }
        public bool PmInPlace { get; set; }
        [JsonIgnore]
        public bool PmMissing => PmRequired && PmInPlace == false;
        [JsonIgnore]
        public string PmMissingDescription
        {
            get
            {
                return PmMissing switch
                {
                    true => "Yes",
                    _ => "No"
                };

            }
        }

        public bool AmRequired { get; set; }
        public bool AmInPlace { get; set; }
        [JsonIgnore]
        public bool AmMissing => AmRequired && AmInPlace == false;
        [JsonIgnore]
        public string AmMissingDescription
        {
            get
            {
                return AmMissing switch
                {
                    true => "Yes",
                    _ => "No"
                };

            }
        }

        public bool WearComponent { get; set; }
        public string WearComponentDescription
        {
            get
            {
                return WearComponent switch
                {
                    true => "Yes",
                    _ => "No"
                };
            }
        }

        public bool SpareRequired { get; set; }
        public bool SpareAvailable { get; set; }
        [JsonIgnore]
        public bool SpareMissing => SpareRequired && SpareAvailable == false;
        [JsonIgnore]
        public string SpareMissingDescription
        {
            get
            {
                return SpareMissing switch
                {
                    true => "Yes",
                    _=>"No"
                };

            }
        }

        public bool ClRequired { get; set; }
        public bool ClInPlace { get; set; }
        [JsonIgnore]
        public bool ClMissing => ClRequired && ClInPlace == false;
        [JsonIgnore]
        public string ClMissingDescription
        {
            get
            {
                return ClMissing switch
                {
                    true => "Yes",
                    _ => "No"
                };

            }
        }

        public List<ComponentActionDto> ComponentActions { get; set; } = null!;

        public ComponentDto()
        {
            ComponentActions = new();
        }

        public ComponentActionDto AddNewAction()
        {
            var action = new ComponentActionDto
            {
                Status = 1,
                SubTypeId = Type,
                OpenDate = DateTime.Now,
                Priority=1,
                Description = Description
            };

            ComponentActions.Add(action);
            return action;
        }

        public int OpenActions => ComponentActions.Count(x => x.Status != 2);


    }
}
