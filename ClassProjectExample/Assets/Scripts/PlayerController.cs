using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Components")]
    public Rigidbody rig;

    [Header("Statistics")]
    public int health;
    public int coinCount;

    void Update()
    {
        // input for jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        // input for movement
        Move();

        // if our health goes down to 0, the game restarts
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    void Move()
    {
        // get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // calculate a direction relative to where we're facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;
        // set that as our velocity
        rig.velocity = dir;
        //ANIMATION HERE
    }

    void TryJump()
    {
        // create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);
        // shoot the raycast
        if (Physics.Raycast(ray, 1.5f))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if we have collided with the Enemy, we will take damage
        if (other.gameObject.name == "Enemy")
        {
            health -= 5;
        }
    }
}