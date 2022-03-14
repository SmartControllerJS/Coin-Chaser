using UnityEngine;

public class EffectorMovement : MonoBehaviour
{

    public float speed = 3f;
    private Rigidbody2D e_RigidBody2D;
    private Vector2 screenBounds;

    void Start()
    {
        // Move the effector from the top to the bbottom of the screen
        e_RigidBody2D = this.GetComponent<Rigidbody2D>();
        e_RigidBody2D.velocity = new Vector2(0, -speed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        // Destroy the object once it's off screen
        if (transform.position.y < screenBounds.y * 2)
        {
            Destroy(this.gameObject);
        }
    }
}
