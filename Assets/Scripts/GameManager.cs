using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() {
        if (GameManager.instance != null){
            Destroy(gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        FindObjectOfType<AudioManager>().StopPlaying("mainMenu");
        FindObjectOfType<AudioManager>().Play("ambience");
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    private bool died = false;

    //Referaces
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnimator;
    public Boss boss;
    public RectTransform bossHealthBar;
    public Animator bossHealthAnim;
    public Animator winMenuAnimator;
    public AudioManager audioManager;


    // Logic (keep track of money)
    public int coins;
    public int experience;
    public int GameManagerWeaponLevel;

    private void Start() {
        GameManagerWeaponLevel = GameObject.Find("Player").transform.GetChild(0).GetComponent<Weapon>().weaponLevel;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    //Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) 
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    // Upgrade Weapon
    public bool TryUpgradeWeapon() 
    {
        // Is the weapon max level?
        if(weaponPrices.Count <= weapon.weaponLevel) {
            FindObjectOfType<AudioManager>().Play("unavaiable");
            return false;
        }

        //Do we have enough cash to upgrade?
        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            GameManagerWeaponLevel++;
            FindObjectOfType<AudioManager>().Play("weaponUpgrade");
            return true;
        }

        FindObjectOfType<AudioManager>().Play("unavaiable");
        return false;
    }

    // Health Bar
    public void onHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }

    // Boss
    public void onBossBattleEnter() {
        boss = GameObject.Find("Boss").GetComponent<Boss>();

        FindObjectOfType<AudioManager>().StopPlaying("ambience");
        FindObjectOfType<AudioManager>().Play("bossBattleMusic");
        bossHealthAnim.SetTrigger("Show");
    }
    public void onBossHit()
    {
        float ratio = (float)boss.hitpoint / (float)boss.maxHitpoint;
        bossHealthBar.localScale = new Vector3(ratio, 1, 1);
    }

    //Experience System
    public int GetCurrentLevel() 
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max level
            {
                return r;
            }
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if(currLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
    }

    // Death and Win menu
    public void backToMainMenu()
    {
        if (player.isAlive == false)
        {
            deathMenuAnimator.SetTrigger("Hide");
        } else if (player.hasWon)
        {
            winMenuAnimator.SetTrigger("Hide");
        }

        FindObjectOfType<AudioManager>().Play("buttonClick");

        hitpointBar.localScale = Vector3.one;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Destroy(hud);
        Destroy(menu);
        Destroy(gameObject);
        died = true;
    }

    //Function to save the game
    public void SaveState() {
        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        string s = "";

        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += GameManagerWeaponLevel; //weapon.weaponLevel.ToString();

        Debug.Log(weapon.weaponLevel.ToString());

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {
        Debug.Log("Entering Load State");
        if (died)
            return;
            
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // create referances to the newly istanciated player and floating text manager
        player = GameObject.Find("Player").GetComponent<Player>();
        floatingTextManager = GameObject.Find("FloatingTextManager").GetComponent<FloatingTextManager>();
        weapon = GameObject.Find("Player").transform.GetChild(0).GetComponent<Weapon>();

        // Update the weapon level, coins and exp to the next scene      
        weapon.SetWeaponLevel(GameManagerWeaponLevel); 

        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        coins = int.Parse(data[0]);

        // Experience
        experience = int.Parse(data[1]);
        
    }
}
