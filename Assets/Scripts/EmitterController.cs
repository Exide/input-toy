using UnityEngine;

public class EmitterController : MonoBehaviour {
	
	private void Start () {
		if (!Input.mousePresent) {
			gameObject.SetActive(false);
		}
	}

	private void Update () {
		if (!gameObject.activeSelf) return;
		if (Camera.main == null) throw new NoCameraException();
		var mousePosition = Input.mousePosition;
		mousePosition.x = Mathf.Clamp(mousePosition.x, 10, Screen.width - 10);
		mousePosition.y = Mathf.Clamp(mousePosition.y, 10, Screen.height - 10);
		mousePosition.z = Camera.main.farClipPlane - 1;
		transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
	}
	
}
