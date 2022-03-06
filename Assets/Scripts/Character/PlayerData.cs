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

    public Vector2Int GetPlayerPositionInRoom()
    {
        return new Vector2Int(Mod(x - 1, Room.WIDTH), Mod(y - 1, Room.HEIGHT));
    }


    private int Mod(int a, int n)
    {
        var pos = a % n;
        return (n + a % n) % n;
    }
}
