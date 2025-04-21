class TreeType
{
    public string Name { get; }
    public string Color { get; }
    public string Texture { get; }

    public TreeType(string name, string color, string texture)
    {
        Name = name;
        Color = color;
        Texture = texture;
    }

    public void Draw(string canvas, int x, int y)
    {
        Console.WriteLine($"Draw {Name} color {Color} with texture " +
            $"{Texture} on {canvas} in coordinates ({x}, {y})");
    }
}

class TreeFactory
{
    private static readonly Dictionary<string, TreeType> treeTypes = new();

    public static TreeType GetTreeType(string name, string color, string texture)
    {
        string key = $"{name}_{color}_{texture}";

        if (!treeTypes.ContainsKey(key))
        {
            treeTypes[key] = new TreeType(name, color, texture);
            Console.WriteLine($"New TreeType object created: {key}");
        }

        return treeTypes[key];
    }
}

class Tree
{
    private readonly int x;
    private readonly int y;
    private readonly TreeType type;

    public Tree(int x, int y, TreeType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

    public void Draw(string canvas)
    {
        type.Draw(canvas, x, y);
    }
}

class Forest
{
    private readonly List<Tree> trees = new();

    public void PlantTree(int x, int y, string name, string color, string texture)
    {
        TreeType type = TreeFactory.GetTreeType(name, color, texture);
        Tree tree = new(x, y, type);
        trees.Add(tree);
    }

    public void Draw(string canvas)
    {
        foreach (var tree in trees)
        {
            tree.Draw(canvas);
        }
    }
}
