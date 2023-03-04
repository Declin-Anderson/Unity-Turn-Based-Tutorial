

// Struct for holding the grid positon of each tile in the grid system
using System;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    // Basic Consturctor
    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    // Overriding the print for the grid positions to print the values in each struct
    public override string ToString()
    {
        return "x: " + x + "; z: " + z;
    }

    public static bool operator ==(GridPosition a, GridPosition b) {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator !=(GridPosition a, GridPosition b) 
    {
        return !(a == b);
    }
}
