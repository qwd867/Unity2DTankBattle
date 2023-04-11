using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {


    //属性值
    public int lifeValue = 1;
    public int playerScore = 2;
    public bool isDead;
    public bool isDefeat;

    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text playerLifeValue;
    public GameObject isDefeatUI;

    //单例
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerScoreText.text = playerScore.ToString();
        playerLifeValue.text = lifeValue.ToString();
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            return;
        }
        if (isDead)
        {
            Recover();
        }

	}
    private void Recover()
    {
        if (lifeValue <= 0)
        {
            //游戏失败，返回主界面
            isDefeat = true;
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
}
