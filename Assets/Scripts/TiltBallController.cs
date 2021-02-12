using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class TiltBallController : MonoBehaviour
{
    private float _speed = 500;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Accelerometer();
    }

    private void Accelerometer()
    {
        _rigidbody.AddForce(new Vector3(Input.acceleration.x, 0, Input.acceleration.y) * (_speed * Time.deltaTime));
    }
}