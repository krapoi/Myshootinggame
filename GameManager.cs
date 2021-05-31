using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxspwan;
    public float curspwan;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeimage;
    public GameObject gameOverSet;

    void Update()
    {
        curspwan += Time.deltaTime;

        if(curspwan > maxspwan)
        {
            SpawnEnemy();
            maxspwan = Random.Range(0.2f, 2f);
            curspwan = 0;
        }

        //# Ui score
        player playerLogic = player.GetComponent<player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); 
    }
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 12);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy],
                    spawnPoints[ranPoint].position,
                    spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        if(ranPoint == 5 || ranPoint == 6 ) //오른쪽에서 아래로 스폰
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);

        }
        else if(ranPoint == 7) // 오른쪽에서 위로 스폰
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), 1);
        }
        else if (ranPoint == 8 || ranPoint == 11  ) //왼쪽에서 아래로 스폰
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else if(ranPoint == 10 || ranPoint == 9) // 왼쪽에서 위로 스폰
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, 1);
        }
        else //직선 스폰
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
    public void UpdateLifeIcon(int life)
    {
        for(int index = 0; index < 1; index++){
            lifeimage[index].color = new Color(1, 1, 1, 0);
        }

        for(int index = 0; index< life; index++)
        {
            lifeimage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void RespwanPlayer()
    {
        Invoke("RespwanPlayerExe", 2f);

    }
     void RespwanPlayerExe()
    {
       
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        player playerLocgic = player.GetComponent<player>();
        playerLocgic.isHit = false;
    }
    public void GameOver()
    {

        gameOverSet.SetActive(true);
    }

}
