using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] float speed;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    float horizontal;
    float vertical;
    float movementSpeed;
    Vector2 movementDirection;
    float lastDirectionHorizontal = 0;
    float lastDirectionVertical = 0;
    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        movementDirection = new Vector2(horizontal, vertical);
        rb.velocity = movementDirection * speed;
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        if(movementSpeed > 0)
        {
            lastDirectionHorizontal = horizontal;
            lastDirectionVertical = vertical;
            Animate(horizontal, vertical, movementSpeed);
        } else {
            Animate(lastDirectionHorizontal, lastDirectionVertical, 0);
        }
        float lastDir = transform.eulerAngles.y;
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void Animate(float horizontal, float vertical, float speed)
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Speed", speed);
    }

}