using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public List<Action> actions;

    [SerializeField]
    public PlayerData playerData;

    void Update()
    {
        foreach(Action action in actions)
        {
            action.OnUpdate(this);
        }
    }

    public void Move(Direction direction)
    {
        switch(direction)
        {
            case Direction.UP:
                playerData.y += 1;
                break;
            case Direction.RIGHT:
                playerData.x += 1;
                break;
            case Direction.DOWN:
                playerData.y -= 1;
                break;
            case Direction.LEFT:
                playerData.x -= 1;
                break;
        }
    }

    public int[] GetCurrentRoomPosition()
    {
        return new int[]{ Mathf.RoundToInt(playerData.x / Room.WIDTH), Mathf.RoundToInt(playerData.y / Room.HEIGHT) };
    }
}
