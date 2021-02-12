using System.Linq;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float maxHeight = 50;
    public float zoomMultiplier = 0.2f;
    private Vector3[] positons = new Vector3[2];
    private Vector3 _lastFrameDifference;
    private static bool IsBeginningPinch => Input.touches.Any(touch => touch.phase == TouchPhase.Began);
    
    private void Update()
    {
        if (Input.touchCount == 2) 
            MoveCamera();
    }

    private void MoveCamera()
    {
        for (var i = 0; i < Input.touches.Length; i++)
        {
            positons[i] = Input.touches[i].position;
        }

        if (!IsBeginningPinch)
        {
            var difference = positons[0] - positons[1];
            var frameDifference = difference - _lastFrameDifference;
            _lastFrameDifference = difference;
            Debug.Log(frameDifference);
            var cameraDistance = Mathf.Abs(frameDifference.y) > Mathf.Abs(frameDifference.x) ? frameDifference.y : frameDifference.x;
            var magnitude = cameraDistance > 0 ? frameDifference.sqrMagnitude : -frameDifference.sqrMagnitude;
            magnitude *= zoomMultiplier;
            var main = Camera.main.transform;
            var position = main.position;
            var velocity = new Vector3(0, Mathf.Clamp(position.y + magnitude, 10, maxHeight), 0);
            position = new Vector3(position.x, velocity.y, position.z);
            // Without SmoothDamp
            main.position = position;
            // With SmoothDamp
            /*main.position = Vector3.SmoothDamp(main.position, position, ref velocity, 0.5f);
            main.position = new Vector3(position.x, Mathf.Clamp(position.y, 10, maxHeight), position.z);*/
        }
        else
        {
            _lastFrameDifference = positons[0] - positons[1];
        }
    }
}
