using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class Mini_UICtrl : MonoBehaviour
{
    //�÷��̾� ��ũ��Ʈ
    [SerializeField] Mini_MarioCtrl player;

    // ���� Ŭ���� & ���ӿ��� UI ����
    [SerializeField] GameObject clearUiImage;
    [SerializeField] GameObject GameOverUiImage;
    public bool isTouchGoal; //TŬ����o, FŬ����x
    public bool isPlayerDead; //F���ӵ��ư�����, T���ӿ���

    //����ȹ�����
    [SerializeField] Mini_Coin coin;   //���� ��ũ��Ʈ
    [SerializeField] TMP_Text coinImageTMP;
    [SerializeField] public int coinCount;

    ////�������
    //[SerializeField] TMP_Text lifeImageTMP;
    //[SerializeField] int lifeCount;



    private void Awake()
    {
        // ���� Ŭ���� & ���ӿ��� UI ����        
        clearUiImage.SetActive(false);
        GameOverUiImage.SetActive(false);
        isTouchGoal = false;
        isPlayerDead = false;

        // ���� ����
        coinCount = 0;

    }
    private void Start()
    {
        // ���� Ŭ���� & ���ӿ��� UI ����
        Time.timeScale = 1f;
        player.OnTouchDoor += TurnOnUI;
        player.OnPlayerDead += PlayerDie;
        player.OnPickUpCoin += PickUpCoin;

        // ���� ����
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
        Debug.Log("���� ����..");
        //�ڷ�ƾ 2�� �ɰ�ʹ�
        isPlayerDead = true;
        GameOverUiImage.SetActive(true);
        Time.timeScale = 0f;

    }

    private void TurnOnUI()
    {
        Debug.Log("���� Ŭ����");
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
