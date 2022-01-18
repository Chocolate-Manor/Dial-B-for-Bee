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
            Vector2 dir = new Vector2(mouseGameCoords.x - transform.position.x,
                mouseGameCoords.y - transform.position.y);
            transform.up = dir;

            //Handle B walking animation and sound
            HandleWalking(dir);
        }

        ;

    }
    
    /// <summary>
    /// Ensure B to walk
    /// When walking forward he will have a different animation compare to backward and sideways.
    /// Also plays walking sound
    /// </summary>
    /// <param name="dir">the mouse direction vector</param>
    private void HandleWalking(Vector2 dir)
    {
        //get the velocity in mouse direction, normalized
        float velocityInDirection = Vector3.Dot(rb.velocity.normalized, dir.normalized);
            
        //set the animation blend
        animator.SetFloat("Blend",  velocityInDirection);

        //check if there's input
        bool hasInput = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0; 
            
        //set animator boolean
        if (hasInput)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //play walking sound
        if (hasInput)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }
    }

    //Handles the actual input of B walking. 
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