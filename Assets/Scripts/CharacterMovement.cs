using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 10;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip footstepSound;
    
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            // get mouse coords
            var mouse = Input.mousePosition;
            var mouseGameCoords = Camera.main.ScreenToWorldPoint(mouse);

            // orient the transform towards mouse
            var dir = new Vector2(mouseGameCoords.x - transform.position.x, mouseGameCoords.y - transform.position.y);
            transform.up = dir;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        // var moveVelocity = moveInput * speed;
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = moveInput * speed;
    }
    
    /// <summary>
    /// Associated with animation event of B walking. 
    /// </summary>
    public void playFootstepSound()
    {
        GameManager.Instance.PlaySoundEffect(footstepSound);
    }
}