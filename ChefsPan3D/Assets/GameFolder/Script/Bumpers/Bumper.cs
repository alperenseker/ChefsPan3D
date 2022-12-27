using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] float bumperForce;

    private void OnCollisionEnter(Collision collision)
    {
        Raycast.Instance.DraggingFlag = false;
        ContactPoint[] tmpContact = collision.contacts;
        foreach (ContactPoint contact in tmpContact)
        {
            Rigidbody rb = contact.otherCollider.GetComponent<Rigidbody>();
            float t = collision.relativeVelocity.magnitude;
            rb.velocity = new Vector3(rb.velocity.x * .25f, rb.velocity.y * .25f, rb.velocity.z * .25f); 
            rb.AddForce(-1 * contact.normal * bumperForce, ForceMode.VelocityChange);
        }
    }
}
