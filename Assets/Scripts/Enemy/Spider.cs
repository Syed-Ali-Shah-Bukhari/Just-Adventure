using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spider : Enemy, IDamagable
{
    public int Health { get; set; }

    public GameObject Acid;



    private bool isCheck = true;
    private bool Dealth = false;
    private float AcidSpeed = 9.0f;
    public override void Init()
    {
        base.Init();
        Health = health;

        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        slider = Instantiate(sliderPrefab, Vector3.zero, Quaternion.identity) as Slider;
        slider.transform.SetParent(canvas.transform);
        slider.maxValue = Health;
        slider.value = Health;
        slider.minValue = -1;
    }
    public override void Update()
    {
        base.Update();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            if (isCheck == true)
            {
                GameObject acid = Instantiate(Acid, this.transform.position, Quaternion.identity);
                Rigidbody2D rb = acid.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.right * AcidSpeed * side;
                Destroy(acid, 2.0f);

                isCheck = false;
                StartCoroutine(Breath());
            }

        }

    }
    IEnumerator Breath()
    {
        yield return new WaitForSeconds(2.0f);
        isCheck = true;
    }
    public void damage()
    {
        Health--;
        if (AS.isPlaying == false)
            PlayAudios(0);
        health = Health;
        slider.value = health;
        if (Health < 1)
        {
            if (Dealth == false)
            {
                anim.SetTrigger("dealth");
                for (int i = 0; i < germs; i++)
                {
                    Instantiate(DiamondPrefab, this.transform.position + new Vector3(i, 0, 0), Quaternion.identity);
                }

                Destroy(this.gameObject, 2.0f);
                Destroy(slider.gameObject);
                AS.Stop();
                Dealth = true;
            }

            return;
        }
        anim.SetTrigger("hitted");
    }
}
