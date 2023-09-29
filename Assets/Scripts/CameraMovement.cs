using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // reference to what the camera will follow
    public Transform target;
    public float smoothing;
    public Vector2 maxPosititon;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame after the player moves
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            //find distance between target and position then move a percentage of that distance each frame
            this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);
             // Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
           // targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosititon.x);
           // targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosititon.y);
            transform.position = Vector3.Lerp(transform.position, this.transform.position,smoothing);

        }
    }
}
