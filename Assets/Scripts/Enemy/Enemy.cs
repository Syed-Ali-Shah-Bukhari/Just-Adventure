using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int germs;
    [SerializeField]
    protected Transform[] points;
    [SerializeField]
    protected SpriteRenderer renderer;
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected GameObject Player;
    [SerializeField]
    protected GameObject DiamondPrefab;
    [SerializeField]
    float RayDistance = 1.5f;
    [SerializeField]
    protected Transform HealthBarPos;
    [SerializeField]
    protected Slider sliderPrefab;
    [SerializeField]
    protected AudioClip[] Audio;
    protected Slider slider;
    int countPoints, counter = 1;
    protected bool isCombat = false;
    protected float side = 1.0f;

    RaycastHit2D hit;
    Ray2D ray2D;
    protected AudioSource AS;

    public virtual void Init()
    {
        anim = this.GetComponentInChildren<Animator>();
        renderer = this.GetComponentInChildren<SpriteRenderer>();
        AS = this.GetComponent<AudioSource>();
        countPoints = points.Length;
    }
    private void Start()
    {
        Init();
    }
    public virtual void Update()
    {
        HealthBar();
        DetectPlayer();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("hit") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            return;

        MovementLogin();
       
    }

    protected void HealthBar()
    {
        try
        {
            Vector3 healthPos = Camera.main.WorldToScreenPoint(HealthBarPos.position);
            slider.transform.position = healthPos;
        }
        catch (System.Exception)
        {
            Debug.Log("Your Slider is Destory");
        }
    }
    protected void MovementLogin()
    {

        if (counter == countPoints)
        {
            counter = 0;
        }
        Vector2 dir = this.transform.position - points[counter].position;

        if (dir.magnitude > 1.0f)
        {
            float steps = speed;
            if (isCombat == false)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, points[counter].position, steps);
            }

            //Player Walk Animation;
        }
        else
        {
            PlayIdleAgain();
            counter++;
        }

        if (counter == 0)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
    }

    protected void DetectPlayer()
    {
        #region oldLogic
       

        if (renderer.flipX == true)
        {
            ray2D = new Ray2D(this.transform.position, -Vector2.right);
        }
        else
        {
            ray2D = new Ray2D(this.transform.position, Vector2.right);
        }

        hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, RayDistance, 1 << 9);
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(ray2D.origin, ray2D.direction * RayDistance, Color.red);
                isCombat = true;
                anim.SetBool("InCombat", true);
                anim.SetTrigger("idle");
                ResetAttack();
                
            }
        }
        else
        {
            Debug.DrawRay(ray2D.origin, ray2D.direction * RayDistance, Color.green);
            isCombat = false;
            anim.SetBool("InCombat", false);
        }
        #endregion
        #region newLogic
        try
        {
            Vector3 dis = this.transform.position - Player.transform.position;
            if (dis.magnitude < 4.0f)
            {
                if (dis.x > 0)
                {
                    renderer.flipX = true;
                    side = -1.0f;
                }
                else if (dis.x < 0)
                {
                    renderer.flipX = false;
                    side = 1.0f;
                }
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Player Destory");
        }
        
        #endregion
    }

    protected void PlayAudios(int index)
    {
        AS.clip = Audio[index];
        AS.Play();
    }
    void ResetAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            return;
        anim.SetTrigger("Attack");
    }
    void PlayIdleAgain()
    {
        anim.SetTrigger("idle");
    }



}
