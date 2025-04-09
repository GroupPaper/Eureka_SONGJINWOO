using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요
using UnityEngine.UI;              // UI(Button) 사용을 위해 필요

public class StartManager : MonoBehaviour
{
    public Button startButton;            // 시작 버튼 (인스펙터에서 연결)
    public AudioClip bgmClip;             // 배경음악 (인스펙터에서 연결)

    private AudioSource audioSource;      // 재생용 오디오 소스

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // 오디오소스 컴포넌트 추가
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.clip = bgmClip;
        audioSource.volume = 0.5f;
        audioSource.Play(); // 배경음 재생

        // 버튼 클릭 시 함수 호출 등록
        startButton.onClick.AddListener(StartScene);
    }

    // 버튼 클릭 시 호출될 함수
    void StartScene()
    {
        // 씬 전환
        SceneManager.LoadScene("MainScene");
    }
}


