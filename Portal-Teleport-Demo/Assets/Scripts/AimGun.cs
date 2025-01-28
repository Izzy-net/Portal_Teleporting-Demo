using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimGun : MonoBehaviour
{
    [SerializeField] GameObject gun;
    Vector2 mousePosition;
    float angle;
    [SerializeField] float minRot = -50;
    [SerializeField] float maxRot = 50;
    float minRotAlt;
    float maxRotAlt;

    private void Start() 
    {
        minRotAlt = -180 - minRot;
        maxRotAlt = 180 - maxRot;
    }

    void Update()
    {
        HandleGunAim();
    }

    private void HandleGunAim()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        angle = Mathf.Atan2(mousePosition.y - gun.transform.position.y, mousePosition.x - gun.transform.position.x) * Mathf.Rad2Deg;
        
        if (transform.localScale.x == -1)
        {
            angle *= -1;

            if (angle < maxRotAlt && angle > 0)
            {
                angle = maxRotAlt;
            }
            else if (angle > minRotAlt && angle < 0)
            {
                angle = minRotAlt;
            }
        }

        else
        {
            angle = Mathf.Clamp(angle, minRot, maxRot);
        }
        
        gun.transform.localRotation = Quaternion.Euler(0,0,angle);
    }
}
