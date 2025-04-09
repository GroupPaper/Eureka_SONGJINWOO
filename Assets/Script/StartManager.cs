using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �ʿ�
using UnityEngine.UI;              // UI(Button) ����� ���� �ʿ�

public class StartManager : MonoBehaviour
{
    public Button startButton;            // ���� ��ư (�ν����Ϳ��� ����)
    public AudioClip bgmClip;             // ������� (�ν����Ϳ��� ����)

    private AudioSource audioSource;      // ����� ����� �ҽ�

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // ������ҽ� ������Ʈ �߰�
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.clip = bgmClip;
        audioSource.volume = 0.5f;
        audioSource.Play(); // ����� ���

        // ��ư Ŭ�� �� �Լ� ȣ�� ���
        startButton.onClick.AddListener(StartScene);
    }

    // ��ư Ŭ�� �� ȣ��� �Լ�
    void StartScene()
    {
        // �� ��ȯ
        SceneManager.LoadScene("MainScene");
    }
}


