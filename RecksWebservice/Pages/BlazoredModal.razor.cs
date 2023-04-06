using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecksWebservice.Pages.Services;
using RecksWebservice.Data;
using RecksWebservice.Pages;

namespace RecksWebservice.Pages.PopupComponents
{
    public partial class BlazoredModal : IDisposable
    {
        const string _defaultStyle = "blazored-modal";
        const string _defaultPosition = "blazored-modal-center";

        [Inject] private Service ModalService { get; set; }

        [Parameter] public bool HideHeader { get; set; }
        [Parameter] public bool HideCloseButton { get; set; }
        [Parameter] public bool DisableBackgroundCancel { get; set; }
        [Parameter] public string Position { get; set; }
        [Parameter] public string Style { get; set; }

        private bool ComponentDisableBackgroundCancel { get; set; }
        private bool ComponentHideHeader { get; set; }
        private bool ComponentHideCloseButton { get; set; }
        public string ComponentPosition { get; set; }
        private string ComponentStyle { get; set; }
        private bool IsVisible { get; set; }
        private string Title { get; set; }
        private RenderFragment Content { get; set; }
        private ModalParameter Parameters { get; set; }

        /*protected override void OnInitialized() error
        {
            ((IService)ModalService).OnShow -= ShowModal;
            ((IService)ModalService).CloseModal -= CloseModal;
        }*/

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async void CloseModal()
        {
            Title = "";
            Content = null;
            ComponentStyle = "";
            IsVisible = false;
            //await InvokeAsync(StateHasChanged); error
        }

        private async void ShowModal(string title, RenderFragment content, ModalParameter parameters, Options options)
        {
            Title = title;
            Content = content;
            Parameters = parameters;
            IsVisible = true;
            //await InvokeAsync(StateHasChanged); error
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((IService)ModalService).OnShow -= ShowModal;
                ((IService)ModalService).CloseModal -= CloseModal;
            }
        }

        private void HandleBackgroundClick()
        {
            if (ComponentDisableBackgroundCancel) return;
            ModalService.Cancel();
        }

        private void SetModalOptions(Options options)
        {
            ComponentHideHeader = HideHeader;
            if (options.HideHeader.HasValue)
            {
                ComponentHideHeader = options.HideHeader.Value;
            }

            ComponentHideCloseButton = HideCloseButton;
            if (options.HideCloseButton.HasValue)
            {
                ComponentHideCloseButton = options.HideCloseButton.Value;
            }

            ComponentDisableBackgroundCancel = DisableBackgroundCancel;
            if (options.DisableBackgroundCancel.HasValue)
            {
                ComponentDisableBackgroundCancel = options.DisableBackgroundCancel.Value;
            }

            ComponentPosition = string.IsNullOrWhiteSpace(options.Position) ? Position : options.Position;
            if (string.IsNullOrWhiteSpace(ComponentPosition))
            {
                ComponentPosition = _defaultPosition;
            }

            ComponentStyle = string.IsNullOrWhiteSpace(options.Style) ? Style : options.Style;
            if (string.IsNullOrWhiteSpace(ComponentStyle))
            {
                ComponentStyle = _defaultStyle;
            }
        }

        public void SetTitle(string title)
        {
            Title = title;
            //StateHasChanged(); error
        }
    }
}

