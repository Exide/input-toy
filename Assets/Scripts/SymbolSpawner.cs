using UnityEngine;

public class SymbolSpawner : MonoBehaviour {
    public GameObject symbolPrefab;

    private void Update() {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            
            if (Input.GetKeyDown(keyCode)) {
                instantiateSymbol(keyCode.ToString());
            }
        }
    }

    private void instantiateSymbol(string symbol) {
        var position = getRandomVisiblePosition();
        var rotation = Quaternion.identity;
        var obj = Instantiate(symbolPrefab, position, rotation);
        var mesh = (TextMesh) obj.GetComponent(typeof(TextMesh));
        mesh.text = symbol;
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
