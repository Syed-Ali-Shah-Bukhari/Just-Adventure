using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nails_AttackScripts : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            PlayerScripts player = other.transform.GetComponent<PlayerScripts>();
            player.PlayerHealthUI.SetActive(false);
            player.anim.SetTrigger("dealth");
            //player.enabled = false;
            Destroy(player.gameObject, 2.0f);
            Invoke("BackToDashBoard", 2.0f);
        }
    }
    public void BackToDashBoard()
    {
        SceneManager.LoadScene(0);
    }
}
