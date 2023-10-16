using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;

    private float score = 0;
    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            score += MoveLeft.speed * Time.deltaTime / 2;
            scoreText.text = "Score : " + (int)score;
        }
    }
}
