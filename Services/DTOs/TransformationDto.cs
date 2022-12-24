using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
  
        public class TransformationDto
        {
            public int Id { get; set; }
            [Required]
            public string Description { get; set; }
            public LineAreaDto? LineArea { get; set; }
            public List<ComponentDto> Components { get; set; }

            public TransformationDto(LineAreaDto lineArea)
            {
                LineArea = lineArea;
                Components = new();
                Description = string.Empty;
            }

            public TransformationDto()
            {

            }

            public ComponentDto AddNewComponent()
            {
                var component = new ComponentDto();
                component.Type = 1;
                Components.Add(component);
                return component;
            }
        }
    }

    public enum SubTypeEnum
    {

        Mechanical = 1,
        Pneumatic = 2,
        Electropneumatic = 3,
        Electrical = 4,
        Hydraulic = 5
    }

