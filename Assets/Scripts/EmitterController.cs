using System;
using UnityEngine;

public class EmitterController : MonoBehaviour {

	private const int SCREEN_PADDING = 10;
	
	private void Start () {
		if (!Input.mousePresent && !Input.touchSupported) {
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
		var pointerPosition = getPointerScreenSpacePosition();
		if (!pointerPosition.HasValue) return;
		transform.position = getPointerWorldSpacePosition(pointerPosition.Value);
	}

	private Vector3? getPointerScreenSpacePosition() {
		if (Input.mousePresent) {
			return Input.mousePosition;
		}

		if (Input.touchSupported && Input.touches.Length > 0) {
			return Input.touches[0].position;
		}

		return null;
	}

	private Vector3 getPointerWorldSpacePosition(Vector3 screenPosition) {
		var x = Mathf.Clamp(screenPosition.x, SCREEN_PADDING, Screen.width - SCREEN_PADDING);
		var y = Mathf.Clamp(screenPosition.y, SCREEN_PADDING, Screen.height - SCREEN_PADDING);
		var z = Camera.main.farClipPlane - 1;
		var position = new Vector3(x, y, z);
		return Camera.main.ScreenToWorldPoint(position);
	}
	
	private void updateParticleColor() {
		var mouseWheelDelta = Input.GetAxis("Mouse ScrollWheel") / 10;
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
