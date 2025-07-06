using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapNode : MonoBehaviour
{
    public string sceneToLoad = "Battle";  // Scene that this node leads to
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log("Node clicked! Going to scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
