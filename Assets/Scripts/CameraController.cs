using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public int maxLeft = 20;
    public int maxRight = -40;


    void Update()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;

        float mouseBoundLeft = Screen.width * 0.15f;
        float mouseBoundRight = Screen.width * 0.85f;

        if (mousePosition.x <= mouseBoundLeft)
        {
            cameraPosition += new Vector3(1, 0, 0);
            gameObject.transform.position = cameraPosition;
        }

        if (mousePosition.x >= mouseBoundRight)
        {
            cameraPosition += new Vector3(-1, 0, 0);
            gameObject.transform.position = cameraPosition;
        }



        if (cameraPosition.x <= maxLeft && cameraPosition.x >= maxRight)
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
            {
                cameraPosition += new Vector3(1, 0, 0);
                gameObject.transform.position = cameraPosition;
            }
            if (Input.GetKey("a") || Input.GetKey("left"))
            {
                cameraPosition += new Vector3(1, 0, 0);
                gameObject.transform.position = cameraPosition;
            }
            if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
            {
                cameraPosition += new Vector3(-1, 0, 0);
                gameObject.transform.position = cameraPosition;
            }
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                cameraPosition += new Vector3(-1, 0, 0);
                gameObject.transform.position = cameraPosition;
            }
        }

        if (cameraPosition.x > maxLeft)
        {
            cameraPosition += new Vector3(-1, 0, 0);
            gameObject.transform.position = cameraPosition;
        }
        if (cameraPosition.x < maxRight)
        {
            cameraPosition += new Vector3(1, 0, 0);
            gameObject.transform.position = cameraPosition;
        }
    }
}
