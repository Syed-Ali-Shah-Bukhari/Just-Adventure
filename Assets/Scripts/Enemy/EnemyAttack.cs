using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    bool isCheck = true;
    protected  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScripts player = collision.GetComponent<PlayerScripts>();
            if (player != null)
            {
                if (isCheck == true)
                {
                    isCheck = false;
                    player.PlayerDamage();
                }
                
                StartCoroutine(restisCheck());
            }
        }
    }

    IEnumerator restisCheck()
    {
        yield return new WaitForSeconds(0.5f);
        isCheck = true;
    }
}
