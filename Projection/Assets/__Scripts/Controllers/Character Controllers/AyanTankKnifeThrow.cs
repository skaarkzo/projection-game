using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyanTankKnifeThrow : MonoBehaviour
{

    [Header("References")]
    public Transform camTransform;
    public Transform attackPoint;
    public GameObject throwingObject;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    public bool readyToThrow;

    // Start is called before the first frame update
    void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    public void Throw()
    {
        readyToThrow = false;

        // Instantiates throwing object
        GameObject projectile = Instantiate(throwingObject, attackPoint.position, camTransform.rotation * Quaternion.Euler(45, 45, 45));

        // Gets Rigidbody Component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Add Force
        Vector3 addForce = transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(addForce, ForceMode.Impulse);

        totalThrows--;

        // Implement Throw Cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
