using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playerMovement : MonoBehaviour
{
    private Animator animator;
    public CharacterController controller;
    public float horizontalSpeed = 12f; // Increased speed for horizontal movement
    public float jumpSpeed = 8f; // Speed for jumping
    private float ySpeed;
    private bool isGrounded;
    private bool isJumping;

    public float forwardSpeed = 3.0f; // Initial forward speed
    public float speedIncrease = 1.0f; // Amount to increase forward speed every interval
    public float interval = 3.0f; // Time interval in seconds
    public float fallThreshold = -10f;

    private float scoreFloat = 0f; // Use a float for score accumulation
    public int score = 0;
    public Text scoreText;

    
    private bool isGameOver = false;
    public gameOverScreen gameOverScreen;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        StartCoroutine(IncreaseSpeed());
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        // Accumulate score gradually using Time.deltaTime
        scoreFloat += Time.deltaTime * 10;  // Adjust the multiplier as needed

        // Convert the accumulated score to an integer
        score = Mathf.FloorToInt(scoreFloat);

        // Update the score UI
        UpdateScoreUI();


        // Check if the player is grounded
        isGrounded = controller.isGrounded;
        
        // Handle horizontal and forward movement
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontal * horizontalSpeed, 0f, forwardSpeed);

        // Handle jumping
        if (isGrounded)
        {
            // Reset ySpeed to prevent unwanted downward force
            if (ySpeed < 0)
            {
                ySpeed = -2f;
                animator.SetBool("isGrounded", true);
                animator.SetBool("isJumping", false);
                isJumping = false;
                animator.SetBool("isFalling", false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed; // Apply jump force
                animator.SetBool("isJumping", true);
                isJumping = true;
            }
        }
        else
        {
            // Apply gravity when not grounded
            ySpeed += Physics.gravity.y * Time.deltaTime;

            // If the player is falling after a jump
            if (isJumping && ySpeed < 0)
            {
                animator.SetBool("isFalling", true);
            }
        }

  

        // Apply vertical movement
        Vector3 verticalMove = new Vector3(0f, ySpeed, 0f);
        controller.Move((moveDirection + verticalMove) * Time.deltaTime);
    }



    // Update the UI with the current score
    private void UpdateScoreUI()
    {

        scoreText.text = "Score: " + score;
        Debug.Log("Score updated: " + score);  // Add this line for debugging


    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverScreen.Setup(score);

        Debug.Log("Game Over!");
    }
    
    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            forwardSpeed += speedIncrease;
        }
    }
    
}
