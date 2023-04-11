using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {


    //属性值
        private float TimeValEnemyBorn;         
        //已被占用的位置列表
        private List<Vector3> itemPostionList = new List<Vector3>();
        public int rightX = 10;
        public int topY=8;

    //引用
         //用来装饰初始化地图所需物体的数组
         //0.Heart 1.Wall 2.Barriar 3.Born 4.River 5.Grass 6.AirBarriar
         public GameObject[] item;

   

    private void Awake()
    {
        //实例化外围墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, (topY + 1), 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -(topY + 1), 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-(rightX + 1), i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3((rightX + 1), i, 0), Quaternion.identity);
        }

        //实例化地图
        //0.Heart 1.Wall 2.Barriar 3.Born 4.River 5.Grass 6.AirBarriar
        //实例化0.Heart
        CreateItem(item[0], new Vector3(0, -topY, 0), Quaternion.identity);
        //用Wall吧Heart围起来
        CreateItem(item[1], new Vector3(-1, -topY, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -topY, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -(topY - 1), 0), Quaternion.identity);
        }
        //1.Wall
        for (int i=0; i < 45; i++)
        {
            CreateItem(item[1],CreateRandomPosition(),Quaternion.identity);
        }
        //2.Barriar
        for (int i=0; i < 45; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        //3.Player_Born
        GameObject go = Instantiate(item[3], new Vector3(-2, -topY, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;
        //3.Enemy_Born
        CreateItem(item[3], new Vector3(rightX, topY, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, topY, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(-rightX, topY, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4, 5);
        //4.River
        for (int i=0; i < 30; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        //5.Grass
        for (int i=0; i < 45; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPostionList.Add(createPosition);
    }

    //产生随机位置的方法
    private Vector3 CreateRandomPosition()
    {
        //不生成x=-10  10的两列，y=-4  4的两行
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-(rightX-1), rightX), Random.Range(-(topY-1), topY), 0);
            if ( ! HasThePosition(createPosition))
            {
                return createPosition;
            }
            
        }
    }

    //用来判断位置列表是否有这个位置
    private bool HasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPostionList.Count; i++)
        {
            if (createPos == itemPostionList[i])
            {
                return true;
            }
        }
        return false;
    }

    //产生敌人的方法
    private void CreateEnemy()
    {
        int num = Random.Range(0,3);
        Vector3 EnemyPos = new Vector3();
        if (0 == num)
        {
            EnemyPos = new Vector3(-rightX,topY,0);
        }
        else if (1 == num)
        {
            EnemyPos = new Vector3(0, topY, 0);
        }
        else
        {
            EnemyPos = new Vector3(rightX, topY, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }
}
