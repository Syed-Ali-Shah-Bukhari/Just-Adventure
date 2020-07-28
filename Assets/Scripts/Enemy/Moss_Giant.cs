using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Moss_Giant : Enemy, IDamagable
{
    public int Health { get; set; }
    private bool Dealth = false;
    
    public override void Init()
    {
        base.Init();
        Health =health ;

        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        slider = Instantiate(sliderPrefab, Vector3.zero, Quaternion.identity) as Slider;
        slider.transform.SetParent(canvas.transform);
        slider.maxValue = Health;
        slider.value = Health;
        slider.minValue = -1;
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
