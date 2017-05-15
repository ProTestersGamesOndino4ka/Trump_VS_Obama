using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
	public void LoadLocalGameMode()
	{
		LoadScene.LoadLocalGameScene();
	}

	public void LoadOnlineGameMode()
	{
		LoadScene.LoadOnlineGameScene();
	}


}
