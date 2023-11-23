﻿using Cayent.Core.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Cayent.Core.Data.Base
{
    public abstract class BaseModel
    {
        DateTime _dateCreated = DateTime.UtcNow.Truncate();
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }

        [ConcurrencyCheck]
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }
}
