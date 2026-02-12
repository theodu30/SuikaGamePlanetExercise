using UnityEngine;

public class Player : MonoBehaviour
{
    public float TurnAngle = 45f;
    public GameObject FruitPrefab;

    public Transform SpawnTransform;

    private GameObject spawnedFruit;

    private void Start()
    {
        if (SpawnTransform == null)
        {
            Debug.LogError("SpawnTransform is not assigned.");
        }

        ReleaseAndSpawnNew(); // Initial fruit spawn
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerTurnLeft += HandlePlayerTurnLeft;
        GameEvents.OnPlayerTurnRight += HandlePlayerTurnRight;
        GameEvents.OnPlayerSpawnFruit += HandlePlayerSpawnFruit;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerTurnLeft -= HandlePlayerTurnLeft;
        GameEvents.OnPlayerTurnRight -= HandlePlayerTurnRight;
        GameEvents.OnPlayerSpawnFruit -= HandlePlayerSpawnFruit;
    }

    private void HandlePlayerTurnLeft(object sender, System.EventArgs args)
    {
        transform.Rotate(0f, 0f, TurnAngle * Time.deltaTime);
    }

    private void HandlePlayerTurnRight(object sender, System.EventArgs args)
    {
        transform.Rotate(0f, 0f, -TurnAngle * Time.deltaTime);
    }

    private void HandlePlayerSpawnFruit(object sender, System.EventArgs args)
    {
        if (FruitPrefab != null && SpawnTransform != null)
        {
            ReleaseAndSpawnNew();
        }
        else
        {
            Debug.LogError("FruitPrefab or SpawnTransform is not assigned.");
        }
    }

    private void ReleaseAndSpawnNew()
    {
        if (spawnedFruit != null)
        {
            if (spawnedFruit.TryGetComponent(out Fruit fruitComponent))
            {
                fruitComponent.ReleaseFruit();
            }

            if (spawnedFruit.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddForce(4f * SpawnTransform.up, ForceMode2D.Impulse);
            }
        }

        spawnedFruit = Instantiate(FruitPrefab, SpawnTransform.position, SpawnTransform.rotation, SpawnTransform);
        if (spawnedFruit.TryGetComponent(out Fruit newFruitComponent))
        {
            GameEvents.CurrentType = GameEvents.NextType;
            newFruitComponent.SetupFruit(GameEvents.CurrentType);
            GameEvents.NextType = Utils.GetRandomFruitType();
            GameEvents.RaiseNextTypeChange(GameEvents.NextType);
        }
    }
}
