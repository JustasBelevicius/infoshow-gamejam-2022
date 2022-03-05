using UnityEngine;

public class PlayerData
{
    public int x;
    public int y;
    public float foodLevel;

    public PlayerData Clone()
    {
        var copy = new PlayerData();
        copy.x = x;
        copy.y = y;
        copy.foodLevel = foodLevel;
        return copy;
    }


    public int[] GetCurrentRoomPosition()
    {
        return new int[] { Mathf.RoundToInt(((x - .5f) / (float)Room.WIDTH) - .5f), Mathf.RoundToInt(((y - .5f) / (float)Room.HEIGHT) - .5f) };
    }
}
