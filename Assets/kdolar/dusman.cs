using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class dusman : MonoBehaviour
{
    public bool tap�nma;
    public Animator kameraanim;
    public LayerMask mask;
    public int MaxCan = 100;
    Animator animator;
    public int Anl�kCan;
    public Transform target; 
    public float moveSpeed = 2f;
    public float jumpForce = 10f;
    public bool i�eride = false;
    public bool vurucam = false;
    public Transform vurusnok;
    private Rigidbody2D rb; 
    private bool isGrounded;
    public bool vuyrabilir = true;
    bool yerdeyim;
    public float KBForce = 3;
    public float KBCounter;
    public float KBTotalTime = 0.3f;
    bool KBvelocity = true;
    public bool KnockFromRight;
    
    
    void Start()
    {
        kameraanim = GameObject.Find("Virtual Camera").GetComponent<Animator>();
        Anl�kCan = MaxCan;
        animator = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Ground"));
        
        
        if (i�eride)
        {
            animator.SetBool("karakterhavada", GameObject.FindGameObjectWithTag("Player").GetComponent<karakter>().dusmanyukar�bakma);
        }
        
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<karakter>().z�plad�m)
        {
            animator.SetTrigger("karakterz�plad�");
        }
        if (Anl�kCan <= 0)
        {
            animator.SetTrigger("die");
            moveSpeed = 0;
        }
        animator.SetBool("tap�nma", tap�nma);
    }
    private void FixedUpdate()
    {
        KarakterKontrol();
        if (target.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
        }
        if (i�eride && vurucam == false && KBCounter <= 0 && yerdeyim && animator.GetBool("karakterhavada")==false)
        {
            animator.SetBool("hareket", true);
            Vector3 direction = new Vector3(target.position.x - transform.position.x,0,0);
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
                        
            


          

        }
        else
        {
            animator.SetBool("hareket", false);
        }
        
        if(KBCounter>0) {

            KBvelocity = true;
            if (KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForce, 1);
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, 1);
            }
            KBCounter -= Time.deltaTime;

        }
        if (KBCounter <= 0 && KBvelocity)
        {
            rb.velocity = Vector2.zero;
            KBvelocity = false;
        }

           
    }
    public void Hasaral(int hasar, float lokasyon) 
    {
        if(Anl�kCan>0)
        {
            KBCounter = KBTotalTime;
            moveSpeed = 0;
            Anl�kCan -= hasar;
            animator.SetTrigger("hurt");
            
            if (lokasyon < transform.position.x)
            {
                KnockFromRight = false;
            }
            if (lokasyon > transform.position.x)
            {
                KnockFromRight = true;

            }

        }

    }
    public void Die()
    {
        Destroy(gameObject); 
    }
   
    public void KarakterKontrol()
    {
        Collider2D[] alg�lanankarakter = Physics2D.OverlapCircleAll(transform.position, 6, mask);
        if(alg�lanankarakter.Length == 1)
        {
            tap�nma = false;
            
            i�eride = true;
            Collider2D[] vurulankarakter = Physics2D.OverlapCircleAll(vurusnok.position, 0.9f, mask);
            if(vurulankarakter.Length == 1 && vuyrabilir && Anl�kCan > 0)
            {
                
                animator.SetTrigger("hit");
                vuyrabilir = false;
                moveSpeed = 0;
                
            }
        }
        if(alg�lanankarakter.Length == 0)
        {
            i�eride = false;

        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 6);
        Gizmos.DrawWireSphere(vurusnok.position, 0.5f);
    }
    public void vurabilir()
    {
        vuyrabilir = true;
        moveSpeed = 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("yer"))
        {
            yerdeyim = true;
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            animator.SetTrigger("die");
            moveSpeed = 0;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("yer"))
        {
            yerdeyim = false;
        }
    }
    public void K�l�cVurus()
    {
        Collider2D[] vurulankarakter = Physics2D.OverlapCircleAll(vurusnok.position, 0.9f, mask);
        if (vurulankarakter.Length == 1 && Anl�kCan > 0)
        {

           
            foreach (Collider2D enemy in vurulankarakter)
            {

                if (enemy.GetComponent<karakter>().anl�kcan > 0)
                {
                    enemy.gameObject.GetComponent<karakter>().Hasaral(25, transform.position.x);
                    kameraanim.SetTrigger("takedamage");
                }

            }
        }
    }
   public void Hasaralbas()
    {
        Time.timeScale = 0.15f;
    }
    public void HasaralBit()
    {
        Time.timeScale = 1f;
    }
}

