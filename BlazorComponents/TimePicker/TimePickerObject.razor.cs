using Microsoft.AspNetCore.Components;

namespace TimePicker
{
    public partial class TimePickerObject
    {
        List<TimeOnly> TimeOptions = new List<TimeOnly>();

        [Parameter]
        public TimeOnly? SelectedTime { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            var t = new TimeOnly(0, 0, 0);
            while (true)
            {
                TimeOptions.Add(t);
                t = t.AddMinutes(15);
                if (TimeOptions.Any() && t.IsBetween(new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0).AddMinutes(15)))
                    break;
            }
           StateHasChanged();
        }


        public void TimeSelected(ChangeEventArgs args)
        {
            if (args.Value != null)
            {
                var tCheck = TimeOnly.TryParse(args.Value.ToString(), out var t);
                if (tCheck)
                {
                    SelectedTime = t;
                }
            }
            else
            {
                SelectedTime = null;
            }
            OnTimeSelected.InvokeAsync(SelectedTime);
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<TimeOnly?> OnTimeSelected { get; set; }


    }

}
