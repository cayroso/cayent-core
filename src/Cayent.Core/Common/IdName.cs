using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Common
{
    public class IdName
    {
        public IdName(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; }
        public string Name { get; }
    }
}
