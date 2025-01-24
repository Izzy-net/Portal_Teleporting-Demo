using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimGun : MonoBehaviour
{
    [SerializeField] GameObject gun;
    Vector2 mousePosition;
    Vector2 direction;
    float minRot = -160;
    float maxRot = 130;
    void Update()
    {
        HandleGunAim();
    }

    private void HandleGunAim()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        var angle = Mathf.Atan2(mousePosition.y - gun.transform.position.y, mousePosition.x - gun.transform.position.x);
        
        Debug.Log(angle);

        if (transform.localScale.x == 1)
        {
            Mathf.Clamp(angle, minRot, maxRot);
            gun.transform.localRotation = quaternion.Euler(0,0,angle);
        }
        else if (transform.localScale.x == -1)
        {
            Mathf.Clamp(angle, minRot + 180, maxRot + 180);
            gun.transform.localRotation = quaternion.Euler(0,0,-angle);
        }
    }
}
