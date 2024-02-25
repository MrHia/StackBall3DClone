using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPartController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private StackController _stackController;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _stackController = transform.parent.GetComponent<StackController>();
        _collider = GetComponent<Collider>();
    }

    public void Shatter()
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = false;
        var forcePoint = transform.parent.position;
        var parentXPos = transform.position.x;
        var xPos = _meshRenderer.bounds.center.x;

        var subDir = (parentXPos - xPos < 0) ? Vector3.right : Vector3.left;
        var dir = (Vector3.up * 1.5f + subDir).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        _rigidbody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.left * torque);
        _rigidbody.velocity = Vector3.down;

    }

    public void RemoveAllChildes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }
}
