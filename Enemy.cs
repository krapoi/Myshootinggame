using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyname;
    public float speed;
    public int enemyscore;
    public int health;
    public Sprite[] sprites;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;

    public float maxshot;
    public float curshot;

    SpriteRenderer spriteRenderer;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
    
        Fire();
        Reload();
    }

    void Fire()
    {
        

        if (curshot < maxshot)
            return;
        if(enemyname == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

        }
        else if(enemyname == "L")
        {
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right*0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left*0.3f, transform.rotation);
            
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            
            rigidR.AddForce(dirVecR.normalized * 7, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 7, ForceMode2D.Impulse);
        }

        curshot = 0;
    }

    void Reload()
    {
        curshot += Time.deltaTime;
    }
    public void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("Retrunsprite", 0.1f);
        if(health <= 0)
        {
            player playerLogic = player.GetComponent<player>();
            playerLogic.score += enemyscore;
            Destroy(gameObject);
        }
    }

    void Retrunsprite()
    {
        spriteRenderer.sprite = sprites[0];


    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            Destroy(collision.gameObject);
        }
        
    }
}
