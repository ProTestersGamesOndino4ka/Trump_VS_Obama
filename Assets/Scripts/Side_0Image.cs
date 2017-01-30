using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Side_0Image : MonoBehaviour {

	Image _image;
	LinkedList<Sprite> _list;
	void Start () {		
		_image = GetComponent<Image> ();
		_list = new LinkedList<Sprite> (Resources.LoadAll ("Images", typeof(Sprite)).Cast<Sprite>());
	}

	public void NextImage()
	{	
		if (_list.Find (_image.sprite).Next == null) {
			_image.sprite = _list.First.Value;
		} else {
			_image.sprite = _list.Find (_image.sprite).Next.Value;
		}
	}

}
