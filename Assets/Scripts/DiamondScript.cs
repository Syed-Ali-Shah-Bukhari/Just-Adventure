using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiamondScript : MonoBehaviour
{
    public int germs = 1;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerScripts player = other.GetComponent<PlayerScripts>();
            if (player != null)
            {
                player.diamonds += germs;
                Destroy(this.gameObject);
            }

        }
    }
}
