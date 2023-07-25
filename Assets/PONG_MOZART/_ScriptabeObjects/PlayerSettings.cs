using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Pong Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public enum Inputs { Mouse_Y, Mouse_X, ArrowsKey, WASD, Joystick, Joystick2 }
    public enum Mode { Human, Computer  }

    private const string MOUSE_Y = "Mouse Y";
    private const string MOUSE_X = "Mouse X";
    private const string ARROWS = "Vertical Arrow";
    private const string WASD = "Vertical WASD";

    private const string JOYSTICK_LEFTSTICK_VERTICAL = "Joystick Left Vertical Stick";
    private const string JOYSTICK_DPAD_VERTICAL = "Joystick Vertical DPad"; 
    
    private const string JOYSTICK2_LEFTSTICK_VERTICAL = "Joystick2 Left Vertical Stick";
    private const string JOYSTICK2_DPAD_VERTICAL = "Joystick2 Vertical DPad"; 
    





    [System.Serializable]
    public struct Setup
    {

        public Mode Mode;
        public Inputs PlayerInput;
        public float InputReactionSpeed;


        public Setup(Mode _mode, Inputs _playerInput, float _reaction)
        {
            Mode = _mode;
            PlayerInput = _playerInput;
            InputReactionSpeed = _reaction;
        }

        public float GetInput()
        {
            switch (PlayerInput)
            {
                case Inputs.Mouse_Y:
                    return Input.GetAxis(MOUSE_Y) * InputReactionSpeed;

                case Inputs.Mouse_X:
                    return Input.GetAxis(MOUSE_X) * InputReactionSpeed;

                case Inputs.ArrowsKey:

                    if (Input.GetKey(KeyCode.DownArrow))  
                        return -1f * InputReactionSpeed;

                    if(Input.GetKey(KeyCode.UpArrow)) 
                        return 1f * InputReactionSpeed;

                    return 0f;


                case Inputs.WASD:
                    return Input.GetAxisRaw(WASD) * InputReactionSpeed;
                
                case Inputs.Joystick:
                    var stick = Input.GetAxis(JOYSTICK_LEFTSTICK_VERTICAL);
                    var dpad = Input.GetAxis(JOYSTICK_DPAD_VERTICAL);

                    if (Mathf.Abs(stick) > 0.0f) return stick * InputReactionSpeed;
                    if (Mathf.Abs(dpad) > 0.0f) return dpad * InputReactionSpeed;

                    return 0f;
                
                case Inputs.Joystick2:
                    var stick2 = Input.GetAxis(JOYSTICK2_LEFTSTICK_VERTICAL);
                    var dpad2 = Input.GetAxis(JOYSTICK2_DPAD_VERTICAL);

                    if (Mathf.Abs(stick2) > 0.0f) return stick2 * InputReactionSpeed;
                    if (Mathf.Abs(dpad2) > 0.0f) return dpad2 * InputReactionSpeed;

                    return 0f;
                    
                    
                    

                default:
                    return 0f;
            }
        }
    }


    public Setup Preferences;


}
