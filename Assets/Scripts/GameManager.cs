using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerHealth player;
    public GameOverMenu gameoverMenu;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.HP <= 0 && !gameOver)
        {
            gameoverMenu.Notify();
            gameOver = true;
        }
    }
}
