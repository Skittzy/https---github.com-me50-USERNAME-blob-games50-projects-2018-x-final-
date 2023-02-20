using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void onPlayButtonClick() {
        FindObjectOfType<AudioManager>().Play("buttonClick");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Dungeon");
    }
}
