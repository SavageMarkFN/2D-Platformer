using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Movement")]
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Jump;
    public KeyCode Dash;
    public KeyCode Slide;

    [Header("Interactions")]
    public KeyCode InventoryKey;
    public KeyCode Interaction;
}
