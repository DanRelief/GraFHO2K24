using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;



    
    private Player player;
    private Animator anim;
    private bool isHitting;
    private float recoveryTime = 1.5f;
    private float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();

        timeCount += Time.deltaTime;
           
        if(timeCount >= recoveryTime)       
        {
            isHitting = false;
            timeCount = 0f;
        }
    }

    #region Movement

    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            anim.SetInteger("Transition", 1);
        }
        else
        {
            anim.SetInteger("Transition", 0);
        }
        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        if (player.IsCutting)
        {
            anim.SetInteger("Transition", 3);
        }

    }
    void OnRun()
    {
        if (player.isRunning)
        {
            anim.SetInteger("Transition", 2);
        }
    }

    #endregion

    #region Attack
    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null)
        {
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion

    public void OnHit()
    {
        if(!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
        
    }  


}

