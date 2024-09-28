using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class Mini_UICtrl : MonoBehaviour
{
    //플레이어 스크립트
    [SerializeField] Mini_MarioCtrl player;

    // 게임 클리어 & 게임오버 UI 관련
    [SerializeField] GameObject clearUiImage;
    [SerializeField] GameObject GameOverUiImage;
    public bool isTouchGoal; //T클리어o, F클리어x
    public bool isPlayerDead; //F게임돌아가는중, T게임오버

    //코인획득관련
    [SerializeField] Mini_Coin coin;   //코인 스크립트
    [SerializeField] TMP_Text coinImageTMP;
    [SerializeField] public int coinCount;

    ////목숨관련
    //[SerializeField] TMP_Text lifeImageTMP;
    //[SerializeField] int lifeCount;



    private void Awake()
    {
        // 게임 클리어 & 게임오버 UI 관련        
        clearUiImage.SetActive(false);
        GameOverUiImage.SetActive(false);
        isTouchGoal = false;
        isPlayerDead = false;

        // 코인 관련
        coinCount = 0;

    }
    private void Start()
    {
        // 게임 클리어 & 게임오버 UI 관련
        Time.timeScale = 1f;
        player.OnTouchDoor += TurnOnUI;
        player.OnPlayerDead += PlayerDie;
        player.OnPickUpCoin += PickUpCoin;

        // 코인 관련
        coinCount = 0;

    }


    private void Update()
    {
        ShowCoinCount();

        if (isTouchGoal)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                ReLoadGame();
            }

        }


    }



    private void PlayerDie()
    {
        Debug.Log("게임 오버..");
        //코루틴 2초 걸고싶다
        isPlayerDead = true;
        GameOverUiImage.SetActive(true);
        Time.timeScale = 0f;

    }

    private void TurnOnUI()
    {
        Debug.Log("게임 클리어");
        isTouchGoal = true;
        clearUiImage.SetActive(true);
        Invoke("timeStop", 2f);

    }

    public void ReLoadGame()
    {
        SceneManager.LoadScene("0920_Mini_LSH");
    }


    public void PickUpCoin()
    {
        coinCount += 1;
        player.OnPickUpCoin -= PickUpCoin;
    }

    private void ShowCoinCount()
    {
        coinImageTMP.text = $"X {coinCount}";

    }


    private void timeStop()
    {
        Time.timeScale = 0f;
    }
    IEnumerator timeStopRoutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
    }

}
