using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration = 2f;
    private bool isInvincible;
    private float invincibleTimer;

    private void Start()
    {
        // Start invincible
        isInvincible = true;
        invincibleTimer = invincibilityDuration;
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
                isInvincible = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (isInvincible) return;   // ignore collision during invincibility
            GameManager.Instance.EndGame();
        }
        else if (other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                GameManager.Instance.AddCoin();
                coin.OnCollected();
            }
        }
    }
}