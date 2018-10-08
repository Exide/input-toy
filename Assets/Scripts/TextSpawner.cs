using TMPro;
using UnityEngine;

public class TextSpawner : MonoBehaviour {
    public GameObject textPrefab;

    private void Update() {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            
            if (Input.GetKeyDown(keyCode)) {
                instantiateText(keyCode.ToString());
            }
        }
    }

    private void instantiateText(string text) {
        var position = getRandomVisiblePosition();
        var rotation = Quaternion.identity;
        var obj = Instantiate(textPrefab, position, rotation);
        var mesh = (TextMeshPro) obj.GetComponent(typeof(TextMeshPro));
        mesh.text = text;
    }

    private Vector3 getRandomVisiblePosition() {
        if (Camera.main == null) throw new NoCameraException();
        var x = Random.Range(100, Screen.width - 100);
        var y = Random.Range(100, Screen.height - 100);
        var z = Camera.main.farClipPlane - 1;
        var screenPosition = new Vector3(x, y, z);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
}
