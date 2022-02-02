using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    //public SceneFader fader;
    public Button[] levelButtons;

	// Use this for initialization
	void Start () {

        int levelReached = PlayerPrefs.GetInt("levelReached", 1); //1 is default value if game is played for first time for example.

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i+1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
