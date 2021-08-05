using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Services
{
    public interface IContainer
    {
        T Resolve<T>();
    }
}
