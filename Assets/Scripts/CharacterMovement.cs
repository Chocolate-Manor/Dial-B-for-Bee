using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 10;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioSource audioSource;
    
    // Update is called once per frame
    void Update()
    {
        
        if (!PauseMenu.IsPaused)
        {
            // get mouse coords
            var mouse = Input.mousePosition;
            var mouseGameCoords = Camera.main.ScreenToWorldPoint(mouse);

            // orient the transform towards mouse
            Vector2 dir = new Vector2(mouseGameCoords.x - transform.position.x, mouseGameCoords.y - transform.position.y);
            transform.up = dir;
            
            
            // if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            // {
            //     animator.SetBool("isWalking", true);
            // }
            // else
            // {
            //     animator.SetBool("isWalking", false);
            // }
        
            animator.SetFloat("Blend",  Input.GetAxis("Horizontal") + Input.GetAxis("Vertical"));
        

            //get the velocity in mouse direction, normalized
            float velocityInDirection = Vector3.Dot(rb.velocity.normalized, dir.normalized);
            //Debug.Log(velocityInDirection);
            
            animator.SetFloat("Blend",  velocityInDirection);

            bool hasInput = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0; 
            if (Mathf.Abs(velocityInDirection) > 0.7 && hasInput)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (hasInput)
            {
                audioSource.UnPause();
            }
            else
            {
                audioSource.Pause();
            }
            
            
        }

;
        

    }

    private void FixedUpdate()
    {
        // var moveVelocity = moveInput * speed;
        var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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