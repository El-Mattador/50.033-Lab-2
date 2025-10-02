using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;
    void Start()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); // the z-component is the distance of the resulting plane from the camera 
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
    }
    void Update()
    {
        float desiredX = player.position.x + offset;
        float desiredY = player.position.y;
        
        // Clamp X position between start and end boundaries
        if (desiredX > startX && desiredX < endX)
        {
            this.transform.position = new Vector3(desiredX, desiredY, this.transform.position.z);
        }
        else
        {
            // If X is out of bounds, only update Y
            this.transform.position = new Vector3(this.transform.position.x, desiredY, this.transform.position.z);
        }
    }
}
