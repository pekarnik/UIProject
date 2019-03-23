using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PanelController : MonoBehaviour
{
	[SerializeField]
	private Transform _lowerPosition;
	[SerializeField]
	private Transform _middlePosition;
	[SerializeField]
	private Transform _upperPosition;
	private void OnMouseDrag()
	{
		if(Input.touchCount==1)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z = 0f;
			touchPosition.x = 0f;
			if (transform.position.y >= _lowerPosition.position.y)
			{
				transform.position = touchPosition;
			}
			else
			{
				transform.position = new Vector3(0,_lowerPosition.position.y,0);
			}
		}
	}

	private void OnMouseUp()
	{
		if (transform.position.y < _lowerPosition.position.y||
			Vector2.Distance(transform.position,_lowerPosition.position)
			<Vector2.Distance(_middlePosition.position,transform.position))
		{
			transform.position = new Vector3(0, _lowerPosition.position.y, 0);
		}
		else if(Vector2.Distance(transform.position, _middlePosition.position)
			< Vector2.Distance(_lowerPosition.position, transform.position)&&
			Vector2.Distance(transform.position, _middlePosition.position)
			< Vector2.Distance(_upperPosition.position, transform.position))
		{
			transform.position = new Vector3(0, _middlePosition.position.y, 0);
		}
		else if (transform.position.y > _upperPosition.position.y||
			Vector2.Distance(transform.position, _upperPosition.position)
			< Vector2.Distance(_middlePosition.position, transform.position))
		{
			transform.position = new Vector3(0, _upperPosition.position.y, 0);
		}
	}
}
