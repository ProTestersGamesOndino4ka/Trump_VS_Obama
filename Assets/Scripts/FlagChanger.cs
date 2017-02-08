using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlagChanger : MonoBehaviour {

	private static Image _flagImage;
	private static List<Sprite> _listOfFlags;

	void Awake()
	{
		_listOfFlags = new List<Sprite> (Resources.LoadAll ("Flags", typeof(Sprite)).Cast<Sprite>());
		_flagImage = GetComponent<Image> ();
	}

	public static void SetFlagSprite(string presidentImageName)
	{
		_flagImage.sprite = _listOfFlags.Find (x => x.name == LocalRecords.allPresidents.Find (y => y.ImageName == presidentImageName).FlagName);
	}

}
