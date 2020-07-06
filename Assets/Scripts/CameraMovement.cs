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
        if (levelBounds == null) return;
        if (transform.position != target.position)
        {
            Bounds cameraBounds = CalculateCameraBounds();
            Bounds mapBounds = GetMapColliderBounds();

            Bounds cameraMoveBounds = new Bounds( //Define the camera's total safe zone for a given map boundary, as defined by mapBounds.
                mapBounds.center,
                new Vector3(
                    mapBounds.size.x <= cameraBounds.size.x ? 0f : mapBounds.size.x - cameraBounds.size.x,
                    mapBounds.size.y <= cameraBounds.size.y ? 0f : mapBounds.size.y - cameraBounds.size.y,
                    cameraBounds.size.z
                )
            );

            transform.position = new Vector3( //Change the camera's position
              Mathf.Clamp(target.position.x, cameraMoveBounds.min.x, cameraMoveBounds.max.x),
              Mathf.Clamp(target.position.y, cameraMoveBounds.min.y, cameraMoveBounds.max.y),
              transform.position.z
            );
        }
    }

    Bounds CalculateCameraBounds()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        return new Bounds(topRight + bottomLeft / 2, topRight - bottomLeft);
    }

    Bounds GetMapColliderBounds()
    {
        return levelBounds.bounds;
    }
}
