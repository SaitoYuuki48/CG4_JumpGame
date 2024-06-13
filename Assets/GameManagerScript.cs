using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GameObject block;
    public GameObject background;
    public GameObject coin;
    public GameObject goal;

    public GameObject goalParticle;

    public TextMeshProUGUI scoreText;

    public static int score = 0;

    //配列の宣言
    int[,] map;

    // Start is called before the first frame update
    void Start()
    {
        map = new int[,]
        {
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            {1,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,1 },
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1 },
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,2,1 },
            {1,0,0,0,0,0,0,3,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 },
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            {1,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            {1,0,0,0,0,1,1,1,0,0,0,3,0,0,0,1,1,1,1,1,0,0,0,0,3,0,3,0,0,0,0,0,0,3,0,0,0,0,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        };

        int lenY = map.GetLength(0);
        int lenX = map.GetLength(1);

        Vector3 position = Vector3.zero;

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                position.x = x;
                position.y = -y + 5;
                if (map[y, x] == 1)
                {
                    Instantiate(block, position, Quaternion.identity);
                }

                if (map[y, x] == 2)
                {
                    goal.transform.position = position;
                    goalParticle.transform.position = position;
                }

                if (map[y, x] == 3)
                {
                    Instantiate(coin, position, Quaternion.identity);
                }

               
                
            }
        }

        for(int y = 0; y < lenY; y++)
        {
            for(int x = 0; x < lenX; x++)
            {
                position.x = x;
                position.y = -y + 5;
                position.z = 3;
                Instantiate(background, position, Quaternion.identity);
            }
        }

        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE " + score;


        //クリアでスペースキーで移行
        if(GoalScript.isGameClear == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
