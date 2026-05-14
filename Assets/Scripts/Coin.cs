using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float bobHeight = 0.2f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float rotateSpeed = 180f;

    [Header("Effects")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupParticlePrefab;

    private Vector3 startPosition;
    private float bobOffset;

    void Start()
    {
        startPosition = transform.localPosition;
        bobOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        // Bobbing motion
        float newY = startPosition.y + Mathf.Sin((Time.time + bobOffset) * bobSpeed) * bobHeight;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);

        // Rotation
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    public void OnCollected()
    {
        // Play sound
        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        // Spawn particle effect
        if (pickupParticlePrefab != null)
            Instantiate(pickupParticlePrefab, transform.position, Quaternion.identity);

        // Return to pool (instead of destroying)
        gameObject.SetActive(false);
    }
}