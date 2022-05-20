using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    private float score = 0.0f;

    private int difficultylevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;

    public Text scoreText;
    private bool isDead = false;
    public DeathMenu deathmenu;

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        if (score >= scoreToNextLevel)
        {
            LevelUp();

        }
        score += Time.deltaTime * difficultylevel;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp()
    {
        if(difficultylevel == maxDifficultyLevel)
        {
            return;
        }
        scoreToNextLevel *= 2;

        difficultylevel++;

        GetComponent<PlayerMotor>().SetSpeed(difficultylevel);
    }
    public void OnDeath()
    {
        isDead = true;
        deathmenu.ToggleEndMenu(score);
    }

}
