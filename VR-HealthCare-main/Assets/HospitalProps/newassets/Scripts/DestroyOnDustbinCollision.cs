using UnityEngine;

public class InjectionDestroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("injection"))
        {
            Debug.Log("Injection collided with dustbin and will be destroyed.");
            Destroy(collision.gameObject);
        }
    }
}
