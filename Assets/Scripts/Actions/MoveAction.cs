using UnityEngine;


[CreateAssetMenu(fileName = "MoveAction", menuName = "Actions/MoveAction", order = 1)]
public class MoveAction : Action
{

    protected override bool CheckInput(PlayerManager controller)
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        return vertical != 0 || horizontal != 0;
    }

    protected override void DoAction(PlayerManager controller)
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            // Move vertically
            controller.Move(vertical < 0 ? Direction.DOWN : Direction.UP);
        } else
        {
            // Move horizontally
            controller.Move(horizontal < 0 ? Direction.LEFT : Direction.RIGHT);
        }
    }
}
