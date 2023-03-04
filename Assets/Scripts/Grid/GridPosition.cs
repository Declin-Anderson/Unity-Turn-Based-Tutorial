

// Struct for holding the grid positon of each tile in the grid system
public struct GridPosition
{
    public int x;
    public int z;

    // Basic Consturctor
    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    // Overriding the print for the grid positions to print the values in each struct
    public override string ToString()
    {
        return "x: " + x + "; z: " + z;
    }
}
