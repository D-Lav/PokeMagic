using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public GameObject currentInterObj = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("interObject"))
        {
            Debug.Log(collision.name);
            currentInterObj = collision.gameObject;
           ;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("interObject"))
        {
            if(collision.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
           
        }
    }

}
