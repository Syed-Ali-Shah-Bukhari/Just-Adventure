using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScripts : MonoBehaviour
{
    bool isChecked = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        IDamagable hit = collision.GetComponent<IDamagable>();
        if (hit != null)
        {
            Debug.Log(collision.gameObject.name);
            if (isChecked == true)
            {
                hit.damage();
                isChecked = false;
            }
            StartCoroutine(RestCheck());
        }
    }
    IEnumerator RestCheck()
    {
        yield return new WaitForSeconds(0.5f);
        isChecked = true;
    }
}
