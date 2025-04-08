using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스 (다른 스크립트에서 쉽게 접근 가능하게 함)
    public static GameManager Instance;

    // 현재 선택된 카드 두 장
    public Card firstCard;
    public Card secondCard;

    // 타이머 관련 UI
    public Text timeTxt;
    float time = 60.0f; // 총 시간 (60초)

    // 게임 오버 패널 및 재시작 버튼
    public GameObject gameOverPanel;
    public Button restartButton;

    // 긴박한 음악이 재생됐는지 확인하는 플래그
    private bool isUrgentMusicPlayed = false;

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // 게임 오버 패널 비활성화
        gameOverPanel.SetActive(false);

        // 재시작 버튼에 클릭 이벤트 연결
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        // 시간이 남아있는 동안만 타이머 작동
        if (time > 0)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2"); // 소수점 2자리까지 표시

            // 시간이 20초 이하로 줄어들면 긴박한 음악 재생 & 타이머 색상 변경
            if (time <= 20f && !isUrgentMusicPlayed)
            {
                AudioManager.Instance.PlayUrgentBGM();
                timeTxt.color = Color.red;
                isUrgentMusicPlayed = true;
            }

            // 시간이 0 이하로 떨어지면 게임 오버 처리
            if (time <= 0)
            {
                time = 0;
                GameOver();
            }
        }
    }

    // 게임 오버 처리 함수
    void GameOver()
    {
        AudioManager.Instance.PlayGameOverSound(); // 게임 오버 사운드 재생
        gameOverPanel.SetActive(true); // 게임 오버 UI 표시
    }

    // 재시작 버튼 클릭 시 실행
    public void RestartGame()
    {
        AudioManager.Instance.PlayNormalBGM(); // 기본 BGM 재생
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    public int totalPairs = 10; // 전체 카드 짝 수 (10쌍)
    private int matchedPairs = 0; // 현재 맞춘 짝 수

    // 카드 두 장 비교 후 처리
    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            // 같은 카드일 경우 제거하고 짝 카운트 증가
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            matchedPairs++;

            // 모든 짝을 맞췄으면 클리어 씬으로 전환
            if (matchedPairs >= totalPairs)
            {
                Invoke("LoadEndScene", 0.1f); // 잠깐의 딜레이 후 전환
            }
        }
        else
        {
            // 다른 카드일 경우 다시 뒤집음
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        // 카드 선택 초기화
        firstCard = null;
        secondCard = null;
    }

    // 게임 클리어 시 EndScene 로드
    void LoadEndScene()
    {
        AudioManager.Instance.PlayClearBGM(); // 클리어 BGM 재생
        SceneManager.LoadScene("EndScene"); // 클리어 씬으로 이동
    }
}