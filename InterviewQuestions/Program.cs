
int i = 1;

void Print(object val) => Console.WriteLine($"Bear{i++}: " + val);

Print(new GummiBear().GetBounceCount());
Print(new CubbiGummiBear().Name);
GummiBase bear = new CubbiGummiBear();
Print(bear.GetBounceCount());



interface IGummiBear
{
    string Name { get; }
    int GetBounceCount();
}

abstract class GummiBase : IGummiBear
{
    public abstract string Name { get; }

    public virtual int GetBounceCount()
    {
        return 1;
    }
}

class GummiBear : GummiBase
{
    public override string Name => "Unknown";
    public override int GetBounceCount() => 2;
}

class CubbiGummiBear : GummiBear
{
    public override string Name => "Cubbi Gummi";
}

/*
 * Bear1: 2
 * Bear2: Cubbi Gummi
 * Bear3: 2
*/