using UnityEngine;
using static Fruit;

public class Utils
{
    public static Color ColorFromInts(int r, int g, int b)
    {
        return ColorFromInts(r, g, b, 255);
    }

    public static Color ColorFromInts(int r, int g, int b, int a)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public static FruitType GetRandomFruitType()
    {
        return (FruitType)Random.Range(0, 5);
    }

    public static Color ColorFromFruitType(FruitType type)
    {
        return type switch
        {
            FruitType.Cherry => ColorFromInts(255, 11, 17),
            FruitType.Strawberry => ColorFromInts(255, 125, 81),
            FruitType.Grape => ColorFromInts(161, 98, 246),
            FruitType.Dekopon => ColorFromInts(255, 182, 12),
            FruitType.Persimmon => ColorFromInts(255, 146, 18),
            FruitType.Apple => ColorFromInts(255, 12, 25),
            FruitType.Pear => ColorFromInts(251, 246, 95),
            FruitType.Peach => ColorFromInts(252, 204, 189),
            FruitType.Pineapple => ColorFromInts(245, 236, 17),
            FruitType.Melon => ColorFromInts(133, 208, 19),
            FruitType.Watermelon => ColorFromInts(0, 132, 0),
            _ => ColorFromInts(255, 255, 255)
        };
    }

    public static float RadiusFromFruitType(FruitType type)
    {
        return type switch
        {
            FruitType.Cherry => 0.2f,
            FruitType.Strawberry => 0.4f,
            FruitType.Grape => 0.6f,
            FruitType.Dekopon => 0.8f,
            FruitType.Persimmon => 1.0f,
            FruitType.Apple => 1.2f,
            FruitType.Pear => 1.4f,
            FruitType.Peach => 1.6f,
            FruitType.Pineapple => 1.8f,
            FruitType.Melon => 2.0f,
            FruitType.Watermelon => 2.2f,
            _ => 1.0f
        };
    }
}
