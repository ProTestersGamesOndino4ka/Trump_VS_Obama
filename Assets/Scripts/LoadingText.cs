using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
	static Text loadingText;

	void Awake()
	{
		loadingText = GetComponent<Text>();
	}

	public static void AddText(string textToAdd)
	{
		
		loadingText.text += "\n" + textToAdd;
	}

	public void OnCloudChoose()
	{
		SaveDataManager manager = new SaveDataManager();	
		manager.OnChooseDataSource(true);
	}

	public void OnLocalChoose()
	{
		SaveDataManager manager = new SaveDataManager();	
		manager.OnChooseDataSource(false);
	}

	public static void EnableButtons()
	{
		Button local = GameObject.Find("local_button").GetComponent<Button>();
		Button cloud = GameObject.Find("cloud_button").GetComponent<Button>();

		local.enabled = cloud.enabled = true;

		local.colors = cloud.colors = new ColorBlock(){ normalColor = Color.green };
	}

}
