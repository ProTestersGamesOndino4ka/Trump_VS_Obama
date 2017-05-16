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
			LocalPresident.onWin();
			EnemyPresident.onLose();
		}
		else if(localScore < enemyScore)
		{
			LocalPresident.onLose();
			EnemyPresident.onWin();
		}
		else
		{
			LocalPresident.onDraw();
			EnemyPresident.onDraw();
		}
	}

}
