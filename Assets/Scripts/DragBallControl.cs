using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBallControl : MonoBehaviour
{
    public float acceleration;
    private Queue<Vector3> _vector3s = new Queue<Vector3>();
    private bool _isWalking;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit, 100, LayerMask.GetMask("Plane")))
            {
                _vector3s.Enqueue(raycastHit.point);
                if (!_isWalking)
                    StartCoroutine(Walk());
            }
        }
    }

    private IEnumerator Walk()
    {
        _isWalking = true;
        while (_vector3s.Count > 0)
        {
            var vector3 = _vector3s.Dequeue();
            while ((transform.position - new Vector3(vector3.x, transform.position.y, vector3.z)).sqrMagnitude > 1f)
            {
                _rigidbody.AddForce((new Vector3(vector3.x, transform.position.y, vector3.z) - transform.position).normalized * (acceleration * Time.deltaTime), ForceMode.VelocityChange);
                //_rigidbody.velocity += (new Vector3(vector3.x, transform.position.y, vector3.z) - transform.position).normalized * (acceleration * Time.deltaTime);
                yield return null;
            }
            
            /*while (Mathf.Abs((transform.position - vector3).magnitude) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, vector3, 0.25f);
                yield return null;
            }*/
        }
        _isWalking = false;
    }
}