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
    float compareAngle = 0f;
    float addToRot = 80;
    float addToCompare = -180;
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
        }

        if (angle > minRot && angle < compareAngle)
        {
            //angle = minRot;
        }
        else if (angle > -compareAngle && angle < maxRot)
        {
            //angle = maxRot;
        }
        Debug.Log(angle);
        Debug.Log("Compare Angle =  " +compareAngle);
        Debug.Log("maxRot = " + maxRot + "minRot = " + minRot);
        gun.transform.localRotation = Quaternion.Euler(0,0,angle);
    }

    public void ChangeForOtherAxisSide()
    {
        addToRot *= -1;
        addToCompare *= -1;
        minRot -= addToRot;
        maxRot += addToRot;
        compareAngle += addToCompare;
    }
}
