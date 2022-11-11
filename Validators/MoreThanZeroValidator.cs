using System.ComponentModel.DataAnnotations;

namespace Validators
{
    public class MoreThanZeroValidator:ValidationAttribute 
    {
        public override bool IsValid(object? value)
        {
            var check = int.TryParse(value.ToString(), out var i);
            return check && i > 0;
        }

    }
}