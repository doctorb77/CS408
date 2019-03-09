using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code taken from unity forum thread https://forum.unity.com/threads/camera-moves-when-mouse-on-edge-of-screen.4840/
//credit for template is given to Omar Rojo who posted the template code for this class on Jan 4, 2007

public class CameraMovement : MonoBehaviour
{
    // Attach to a camera

    public int mDelta = 10; // Pixels. The width border at the edge in which the movement work
    public float mSpeed = 3.0f; // Scale. Speed of the movement

    private Vector3 mRightDirection = Vector3.right; // Direction the camera should move when on the right edge
    private Vector3 mDownDirection = Vector3.down;

    public void Update()
    {
        // Check if on the right edge
        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            // Move the camera
            transform.position += mRightDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.x <= mDelta)
        {
            // Move the camera
            transform.position -= mRightDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y >= Screen.height - mDelta)
        {
            // Move the camera
            transform.position -= mDownDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y <= mDelta)
        {
            // Move the camera
            transform.position += mDownDirection * Time.deltaTime * mSpeed;
        }
    }
}
