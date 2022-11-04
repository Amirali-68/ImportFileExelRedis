

namespace ImportFileExelRedis.Infrastructure
{
    public class PropertyManager<T>
    {

        private readonly Dictionary<string, PropertyByName<T>> _properties;

        public PropertyManager(IEnumerable<PropertyByName<T>> properties)
        {
            _properties = new Dictionary<string, PropertyByName<T>>();
    
            var poz = 1;
            foreach (var propertyByName in properties.Where(p => !p.Ignore))
            {
                propertyByName.PropertyOrderPosition = poz;
                poz++;
                _properties.Add(propertyByName.PropertyName, propertyByName);
            }
        }
        public PropertyByName<T>[] GetProperties => _properties.Values.ToArray();
        
    }
}
