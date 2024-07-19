namespace EntityFrameWorkHomeWork01.Model;

public class Item
{
    public int Id { get; set; }
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public string StNumber { get; set; } = null!;

    public override string ToString()
    {
        return Make;
    }
}