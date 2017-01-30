using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Side_0Image : MonoBehaviour {

	Image _image;
	LinkedList<Sprite> _list;
	Object[] _images;
	void Start () {
		
		_image = GetComponent<Image> ();
		_images = Resources.LoadAll ("Images", typeof(Sprite));
		_list = new LinkedList<Sprite> (Resources.LoadAll ("Images", typeof(Sprite)).Cast<Sprite>());
		/*
		foreach (Sprite image in _images) {
			_list.AddFirst (image);
		}*/
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
