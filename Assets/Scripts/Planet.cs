using UnityEngine;

public class Planet : MonoBehaviour
{
    private CircleCollider2D circleCollider;

    public float Gravity = 9.81f;

    private void Awake()
    {
        // Get self circle collider
        circleCollider = GetComponent<CircleCollider2D>();

        // If no circleCollider found, destroy gameobject
        if (circleCollider == null)
        {
            Debug.LogError("No CircleCollider2D, deleting gameObject");
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Fruit>(out _))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            float distance = Vector2.Distance(collision.transform.position, transform.position);

            if (distance < .1f)
            {
                collision.attachedRigidbody.linearDamping = 8f;
                collision.attachedRigidbody.linearVelocity *= .9f;
            }
            else
            {
                collision.attachedRigidbody.linearDamping = 1f;
            }

            float intensity = distance / circleCollider.radius;

            collision.attachedRigidbody.AddForce(Gravity * intensity * collision.attachedRigidbody.mass * Time.deltaTime * direction);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Fruit fruit))
        {
            if (!fruit.WasDestroyed())
            {
                GameEvents.RaiseGameLost();
            }
        }
    }
}
