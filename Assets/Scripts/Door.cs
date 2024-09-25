using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] GameObject clearImageUI;

    private void Start()
    {
        Debug.Log(0);
        clearImageUI.SetActive(false);
        player.OnClear += GameClear;
    }


    public void GameClear()
    {
        Debug.Log(2);
        clearImageUI.SetActive(true);
        Time.timeScale = 0f;
    }


}
