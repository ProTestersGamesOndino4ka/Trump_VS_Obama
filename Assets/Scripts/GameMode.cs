using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
	public void LoadLocalGameMode ()
	{
		SceneManager.LoadScene ("scene_local");
	}

	public void LoadOnlineGameMode ()
	{
		SceneManager.LoadScene ("scene_online");
	}


}
