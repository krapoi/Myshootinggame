using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float maxshot;
    public float curshot;

    public int life;
    public int score;
    public int boom;
    
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;
    public bool isHit;

    public GameObject bulletObjA;
    public GameObject BoomEffect;
    public GameManager manager;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();        
    }
    void Update()
    {
        Move();
        Playerboom();
        Fire();
        Reload();
    }
    void Fire()
    {
        if (!Input.GetKey(KeyCode.Z))
            return;

        if (curshot < maxshot)
            return;

        GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right*0.1f, transform.rotation);
        GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left*0.1f, transform.rotation);
        Rigidbody2D rigidR= bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curshot = 0;
    }

    void Playerboom()
    {
        if (!Input.GetKeyUp(KeyCode.X))
            return;
        if (boom <= 0)
            return;
        //Effect Visible
        BoomEffect.SetActive(true);
        Invoke("OffBoomEffect", 2f);
        //Remove Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int index=0; index < enemies.Length; index++)
        {
          Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
         enemyLogic.OnHit(100);
         }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for(int index = 0;index<bullets.Length;index++)
        {
            Destroy(bullets[index]);
        }

        boom--;
    }

    void OffBoomEffect()
    {
        BoomEffect.SetActive(false);
    }
    void Reload()
    {
        curshot += Time.deltaTime;
    }

        void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;


        if (Input.GetButtonDown("Horizontal") ||
            Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
                return;
            isHit = true;
            life--;
            boom = 2;
            manager.UpdateLifeIcon(life);
            
            if(life == 0)
            {
                manager.GameOver();
            }
            else
            {
            manager.RespwanPlayer();
            }
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
