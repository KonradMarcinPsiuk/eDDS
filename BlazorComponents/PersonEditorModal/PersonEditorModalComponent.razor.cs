using Blazored.Modal;
using Blazored.Modal.Services;
using DTOs;
using Interfaces;
using Microsoft.AspNetCore.Components;
using ModalBaseLibrary;
using PeopleService;

namespace PersonEditorModal
{
    public partial class PersonEditorModalComponent
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        
        [Inject] private IPeopleService _peopleService { get; set; }
        
        [Parameter]
        public  int? PersonId { get; set; }

        private bool _isLoading = true;
        
        private PersonDto Person { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (PersonId != null)
            {
                Person = await _peopleService.GetPerson((int)PersonId);
            }
            else
            {
                Person = new PersonDto();
            }
            await base.OnInitializedAsync();
        }


        private async Task CloseModal()
        {
           
            await _peopleService.SavePerson(Person);
            await ModalInstance.CloseAsync(ModalResult.Ok(Person));
        }

       

        private async Task CancelMethod()
        {
           
           
            await ModalInstance.CloseAsync(ModalResult.Cancel());
        }

    }
}
