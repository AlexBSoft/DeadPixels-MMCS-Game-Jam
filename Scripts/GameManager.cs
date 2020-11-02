using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject logObject;
    public GameObject livesHolder;
    public GameObject CharacteristicsHolder;
    public GameObject livePrefab;
    public GameObject StageText;
    public float fadeOutTime = 0.03f;
    

    public GameObject Death;

    public GameObject Player;
    public PlayerController PlayerController;

    public int wave_number =0;
    public int enemies_left = 0;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public int difficulty_modifier;
    public int kills;
    public int totalkills;
    public int lives;
    
    private int lastlives;
    private string logtext;
    private bool gameEnded = false;
    //private Dictionary<string, float> oldcharacteristics = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        }
        PlayerController = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLives();
        UpdateCharacteristics();
        if(enemies_left <= kills && GameObject.Find("EnemiesHolder").transform.childCount < 1){
            totalkills+=kills;
            kills=0;
            wave_number++;
            enemies_left = wave_number*2;
            StartCoroutine(Wavego());
        }
        if(gameEnded){
             if (Input.anyKeyDown) {
                SceneManager.LoadScene("SampleScene"); 
                //PlayerController.inventory.Container.Items = new InventorySlot[14];
                //SceneManager.LoadScene("EMPTYSCENE");
            }
        }
        
        if (Input.GetKeyDown("r")) {
            //PlayerController.inventory.Container.Items = new InventorySlot[14];
            //Player.SetActive(false);
            PlayerController.ClearAll();
            GameObject.Find("InventoryScreen").GetComponent<DisplayInventory>().CreateSlots();
            //SceneManager.LoadScene("EMPTYSCENE");
        }
    }

    IEnumerator Wavego()
    {
        StageText.transform.GetComponent<TextMeshProUGUI>().color = Color.white;
        StageText.transform.GetComponent<TextMeshProUGUI>().text = "Stage "+wave_number;
        yield return new WaitForSeconds(3);
        StageText.transform.GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f,0f);
        yield return new WaitForSeconds(2);
        StartWave();
    }

    

    public void StartWave(){
        for (int i = 0; i < enemies_left; i++)
        {
            int pro = Random.Range(1,100);
            if(pro <=50){
                CreateEnemy(enemy1);
            }else if(pro > 50 && pro <= 80){
                CreateEnemy(enemy2);
            }else if(pro > 80 && pro <=100){
                CreateEnemy(enemy3);
            }
        }
    }
    public void CreateEnemy(GameObject prefab){
        var enemy = Instantiate(prefab,new Vector3(Random.Range(-10,10),Random.Range(-10,10)),Quaternion.identity);
        enemy.transform.SetParent(GameObject.Find("EnemiesHolder").transform);
    }

    public void LogText(string text){
        logtext+=text+"\r\n";
        int numLines = logtext.Split('\r').Length;
        if(numLines>6){
            int index = logtext.IndexOf("\r\n");
            logtext = logtext.Substring(index + 1);
        }
        logObject.GetComponent<TextMeshProUGUI>().text=logtext;
    }

    public void UpdateCharacteristics(){
        string text = "";
        foreach(KeyValuePair<PlayerBuffs,int> ch in PlayerController.characteristics)
        {
            text += ch.Key+": "+ch.Value+"\r\n";
        }
        foreach(KeyValuePair<string,int> ch in PlayerController.PlayerCurses)
        {
            if(ch.Key == "greed" && ch.Value>0)
                text += "Curse Greed \r\n";
            if(ch.Key == "stupid" && ch.Value>0)
                text += "Curse Stupid -10 int \r\n";
            if(ch.Key == "confusion" && ch.Value>0)
                text += "Curse Сonfusion \r\n";
        }
        CharacteristicsHolder.GetComponent<TextMeshProUGUI>().text=text;
    }

    public void DrawLives(){
        if(lives != lastlives){
            int xx = -50;
            foreach (Transform child in livesHolder.transform) {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = 1; i <= lives; i++)
            {
                var obj = Instantiate(livePrefab, new Vector3(xx*i, -32, 0), Quaternion.identity, transform);
                obj.transform.SetParent(livesHolder.transform);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(xx*i, -32, 0);
            }
            lastlives = lives;
        }
    }


    public void SetLives(int count = -1){
        lives += count;
        if(lives <1){
            LogText("You died");
            gameEnded=true;
            StageText.transform.GetComponent<TextMeshProUGUI>().text = "You died.";
            StageText.transform.GetComponent<TextMeshProUGUI>().color = Color.white;
            PlayerController.ClearAll();
            var particles = Instantiate(Death, Vector3.zero, Quaternion.identity, transform);
            // particles.transform.SetParent(Player.transform);
            particles.transform.position = Player.transform.position;
            
            Destroy(Player);
        }
    }
}
