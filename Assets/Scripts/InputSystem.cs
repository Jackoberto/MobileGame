using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Transform compass, transformDirection;
    private Vector3 _lastUpdatedRotation;
    private float _speed = 500;
    private Rigidbody _rigidbody;
    private int _tick;

    private async void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        #if UNITY_EDITOR
        await Task.Delay(3000);
        #endif
        Input.compass.enabled = true;
    }

    private void FixedUpdate()
    {
        CompassControls();
        //_lastUpdatedRotation = new Vector3(0, 0, Input.compass.magneticHeading);
        //rotation = Quaternion.Euler(0, 0, Input.compass.magneticHeading);
        //if (Mathf.Abs((_lastUpdatedRotation - rotation.eulerAngles).magnitude) > 5)
        //compass.eulerAngles = Vector3.RotateTowards(rotation.eulerAngles, _lastUpdatedRotation, 10, 1f);
    }

    private void CompassControls()
    {
        if (_tick % 26 == 0)
        {
            _lastUpdatedRotation = new Vector3(0, 0, Input.compass.magneticHeading);
            StartCoroutine(Rotate(compass, _lastUpdatedRotation, 0.50f));
            Debug.Log("Acceleration "+Input.acceleration);
        }
        
        transformDirection.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
        Debug.Log("Magnetic Heading "+Input.compass.magneticHeading);
        _tick++;
        Debug.Log("Transform Direction " + transformDirection.forward);
        Debug.Log("Transform Rotation " + transform.rotation);
        _rigidbody.velocity = transformDirection.forward * (_speed * Time.deltaTime);
    }

    private void Accelerometer()
    {
        _rigidbody.AddForce(new Vector3(Input.acceleration.x, 0, Input.acceleration.y) * (_speed * Time.deltaTime));
    }

    IEnumerator Rotate(Transform trans, Vector3 target, float duration)
    {
        var start = trans.eulerAngles;
        if (start.z > target.z + 220)
            target += new Vector3(0,0,360);
        else if (target.z > start.z + 220)
            start += new Vector3(0,0,360);
        var t = 0f;
        while(t < duration)
        {
            trans.eulerAngles = Vector3.Slerp(start, target, t / duration);
            yield return null;
            t += Time.deltaTime;
        }
    }
}