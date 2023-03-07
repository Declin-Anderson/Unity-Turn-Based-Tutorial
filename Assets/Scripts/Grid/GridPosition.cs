

// Struct for holding the grid positon of each tile in the grid system
using System;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Struct for the grid tiles
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

    //* Overriding the equals function to be able to compare 2 coordinates
    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    //* Overriding the equals function to be able to compare 2 grid positions
    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    //* Overriding to gather the hash code
    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    //* Overriding the print for the grid positions to print the values in each struct
    public override string ToString()
    {
        return "x: " + x + "; z: " + z;
    }

    //* Overriding the compare operator to be able to check coordinates to see if they are equal
    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    //* Overriding the compare operator to be able to check coordinates to see if they are equal
    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    //* Overriding the addition operator to be able to add two coordinates
    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    //* Overriding the subtraction operator to be able to subtract two coordinates
    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
}
