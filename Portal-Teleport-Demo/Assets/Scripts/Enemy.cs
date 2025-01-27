using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 20f;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
