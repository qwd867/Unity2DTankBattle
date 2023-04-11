using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float moveSpeed = 10;

    private float Bullet_timeVal;
    public bool isPlayerBullet;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
        if (Bullet_timeVal > 3)
        {
            Destroy(gameObject);
            Bullet_timeVal = 0;
        }
        else
        {
            Bullet_timeVal += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//触发器
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("Die");//Tank死亡
                    Destroy(gameObject);//Bullet被销毁
                }
                break;
            case "Heart":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("Die");//Heart被击中
                }
                Destroy(gameObject);//Bullet被销毁
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("Die");//Enemy死亡  
                    Destroy(gameObject);//Bullet被销毁
                }
                
                break;
            case "Wall":
                Destroy(collision.gameObject);//Wall被销毁
                Destroy(gameObject);//Bullet被销毁
                break;
            case "Barriar":
                Destroy(gameObject);//BUllet被销毁
                break;

        }
    }
}

