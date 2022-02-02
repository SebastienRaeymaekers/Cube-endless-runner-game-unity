using UnityEngine;

public class DontDestoyObject : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
