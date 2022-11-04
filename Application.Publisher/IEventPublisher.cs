using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Publisher
{
    public partial interface IEventPublisher
    {
        Task PublishAsync(string message);
    }
}
