using UnityEngine;

public class EmitterController : MonoBehaviour {
	
	private void Start () {
		if (!Input.mousePresent) {
			gameObject.SetActive(false);
		}
	}

	private void Update () {
		if (!gameObject.activeSelf) return;
		updateParticlePosition();
		updateParticleColor();
	}

	private void updateParticlePosition() {
		if (Camera.main == null) throw new NoCameraException();
		var mousePosition = Input.mousePosition;
		mousePosition.x = Mathf.Clamp(mousePosition.x, 10, Screen.width - 10);
		mousePosition.y = Mathf.Clamp(mousePosition.y, 10, Screen.height - 10);
		mousePosition.z = Camera.main.farClipPlane - 1;
		transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
	}
	
	private void updateParticleColor() {
		var mouseWheelDelta = Input.GetAxis("Mouse ScrollWheel");
		if (mouseWheelDelta == 0) return;
		var particleSettings = GetComponent<ParticleSystem>().main;
		particleSettings.startColor = modifyHue(particleSettings.startColor.color, mouseWheelDelta);
	}

	private Color modifyHue(Color color, float change) {
		float hue;
		float saturation;
		float value;
		Color.RGBToHSV(color, out hue, out saturation, out value);
		hue += change;
		hue = loopOnBoundary(hue, 0, 1);
		return Color.HSVToRGB(hue, saturation, value);
	}

	private float loopOnBoundary(float value, float minimum, float maximum) {
		var delta = maximum - minimum;
		if (value > maximum) return value - delta;
		if (value < minimum) return value + delta;
		return value;
	}
}
