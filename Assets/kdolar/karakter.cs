using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class karakter : MonoBehaviour
{
    
    public int maxcan = 100;
    public GameObject cp;
    public int anlýkcan;
    Rigidbody2D rigid;
    public float hýz;
    Animator anim;
    public float zýpgüc;
    public Transform saldýrýnok;
    public float range = 1;
    public LayerMask dusmanlayer;
    public LayerMask yer;
    public bool yerdeyim;
    bool ölüyom = false;
    public Slider slider;
    public Slider mana;
    public bool saðabakýyom = true;
    public float Mana = 0;
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public Image skill1;
    private bool CanDash = true;
    private bool isdashing;
    private float dashingpower = 25f;
    private float dashingpowerair = 13f;
    private float dashingtime = 0.2f;
    private float dashingcooldawn = 2f;
    public bool zýpladým = false;
    bool cooldawnskillimage = false;
    public float newdashtime=2;
    public bool havadayým = false;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] Transform yerkontrol;
    [SerializeField] Transform saðkontrol;
    [SerializeField] Transform solkontrol;
    [SerializeField] Transform yukarýkontrol;
    List<string> isimListesi = new List<string>();
    public bool KnockFromRight;
    public bool cpsilme;
    public bool dusmanyukarýbakma;
    

    public List<Vector2> cpnoktalarý = new List<Vector2>();
    void Start()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anlýkcan = maxcan;
        skill1.fillAmount = 0;
        cpnoktalarý = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<checkpoint>().cpnoktalarý;
        cpsilme = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<checkpoint>().cpsilme;

        transform.position = new Vector2(cpnoktalarý[0].x , cpnoktalarý[0].y+1);
    }

    // Update is called once per frame
    void Update()
    {
      
        if (rigid.velocity.y <= -0.4f || rigid.velocity.y >= 0.4f)
        {
            havadayým = true;
        }
        else
        {
            havadayým = false;
        }
        if (cooldawnskillimage)
        {
            
            newdashtime -= Time.deltaTime;
            skill1.fillAmount = newdashtime / dashingcooldawn;
        }

        if (isdashing)
        {
            return;
        }

       
        mana.value = Mana;
        if (Mana > 100)
        {
            Mana = 100;
        }
        slider.value = anlýkcan;
        anim.SetBool("yerdeyim", !dusmanyukarýbakma);
        anim.SetFloat("y velocity", rigid.velocity.y);
        if (yerdeyim && Input.GetKeyDown(KeyCode.Space))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, zýpgüc);
            yerdeyim = false;
            hýz = 5;
            
            
        }

        if (Input.GetKeyDown(KeyCode.Z) && yerdeyim)
        {
            
            
            anim.SetBool("vuruyom", true);
        }
        if (Input.GetKeyDown(KeyCode.X) && yerdeyim)
        {

            if (anim.GetBool("vuruyom") == true)
            {
                anim.SetBool("attack2", true);
            }
            
        }
        
        if (anlýkcan <= 0 && ölüyom==false)
        {
            anim.SetTrigger("die");
            ölüyom = true;

            if (cpsilme)
            {
                cpnoktalarý.RemoveAt(0);
                

            }

            cpsilme = !cpsilme;

        }

        if(CanDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        duvarcheck();
        yercheck();
        if (isdashing)
        {
            return;
        }
        //Debug.Log(isimListesi.Count);
    
    float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            anim.SetBool("hareket", true);
        }
        else
        {
            anim.SetBool("hareket", false);
        }

        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1, 1);
            saðabakýyom = true;
        }
        if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            saðabakýyom = false;
        }

        if (KBCounter <= 0)
        {
            rigid.velocity = new Vector3(horizontal * hýz, rigid.velocity.y, 0);
        }
        else
        {
            
            if (KnockFromRight)
            {
                rigid.velocity = new Vector2(-KBForce, 1);
            }
            if(KnockFromRight == false)
            {
                rigid.velocity = new Vector2(KBForce, 1);
            }
            KBCounter -= Time.deltaTime;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("yer"))
        {
            yerdeyim = true;
        }
        if (collision.gameObject.CompareTag("trap") )
        {
            if(ölüyom == false)
            {
                if (cpsilme)
                {
                    cpnoktalarý.RemoveAt(0);


                }
                cpsilme = !cpsilme;

                anim.SetTrigger("die");
                ölüyom = true;


                


            }


        }
    }
   
    public void kýlýcanimson()
    {
        anim.SetBool("vuruyom", false);
        hýz = 5;
    }
    public void kýlýcanimsbasla()
    {
        hýz = 0;
    }
    public void Saldýr()
    {
        Collider2D[] vurulandusmanlar = Physics2D.OverlapCircleAll(saldýrýnok.position, range,dusmanlayer);

        foreach (Collider2D enemy in vurulandusmanlar)
        {
            enemy.gameObject.GetComponent<dusman>().Hasaral(25,transform.position.x);
            enemy.gameObject.GetComponentInChildren<ParticleSystem>().Play(true);
            Mana += 10;
        }
    }
   void yercheck()
    {

        dusmanyukarýbakma = true;
        Collider2D[] yerdekiler = Physics2D.OverlapCircleAll(yerkontrol.position, 0.2f, yer);
        if (yerdekiler.Length > 0)
        {
            dusmanyukarýbakma = false;
        }
    }
    void duvarcheck()
    {

        
        Collider2D[] sagduvar = Physics2D.OverlapCircleAll(saðkontrol.position, 0.2f, yer);
        if (sagduvar.Length > 0)
        {
            anim.SetInteger("duvark", 1);
        }

        Collider2D[] solduvar = Physics2D.OverlapCircleAll(solkontrol.position, 0.2f, yer);
        if (solduvar.Length > 0)
        {
            anim.SetInteger("duvark", 2);
        }

        Collider2D[] tavank = Physics2D.OverlapCircleAll(yukarýkontrol.position, 0.28f, yer);
        if (tavank.Length > 0 )
        {
            anim.SetInteger("duvark", 3);
            
        }
        
        if(sagduvar.Length == 0 && tavank.Length == 0)
        {
            anim.SetInteger("duvark", 0);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(saldýrýnok.position, range);
        
    }

    public void Hasaral(int hasar, float lokasyon)
    {
        if (isdashing)
        {
            return;
        }
        if(ölüyom)
        {
            return;
        }
        hýz = 5;
        anim.SetBool("vuruyom",false); 
        anlýkcan -= 25;
        anim.SetTrigger("hurt");
        if(lokasyon < transform.position.x)
        {
            KnockFromRight = false;

        }
        if (lokasyon > transform.position.x)
        {
            KnockFromRight = true;

        }
        KBCounter = KBTotalTime;
    }

    public void Die()
    {
        
        Destroy(this.gameObject);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    
    }


    private IEnumerator Dash()
    {
        if(Mana >= 10)
        {
            
            Mana -= 10;
            CanDash = false;
            isdashing = true;
            float originalgvr = rigid.gravityScale;
            rigid.gravityScale = 0f;
            float vertical = 0;
            if (Input.GetAxisRaw("Vertical") >= 0)
            {
                vertical = Input.GetAxisRaw("Vertical");
            }
            else
            {
                vertical = 0;
            }
            rigid.velocity = new Vector2(transform.localScale.x * dashingpower, Input.GetAxisRaw("Vertical") * dashingpowerair);
            tr.emitting = true;
            cooldawnskillimage = true;
            yield return new WaitForSeconds(dashingtime);
            tr.emitting = false;
            rigid.velocity = Vector2.zero;
            rigid.gravityScale = originalgvr;
            isdashing = false;
            newdashtime = dashingcooldawn;
            cooldawnskillimage = true;
            yield return new WaitForSeconds(dashingcooldawn);
            cooldawnskillimage = false;
            
            CanDash = true;
        }
        
        
    }
    public void vurus2son()
    {
        anim.SetBool("attack2", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bonefire"))
        {

            if (!cpnoktalarý.Contains(collision.transform.position))
            {
                cpnoktalarý.Insert(0, collision.transform.position);
                cpsilme = false;

            }
        }
    }


}
