using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    
    public void JumpPlayer(bool jump)
    {
        anim.SetBool("Jump", jump);
    }
    public void SwingAttack(bool isFire)
    {
        
        anim.SetTrigger ("Attack");
        anim.SetBool("FireAttack", isFire);
       
    }
    public void RunAttack()
    {
        anim.SetTrigger("Run_Attack");
       
    }

    
   
}

