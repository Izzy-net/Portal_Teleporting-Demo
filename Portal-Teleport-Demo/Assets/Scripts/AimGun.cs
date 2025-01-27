using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimGun : MonoBehaviour
{
    [SerializeField] GameObject gun;
    Vector2 mousePosition;
    Vector2 direction;
    float angle;
    [SerializeField] float minRot = -130;
    [SerializeField] float maxRot = 130;
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

            if (angle < 130 && angle > 0)
            {
                angle = 130;
            }
            else if (angle > -130 && angle < 0)
            {
                angle = -130;
            }
        }
        else
        {
            angle = Mathf.Clamp(angle, -50, 50);
        }

        gun.transform.localRotation = Quaternion.Euler(0,0,angle);
    }
}
