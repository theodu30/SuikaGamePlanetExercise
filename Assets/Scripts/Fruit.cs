using System.IO;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class Fruit : MonoBehaviour
{
    public enum FruitType
    {
        Cherry = 0,
        Strawberry,
        Grape,
        Dekopon,
        Persimmon,
        Apple,
        Pear,
        Peach,
        Pineapple,
        Melon,
        Watermelon
    }

    private Transform renderTransform;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;

    public FruitType Type;

    private bool released = false;

    private bool destroyed = false;
    public bool WasDestroyed()
    {
        return destroyed;
    }

    private void Awake()
    {
        // If no child, in this case no render object, destroy gameobject
        if (transform.childCount == 0)
        {
            Debug.LogError("No Render Object, deleting gameobject");
            Destroy(gameObject);
            return;
        }

        // Get render object Transform et self circle collider
        renderTransform = transform.GetChild(0);
        circleCollider = GetComponent<CircleCollider2D>();

        // If no circleCollider found, destroy gameobject
        if (circleCollider == null)
        {
            Debug.LogError("No CircleCollider2D, deleting gameObject");
            Destroy(gameObject);
        }

        // Get sprite renderer in render object
        spriteRenderer = renderTransform.GetComponent<SpriteRenderer>();

        // If no sprite renderer found, destroy gameobject
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer in Render Object, deleting gameObject");
            Destroy(gameObject);
        }

        // Get self rigidbody
        rb = GetComponent<Rigidbody2D>();

        // If no rigidbody found, destroy gameobject
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D, deleting gameObject");
            Destroy(gameObject);
        }

        rb.bodyType = RigidbodyType2D.Kinematic;
        circleCollider.enabled = false;
    }

    public void SetupFruit(FruitType type)
    {
        Type = type;
        SetupParameters();
    }

    private void SetupParameters()
    {
        float radius = Utils.RadiusFromFruitType(Type);

        circleCollider.radius = radius / 2f;
        renderTransform.localScale = radius * Vector3.one;

        spriteRenderer.color = Utils.ColorFromFruitType(Type);

        rb.mass = radius * 2f;
    }

    [ContextMenu("ReloadFruit")]
    public void ReloadFruit()
    {
        SetupFruit(Type);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (released && collision.transform.TryGetComponent(out Fruit other))
        {
            if (other.Type == Type)
            {
                // If last fruit type, do nothing
                if (Type == FruitType.Watermelon)
                {
                    GameEvents.RaiseScoreChange(5000); // Grant 10000 points for merging two watermelons (5000 each)
                    destroyed = true;
                    other.destroyed = true;
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    return;
                }

                GameEvents.RaiseScoreChange(((int)Type + 1) * 100);
                other.destroyed = true;
                Destroy(other.gameObject);
                Type = (FruitType)((int)Type + 1);
                SetupFruit(Type);
            }
        }
    }

    public void ReleaseFruit()
    {
        if (released) return;
        transform.parent = null;
        released = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        circleCollider.enabled = true;
    }
}
