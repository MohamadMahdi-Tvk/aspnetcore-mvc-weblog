using System;
using System.Collections.Generic;
using System.Text;

namespace WeblogSample.Data.Entities;

public class Role 
{
    public short Id { get; set; }
    public string Name { get; set; }
    public List<Person> People { get; set; } = new();
}