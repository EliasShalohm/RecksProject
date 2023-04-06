using System;
namespace RecksWebservice.Data
{
    public class ModalParameter
    {
        private Dictionary<string, object> _parameters;

        public ModalParameter()
        {
            _parameters = new Dictionary<string, object>();
        }

        public void Add(string parameterName, object value)
        {
            _parameters[parameterName] = value;
        }

        public T Get<T>(string parameterName)
        {
            if (!_parameters.ContainsKey(parameterName))
            {
                throw new KeyNotFoundException("Not Exist.");
            }
            return (T)_parameters[parameterName];
        }

        public T TryGet<T>(string parameterName)
        {
            if (_parameters.ContainsKey(parameterName))
            {
                return (T)_parameters[parameterName];
            }
            return default;
        }
    }
}