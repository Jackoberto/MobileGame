using UnityEngine;

public class TouchBallController : MonoBehaviour
{
    public float acceleration;
    private Rigidbody _rigidbody;
    private Vector3 _movePos;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit, 100, LayerMask.GetMask("Plane")))
            {
                _movePos = raycastHit.point;
            }
        }
        _rigidbody.AddForce((_movePos - transform.position).normalized * (acceleration * Time.deltaTime), ForceMode.VelocityChange);
    }
}