using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler
{
	public static int localScore { get; private set; }

	public static int enemyScore { get; private set; }

	public static void IncreaseLocalScore()
	{
		localScore++;
		LocalScoreText.SetText(localScore.ToString());
		//GoogleAutho.Message();
        
	}

	public static void IncreaseEnemyScore()
	{
		enemyScore++;
		EnemyScoreText.SetText(enemyScore.ToString());
	}

	public static void ClearScores()
	{
		localScore = enemyScore = 0;
	}

	public static void ChooseWinner()
	{
		if(localScore > enemyScore)
		{
			LocalPresidentImage.onWin();
			EnemyPresidentImage.onLose();
		}
		else if(localScore < enemyScore)
		{
			LocalPresidentImage.onLose();
			EnemyPresidentImage.onWin();
		}
		else
		{
			LocalPresidentImage.onDraw();
			EnemyPresidentImage.onDraw();
		}
	}

}
