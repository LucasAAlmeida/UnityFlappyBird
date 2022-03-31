using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody birdRb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float horizontalSpeed;
    private readonly float verticalRange = 7;
    private readonly float horizontalRange = 10;
    private readonly float facingUpAngle = 225;
    private readonly float facingDownAngle = 315;
    private readonly float reboundForceIncrease = 0.1f;

    private void Awake()
    {
        birdRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleHorizontalMovement();
        HandleVerticalMovement();
        HandleRotation();
    }

    private void HandleHorizontalMovement()
    {
        var previousX = transform.position.x;

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * horizontalSpeed);

        if (transform.position.x > horizontalRange || transform.position.x < -horizontalRange) {
            transform.position = new Vector3(previousX, transform.position.y, transform.position.z);
        }
    }
    private void HandleVerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            birdRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            transform.localEulerAngles = Vector3.right * facingUpAngle;
        }

        var reboundForce = jumpForce * reboundForceIncrease;
        if (transform.position.y > verticalRange) {
            birdRb.AddForce(Vector3.down * reboundForce, ForceMode.VelocityChange);
            transform.localEulerAngles = Vector3.right * facingDownAngle;
        } else if (transform.position.y < -verticalRange) {
            birdRb.AddForce(Vector3.up * reboundForce, ForceMode.VelocityChange);
            transform.localEulerAngles = Vector3.right * facingUpAngle;
        }
    }
    private void HandleRotation()
    {
        var previousEulerAngles = transform.localEulerAngles;
        transform.Rotate(Vector3.left * birdRb.velocity.y);
        if (transform.localEulerAngles.x < facingUpAngle || transform.localEulerAngles.x > facingDownAngle) {
            transform.localEulerAngles = previousEulerAngles;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) {
            GameHandler.Instance.GameOver();
        }
    }
}
