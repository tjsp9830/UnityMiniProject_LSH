using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : MonoBehaviour
{
    [SerializeField] Animation ani;

    void Start()
    {
        //�۾� �Ĺݺ�, �÷��̾ ��ó�� �������� ����ǰ� ��ġ��
        flowerMove();
    }

    private void flowerMove()
    {
        ani.Play();
    }

}
