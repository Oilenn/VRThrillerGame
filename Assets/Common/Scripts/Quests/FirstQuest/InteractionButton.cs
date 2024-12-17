using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    private bool isPressed = false;
    public bool IsPressed {  get { return isPressed; } } 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            isPressed = true;
        }
    }

    public void Update()
    {
        if (isPressed)
        {
            Debug.Log("Pressed");
        }
    }
}
