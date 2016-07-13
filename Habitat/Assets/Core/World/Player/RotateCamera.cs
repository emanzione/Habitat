using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {
	Vector2 _lastMousePos;
	Vector2 rotation;
	Vector3 _targetScale;
	Quaternion _targetRotation;
	// Use this for initialization
	void Start () {
		_lastMousePos = Input.mousePosition;
		rotation = new Vector2(0, 22f);
		_targetScale = transform.localScale;
		_targetRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
		transform.rotation = _targetRotation;
	}


	// Update is called once per frame
	void LateUpdate () {
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			if (Input.GetMouseButton(1)) {
				//Cursor.lockState = CursorLockMode.Locked;
				Vector2 diff = _lastMousePos - new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				rotation += new Vector2 (-diff.x, diff.y) / 3f;
				rotation.y = Mathf.Clamp (rotation.y, 10f, 80f);
				_targetRotation = Quaternion.Euler (rotation.y, rotation.x, 0);
			} /*else {
				Cursor.lockState = CursorLockMode.None;
			}*/
			_targetScale = new Vector3(_targetScale.x,_targetScale.y,Mathf.Clamp(_targetScale.z-Input.mouseScrollDelta.y/5f,.5f,10f));
		}

		transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, .2f);
		transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, .2f);
		_lastMousePos = Input.mousePosition;
	}
}
