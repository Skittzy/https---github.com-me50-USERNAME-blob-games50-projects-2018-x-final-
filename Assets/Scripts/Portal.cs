using UnityEngine;

public class Portal : Collidable
{
    public string sceneName;

    protected override void OnCollide(Collider2D coll) {
        if(coll.name == "Player") {
            //Teleport player deeper in to the dungeon
            GameManager.instance.SaveState();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
