using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavesBookOpen : MonoBehaviour
{
    public GameObject GameObject; // This is the menu you want to activate
    public GameObject[] caves;       // Array of caves GameObjects

    // Update is called once per frame
    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Iterate through the caves array to see if any GameObject is active
            foreach (GameObject cave in caves)
            {
                if (cave.activeSelf)
                {
                    // Activate the menu GameObject
                    GameObject.SetActive(true);

                    // Unlock and show the cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // Break out of the loop once a condition is met
                    break;
                }
            }
        }
    }
}
