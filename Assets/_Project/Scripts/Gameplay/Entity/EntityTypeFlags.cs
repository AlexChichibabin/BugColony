using System;

[Flags]
public enum EntityTypeFlags
{
    None = 0,
    Resources = 1 << 0,
    Workers = 1 << 1,
    Predators = 1 << 2
}