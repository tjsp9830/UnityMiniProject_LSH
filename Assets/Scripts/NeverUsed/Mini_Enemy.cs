using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mini_Enemy : MonoBehaviour
{
    
    // ����� �ʵ�
    //[SerializeField] Animation ani;
    //[SerializeField] float moveSpeed = 1f;


    //�����ڿ��� �ִ� �ʱ�ȭ (�ʼ����� �����غ���)
    public Mini_Enemy()
    {
        //ani = GetComponent<Animation>();
    }



    // �޼��带 FSM�� EagleŬ���� �����ؼ� 3���� ���� ��������� �ҵ�


    //�̵� �Լ�
    //����, �ź�: x���� ���� �̵�, ���α׷� ����� ����, ������ ������ (���� �������� ������)
    // ��:  y���� ���� �̵�, �÷��̾� ������ ����, ������ ���� (���� �÷��̾� �־����� �Ʒ��� �̵���)
    public abstract void EnemyMove();

    //�����ϴ� �Լ�
    //����, �ź�: �� �����ϴ� �Լ�. ���̶� �ε����� ������� ����
    // ��:  �÷��̾� �����ϴ� �Լ�. ĳ���Ͱ� ��ó�� ���� �̵�
    public abstract void EnemyDetect();

    //�ִϸ��̼� �÷��� �Լ�
    //����, �ź�: �ȴ� �ִϸ��̼� ����
    // ��:  �� �������� �ִϸ��̼� ����
    public abstract void EnemyAnimePlay();


}
