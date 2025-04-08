using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요
using UnityEngine.UI;              // UI(Button) 사용을 위해 필요

// 시작 화면에서 사용되는 매니저 스크립트
public class StartManager : MonoBehaviour
{
    public Button startButton; // 인스펙터에서 연결할 시작 버튼

    // 게임 시작 시 실행됨
    void Start()
    {
        // startButton이 클릭되면 LoadMainScene 함수가 실행되도록 이벤트 등록
        startButton.onClick.AddListener(LoadMainScene);
    }

    // MainScene으로 씬을 전환하는 함수
    void LoadMainScene()
    {
        // 이름이 "MainScene"인 씬을 로드 (씬 이름은 정확히 일치해야 함)
        SceneManager.LoadScene("MainScene");
    }
}

