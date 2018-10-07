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
		mousePosition.z = Camera.main.farClipPlane - 1;
		transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
	}
	
}
