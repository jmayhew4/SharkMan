using UnityEngine;
using System.Collections;

public class SharkmanController : MonoBehaviour {

    // setup some variables
    Animator animator;
    AudioSource soundController;
   
    Transform groundCheck;
    Transform spawnPoint;
    
    public float walkingSpeed;
    public float sprintingSpeed;
    bool facingRight = true;
    public bool isGrounded = true;
    public bool hasDoubleJumped = false;
    
    public AudioClip jumpSound;
    
    // Stats n such 
    public int maxHealth = 3;
    public int currentHealth;
	public int dogBonesCollected = 0;
    

    
    // Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        soundController = GetComponent<AudioSource>();
        spawnPoint = GameObject.Find("StartingSpawnPoint").transform;
        
        currentHealth = maxHealth;
       
	}
	
	// Update is called once per frame
	void Update () {
        // Checks if you can collide with something
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("PlatformLow"));
        if(isGrounded == true)
        {
            hasDoubleJumped = false;
            animator.SetBool("isJumping", false);
        }

        if (Input.GetAxis("Horizontal") > 0 && Input.GetButton("Sprint") == false)    // Walk Right
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(walkingSpeed, GetComponent<Rigidbody2D>().velocity.y);
            animator.SetBool("isWalking", true);
            animator.SetBool("isSprinting", false);
            if (facingRight == false){Flip();}
        }
        else if (Input.GetAxis("Horizontal") < 0 && Input.GetButton("Sprint") == false)  // Walk Left
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(walkingSpeed * -1, GetComponent<Rigidbody2D>().velocity.y);
            animator.SetBool("isWalking", true);
            animator.SetBool("isSprinting", false);
            if (facingRight == true){Flip();}
        } else if (Input.GetAxis("Horizontal") > 0 && Input.GetButton("Sprint") == true)  // Sprint Right
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(sprintingSpeed, GetComponent<Rigidbody2D>().velocity.y);
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", true);
            if (facingRight == false) { Flip(); }
        }else if (Input.GetAxis("Horizontal") < 0 && Input.GetButton("Sprint") == true)  // Sprint Left
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(sprintingSpeed * -1, GetComponent<Rigidbody2D>().velocity.y);
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", true);
            if (facingRight == true) { Flip(); }
        }

        else if (Input.GetAxis("Horizontal") == 0) // Standing Still
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", false);
        }

        /////////////////////////////////////////////////////////////////////////
        // SpawnPoint location reset, if player has fallen off the main platforms
        if (transform.position.y < -20)
        {
            transform.position = spawnPoint.position;
            currentHealth = currentHealth-1;

            //if no health in effect, just use either
            //transform.position = spawnPoint.position;
            //Die();
        }


        /////////////////////////////////////////////////////////////////////////
        // Jump scripting / Double Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping", true);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 11.5f), ForceMode2D.Impulse);
            soundController.PlayOneShot(jumpSound, .75f);
        }else if (Input.GetButtonDown("Jump") && isGrounded==false && hasDoubleJumped==false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 14), ForceMode2D.Impulse);
            hasDoubleJumped = true;
            soundController.PlayOneShot(jumpSound, .45f);
        }
        // Future proofing if health pickups becomes a thing
        if(currentHealth > maxHealth)
        {
			currentHealth = maxHealth;
		}
		
		if(currentHealth <= 0)
		{
			Die();
		}
    }

    // Flipping character
    void Flip (){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    void Die(){
		// load the game over screen
		Application.LoadLevel(3);
	}
}
