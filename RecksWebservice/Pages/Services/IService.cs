using System;
using Microsoft.AspNetCore.Components;
using RecksWebservice.Pages.Services;
using RecksWebservice.Data;
namespace RecksWebservice.Pages.Services
{
	public class IService : Service
	{
        public event Action<ModalResult> OnClose;
        internal event Action CloseModal;
        internal event Action<string, RenderFragment, ModalParameter, Options> OnShow;
        private Type _modalType;

        public void Show<T>(string title, ModalParameter parameters) where T : ComponentBase
        {
            Show<T>(title, parameters, new Options());
        }

        public void Show<T>(string title, ModalParameter parameters = null, Options options = null) where T : ComponentBase
        {
            Show(typeof(T), title, parameters, options);
        }

        public void Show(Type contentComponent, string title, ModalParameter parameters, Options options)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException("Must be a Blazor Component");
            }

            var content = new RenderFragment(x => { x.OpenComponent(1, contentComponent); x.CloseComponent(); });
            _modalType = contentComponent;

            OnShow?.Invoke(title, content, parameters, options);
        }

        public void Close(ModalResult modalResult)
        {
            modalResult.ModalType = _modalType;
            CloseModal?.Invoke();
            OnClose?.Invoke(modalResult);
        }

        public void Cancel()
        {
            CloseModal?.Invoke();
            OnClose?.Invoke(ModalResult.Cancel(_modalType));
        }
    }
	}

