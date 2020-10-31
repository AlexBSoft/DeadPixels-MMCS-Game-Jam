using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class IntroPrressAny : MonoBehaviour
{
    public GameObject tmpro;

    int screen = 1;
    public TextMeshProUGUI text;

    void Update()
    {
        if (Input.anyKeyDown) {
            switch (screen)
            {
                case 1: text.text= "Every item you pick up can be cursed."; screen++; break;
                case 2: text.text = "As long you are in the dungeon, then more difficult it becomes for you to survive."; screen++; break;
                case 3: text.text= "My advice: don't collect them all! Good luck, you will never get out."; screen++; break;
                default: SceneManager.LoadScene("SampleScene"); break; 
            }
        }
    }
}
