using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImportFileExelRedis.Infrastructure
{
    public class PropertyByName<T>
    { 
        public PropertyByName(string propertyName, Func<T, object>? func = null, bool ignore = false)
        {
            PropertyName = propertyName;

            if (func != null)
                GetProperty = obj => Task.FromResult(func(obj));

            PropertyOrderPosition = 1;
            Ignore = ignore;
        }

        public int PropertyOrderPosition { get; set; }
        public Func<T, Task<object>> GetProperty { get; }
        public string PropertyName { get; }
        public bool Ignore { get; set; }


    }
}
