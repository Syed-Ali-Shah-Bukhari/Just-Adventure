using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Skeleton_Scripts : Enemy, IDamagable
{

    public int Health { get; set; }
    private bool Dealth = false;

    public GameObject KeyPrefab;
    public void damage()
    {
        Health--;
        if (AS.isPlaying == false)
            PlayAudios(0);
        health = Health;

        slider.value = health;
        if (Health < 0)
        {
            if (Dealth == false)
            {
                anim.SetTrigger("dealth");
                Instantiate(KeyPrefab, this.transform.position, Quaternion.identity);
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




}
