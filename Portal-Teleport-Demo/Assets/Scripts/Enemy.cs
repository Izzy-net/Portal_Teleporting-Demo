using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 20f;
    [SerializeField] float collisionDamage = 10f;
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(collisionDamage);
        }
    }
}
