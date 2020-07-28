using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScripts : MonoBehaviour
{
    private PlayerAnimation animation;

    public FixedJoystick joystick;
    public SpriteRenderer PlayerSpirite;

    public Animator anim;
    public Vector2 velocity;
    public float movSpeed = 2.0f, jumpSpeed = 4.0f;
    public bool isGround;
    public int diamonds;
    public Text diamondsUI;
    public GameObject PlayerHealthUI;
    public int PlayerHealth = 5;
    public static bool isFire { get; set; }
    public LayerMask layer;

    public AudioClip[] Audio;


    float joyStickControl;
    bool isJump = false;
    bool isWalking = false;
    Rigidbody2D rb2D;
    private AudioSource AS;


    private void Awake()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
        animation = this.GetComponent<PlayerAnimation>();
        AS = this.GetComponent<AudioSource>();

    }
    private void FixedUpdate()
    {
        joyStickControl = joystick.Horizontal;
        DetectGround();
        //  Debug.Log(joystick.Horizontal);
        PlayerMovement();
        // Attack();
        CountDiamonds();

    }
    void DetectGround()
    {
        float distance = 1.6f;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, distance, layer);
        if (hit)
        {

            Debug.DrawRay(this.transform.position, Vector2.down * distance, Color.red);
            isGround = true;
            animation.JumpPlayer(false);

        }
        else
        {
            Debug.DrawRay(this.transform.position, Vector2.down * distance, Color.green);
        }
    }

    public void Attack()
    {
        isFire = false;
        if (isGround == true && joyStickControl != 0.0f)
        {
            animation.RunAttack();
            if (AS.isPlaying == false)
                PlayAudios(3);
        }
        else if (isGround == true && joyStickControl == 0.0f)
        {
            animation.SwingAttack(isFire);
            if (AS.isPlaying == false)
                PlayAudios(3);
        }

    }


    public void Jump()
    {
        if (AS.isPlaying == false && isGround == true)
            PlayAudios(0);

        isJump = true;
    }
    void PlayerMovement()
    {

        if (isGround == true && isJump == true)
        {
            velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            isJump = false;
        }
        else
        {
            if (isGround == false)
            {
                anim.SetBool("Run", false);
                animation.JumpPlayer(true);
            }
            else
            {
                bool check = joyStickControl != 0.0f ? true : false;
                anim.SetBool("Run", check);
                animation.JumpPlayer(false);
                if (check == true)
                {
                    if (AS.isPlaying == false)
                    {
                        PlayAudios(2);
                        isWalking = true;
                    }
                    //  Debug.Log(AS.clip.length);
                }
                else
                {
                    if (isWalking == true)
                    {
                        AS.Stop();
                        isWalking = false;
                    }
                }
            }


            flipPlayer();
            velocity = new Vector2(joyStickControl * movSpeed, rb2D.velocity.y);


        }

        rb2D.velocity = velocity;
    }
    private void flipPlayer()
    {
        if (joyStickControl < 0.0f)
        {
            PlayerSpirite.flipX = true;

        }
        else if (joyStickControl > 0.0f)
        {
            PlayerSpirite.flipX = false;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Player_G")
        {
            PlayAudios(1);
        }
        if (collision.collider.tag == "Key")
        {
            GateScripts._gateScripts.PlayerKeyText.text = "1";
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player_G")
        {
            isGround = false;
        }
    }

    private bool death = false;
    public void PlayerDamage()
    {
        if (AS.isPlaying == false)
            PlayAudios(4);

        PlayerHealth--;
        PlayerHealthUI.transform.GetChild(PlayerHealth).transform.gameObject.SetActive(false);
        if (PlayerHealth < 1)
        {
            Invoke("BackToDashBoard", 1.5f);
            if (death == false)
            {
                anim.SetTrigger("dealth");
                
                Destroy(this.gameObject, 2.0f);
                
                death = true;
            }
            
            return;
        }
        anim.SetTrigger("hitted");
    }
    public void BackToDashBoard()
    {
        SceneManager.LoadScene(0);
    }
    void CountDiamonds()
    {
        diamondsUI.text = diamonds.ToString();
    }


    void PlayAudios(int index)
    {
        AS.clip = Audio[index];
        AS.Play();
    }
}
