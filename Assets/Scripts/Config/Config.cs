public class Config
{
    public string version;
    public float actionCost;
    public float startingFood;
    public float maxFood;
    public int foodRestore;
    public int foodItemsPerRoom;

    public override string ToString()
    {
        return
            $"version: {version}\n" +
            $"maxFood: {maxFood}\n" +
            $"startingFood: {startingFood}\n" +
            $"actionCost: {actionCost}\n" +
            $"foodRestore: {foodRestore}\n" +
            $"foodItemsPerRoom: {foodItemsPerRoom}";
    }
}
