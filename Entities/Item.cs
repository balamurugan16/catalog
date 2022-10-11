using System;

namespace Catalog.Entities
{
  // Records are a feature of C# 9. It is structurally similar to classes and structs
  // They follow a value based equality whereas classes follow reference based equality.
  // It checks if 2 records have the same structure and the same value in their properties.
  public record Item {
    public Guid Id { get; init; } 
    
    public string Name { get; init; }

    public decimal Price { get; init; }

    public DateTimeOffset CreateDate { get; init; }    
  }
}