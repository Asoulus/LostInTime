using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerInputHandler.instance.InteractionButtonPressed();   //e         
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            PlayerInputHandler.instance.LeftMouseButtonPressed();//left mouse
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerInputHandler.instance.EscapeButtonPressed();//esc
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerInputHandler.instance.QuestButtonPressed();//Q
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerInputHandler.instance.ReloadButtonPressed();//R
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerInputHandler.instance.OneButtonPressed(1);//1
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerInputHandler.instance.TwoButtonPressed(2);//2
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            PlayerInputHandler.instance.ScrollWheelUsed(Input.GetAxis("Mouse ScrollWheel"));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player_Master.instance.CallEventPlayerHeal(50f); //TODO Figure out amount
        }
    }
}
