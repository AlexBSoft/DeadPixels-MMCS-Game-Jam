using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptyScene : MonoBehaviour
{
    // Фикс фигни с инвентарем
    void Start()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
