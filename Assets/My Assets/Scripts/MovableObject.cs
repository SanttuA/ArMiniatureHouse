using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {

	Vector3 distance;
	float posX;
	float posY;

	void OnMouseDown(){
		distance = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - distance.x;
		posY = Input.mousePosition.y - distance.y;

		User.movableObject = this.transform;
	}

	void OnMouseDrag(){
		Vector3 curPos = 
			new Vector3(Input.mousePosition.x - posX, 
				Input.mousePosition.y - posY, distance.z);  

		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
		transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.z);
	}
}
