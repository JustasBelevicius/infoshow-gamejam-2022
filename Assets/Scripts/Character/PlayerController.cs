using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public List<Action> actions;

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
                Debug.Log("Move UP");
                break;
            case Direction.RIGHT:
                Debug.Log("Move RIGHT");
                break;
            case Direction.DOWN:
                Debug.Log("Move DOWN");
                break;
            case Direction.LEFT:
                Debug.Log("Move LEFT");
                break;
        }
    }


}
