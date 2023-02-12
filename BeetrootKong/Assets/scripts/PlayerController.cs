using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    float speed = 2.0f;
    float horizontalInput;
    float verticalInput;
    float jumpforce = 150.0f;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    bool isGrounded;
    bool canClimb = false;
    bool invincible = false;
    Color color = Color.white;
    int lives = 3;
    int finish = 0;
    public TextMeshProUGUI amountTXT;
    public TextMeshProUGUI chrono;
    public GameObject gameOver;
    public GameObject finishScreen;
    public GameObject finishButton;
    public GameObject gameController;
    public GameObject ballz;
    public GameObject farm;
    public SpriteRenderer[] endPlaces;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        SetLivesText();
        color.a = 1.0f;
        sprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(speed * Time.deltaTime * horizontalInput, 0, 0);
        if(horizontalInput > 0){
            sprite.flipX = false;
        }else if(horizontalInput < 0){
            sprite.flipX = true;
        }

        //tester is grounded
        if(Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(new Vector2(0f, jumpforce));
            isGrounded = false;
            canClimb = false;
        }

        //climb roots
        if(Input.GetAxis("Vertical") > 0 && canClimb){
            rb.gravityScale = 0;
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(0, speed * Time.deltaTime * verticalInput, 0);
        }

        if(lives == 0){
            gameObject.SetActive(false);
            gameOver.SetActive(true);
            finishButton.SetActive(true);
        }
    }

    void SetLivesText(){
        amountTXT.text = lives.ToString();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Platform"){
            isGrounded = true;
        }
        if(other.gameObject.tag == "Balls" && !invincible){
            //freeze, -1 life and place player at the start. resume
            lives--;
            SetLivesText();
            if(lives > 0){StartCoroutine(BackToStart());}

        }
        if(other.gameObject.tag == "Finish" && finish < 3){
            finish++;
            endPlaces[finish-1].color = new Color(255,255,255);
            StartCoroutine(BackToStart());
        }else if(other.gameObject.tag == "Finish" && finish == 3){
            finish++;
            endPlaces[finish-1].color = new Color(255,255,255);
            EndGame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Roots" && isGrounded){
            canClimb = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Roots"){
            canClimb = false;
            rb.gravityScale = 1;
        }
    }

    IEnumerator BackToStart(){
        transform.position = new Vector2(-4.0f, -3.85f); //start position
        invincible = true;
        Time.timeScale = 0;
        chrono.text = "3";
        yield return new WaitForSecondsRealtime(1);
        chrono.text = "2";
        yield return new WaitForSecondsRealtime(1);
        chrono.text = "1";
        yield return new WaitForSecondsRealtime(1);
        chrono.text = "GO";
        Time.timeScale = 1;
        color.a = 0.5f;
        sprite.color = color;
        yield return new WaitForSeconds(1);
        chrono.text = "";
        color.a = 1.0f;
        sprite.color = color;
        invincible = false;

    }

    void EndGame(){
        gameController.SetActive(false);
        ballz.SetActive(false);
        farm.SetActive(false);
        finishScreen.SetActive(true);
        finishButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
