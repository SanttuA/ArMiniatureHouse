using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

	private Vector3 startVector;
	private Vector3 currentVector;
	public static Transform movableObject;

	private Vector3 distance;
	private float posX;
	private float posY;

	void Update()
	{
		
		if (Input.touchCount == 1) 
		{
			if (Input.GetTouch (0).phase == TouchPhase.Began)
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
				if (Physics.Raycast (ray, out hit) && (hit.collider.tag == "Draggable"))
				{
					movableObject = hit.transform;
					distance = Camera.main.WorldToScreenPoint(movableObject.position);
					posX = Input.GetTouch (0).position.x - distance.x;
					posY = Input.GetTouch (0).position.y - distance.y;
				}
			}
			if (Input.GetTouch (0).phase == TouchPhase.Moved && movableObject != null)
			{
				Vector3 curPos = new Vector3(Input.GetTouch (0).position.x - posX, 
					Input.GetTouch (0).position.y - posY, distance.z);  

				Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
				movableObject.position = new Vector3(worldPos.x, movableObject.position.y, worldPos.z);
			}
			if(Input.GetTouch (0).phase == TouchPhase.Ended)
			{
				movableObject = null;
				//might have to null posx, posy, distance...
			}
		}

		if (Input.touchCount == 2)
		{
			if (Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch (1).phase == TouchPhase.Began)
			{
				startVector = Input.touches [0].position - Input.touches [1].position;
			}
			else if(Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (1).phase == TouchPhase.Moved)
			{
				currentVector = Input.touches [0].position - Input.touches [1].position;

				float startAngle = Mathf.Atan2 (startVector.y, startVector.x);
				float endAngle = Mathf.Atan2 (currentVector.y, currentVector.x);

				float deltaAngle = endAngle - startAngle;

				//apply rotation to the movableObject
				movableObject.Rotate(new Vector3(0, -deltaAngle, 0));
			}
		}


	}
	
}
