using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;
    public PlayerData playerData = new PlayerData();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

