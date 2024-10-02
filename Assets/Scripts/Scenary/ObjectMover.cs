using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float conveyorSpeed = 5f;
    public Vector3 direction = Vector3.forward;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.MovePosition(rb.position + direction.normalized * conveyorSpeed * Time.deltaTime);
                rb.constraints |= RigidbodyConstraints.FreezePositionY;
                rb.isKinematic = true;
            }
        }
    }
}

