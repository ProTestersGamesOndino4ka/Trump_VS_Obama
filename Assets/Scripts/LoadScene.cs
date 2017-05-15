using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadScene
{
	public static void LoadFreePresidentScene()
	{
		SceneManager.LoadScene("scene_freePresident");
	}

	public static void LoadLocalGameScene()
	{
		SceneManager.LoadScene("scene_local");
	}

	public static void LoadOnlineGameScene()
	{
		SceneManager.LoadScene("scene_online");
	}

	public static void LoadGamemodeScene()
	{
		SceneManager.LoadScene("scene_gamemode");
	}
}
