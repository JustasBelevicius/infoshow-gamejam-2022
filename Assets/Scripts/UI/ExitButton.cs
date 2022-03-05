using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton: MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Exit();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
