using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackVisuals : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem attackParticles;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            DoAttackVisuals();
        }
    }
    public void DoAttackVisuals()
    {
        anim.SetTrigger("DoAttack");
        attackParticles.Play();
    }
}
