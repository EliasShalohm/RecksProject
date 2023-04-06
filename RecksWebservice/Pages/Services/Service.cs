using System;
using Microsoft.AspNetCore.Components;
using RecksWebservice.Pages.Services;
using RecksWebservice.Data;
namespace RecksWebservice.Pages.Services
{
	public interface Service
	{
		event Action<ModalResult> OnClose;
		void Show<T>(string title, ModalParameter parameters) where T : ComponentBase;
		
        void Show<T>(string title, ModalParameter parameters = null, Options options = null) where T : ComponentBase;

        void Show(Type contentComponent, string title, ModalParameter parameters, Options options);

		void Close(ModalResult modalResult);

		void Cancel();
    }
}

