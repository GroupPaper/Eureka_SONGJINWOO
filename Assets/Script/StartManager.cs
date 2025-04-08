using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �ʿ�
using UnityEngine.UI;              // UI(Button) ����� ���� �ʿ�

// ���� ȭ�鿡�� ���Ǵ� �Ŵ��� ��ũ��Ʈ
public class StartManager : MonoBehaviour
{
    public Button startButton; // �ν����Ϳ��� ������ ���� ��ư

    // ���� ���� �� �����
    void Start()
    {
        // startButton�� Ŭ���Ǹ� LoadMainScene �Լ��� ����ǵ��� �̺�Ʈ ���
        startButton.onClick.AddListener(LoadMainScene);
    }

    // MainScene���� ���� ��ȯ�ϴ� �Լ�
    void LoadMainScene()
    {
        // �̸��� "MainScene"�� ���� �ε� (�� �̸��� ��Ȯ�� ��ġ�ؾ� ��)
        SceneManager.LoadScene("MainScene");
    }
}

