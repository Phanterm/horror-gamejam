using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Collider2D levelBounds;

    // Start is called before the first frame update
    void Start()
    {
        AdjustCamera();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        AdjustCamera();
    }

    void AdjustCamera() //This function aligns the camera to the player, and prevents it from scrolling beyond the bounds we define.
    {
        if (transform.position != target.position)
        {
            Rect cameraRect = CalculateCameraRect();
            Bounds mapBounds = GetMapColliderBounds();
            Rect cameraMoveRect = new Rect( //Define the camera's total safe zone for a given map boundary, as defined by mapBounds.
               mapBounds.center.x,
               mapBounds.center.y,
               mapBounds.size.x <= cameraRect.size.x ? 0f : mapBounds.size.x - cameraRect.size.x,
               mapBounds.size.y <= cameraRect.size.y ? 0f : mapBounds.size.y - cameraRect.size.y
            );
            transform.position = new Vector3( //Change the camera's position
              Mathf.Clamp(target.position.x, cameraMoveRect.xMin, cameraMoveRect.xMax),
              Mathf.Clamp(target.position.y, cameraMoveRect.yMin, cameraMoveRect.yMax),
              transform.position.z
            );
        }
    }

    Rect CalculateCameraRect()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        return new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }

    Bounds GetMapColliderBounds()
    {
        return levelBounds.bounds;
    }
}
