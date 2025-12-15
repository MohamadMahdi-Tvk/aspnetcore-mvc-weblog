using System;
using System.Collections.Generic;
using System.Text;

namespace WeblogSample.Data.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; } 

    public DateTime InsertDate { get; set; } = DateTime.Now;

    public DateTime UpdateDate { get; set; }

    public bool IsActive { get; set; } = true;
}
