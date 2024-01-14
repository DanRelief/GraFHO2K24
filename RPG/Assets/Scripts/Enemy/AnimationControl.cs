using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private PlayerAnim player;
    private Animator anim;
    private Skeleton skeleton;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();
    }
    public void PlayAnim(int value)
    {
        anim.SetInteger("Transition", value);
    }
    public void Attack()
    {
        if(!skeleton.isDead)
        {
            print("entrei");
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                GameObject _player = GameObject.FindGameObjectWithTag("Player");
                _player.GetComponent<Player>().hit += 1;
                int a = _player.GetComponent<Player>().hit;
                print(a);
                player.OnHit();

            }
        }
        
    }

    public void OnHit()
    {

        if(skeleton.currentHealth <= 0)
        {

            skeleton.isDead = true;
            Destroy(skeleton.gameObject.GetComponent<Collider2D>());

            anim.SetTrigger("death");
            Points.pontuacao += 1;

            Destroy(skeleton.gameObject, 1f);
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth--;

            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
