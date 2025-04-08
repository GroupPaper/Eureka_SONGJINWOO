using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오디오를 전역에서 관리하는 AudioManager 클래스
public class AudioManager : MonoBehaviour
{
    // 싱글톤 패턴: 어디서든 AudioManager.Instance로 접근 가능
    public static AudioManager Instance;

    // 배경음 및 상황별 효과음 오디오 클립
    public AudioClip normalBGM;     // 일반 배경 음악
    public AudioClip urgentBGM;     // 긴박할 때 음악 (ex. 시간 부족)
    public AudioClip gameOverSound; // 게임 오버 시 효과음
    public AudioClip clearBGM;      // 클리어 시 음악

    private AudioSource audioSource;    // 오디오를 재생하는 컴포넌트
    private bool isUrgentPlaying = false; // 긴박 음악이 한 번만 재생되도록 체크

    // Awake는 씬이 로드될 때 가장 먼저 실행됨
    private void Awake()
    {
        // 싱글톤 설정: 인스턴스가 없으면 자신을 인스턴스로 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복 AudioManager 제거
            return;
        }
    }

    // 게임 시작 시 호출됨
    private void Start()
    {
        // AudioSource 컴포넌트를 동적으로 추가 및 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;         // 배경음은 반복 재생
        audioSource.playOnAwake = false; // 자동 재생 X
        audioSource.volume = 0.5f;       // 기본 볼륨 설정

        PlayNormalBGM(); // 처음에는 일반 배경음 재생
    }

    // 일반 배경음 재생 함수
    public void PlayNormalBGM()
    {
        audioSource.loop = true;
        audioSource.clip = normalBGM;
        audioSource.Play();
    }

    // 긴박한 상황용 배경음 재생 함수 (한 번만 재생되도록 체크)
    public void PlayUrgentBGM()
    {
        if (urgentBGM != null && !isUrgentPlaying)
        {
            audioSource.Stop(); // 현재 음악 정지
            audioSource.loop = true;
            audioSource.clip = urgentBGM;
            audioSource.Play();
            isUrgentPlaying = true;
        }
    }

    // 게임 오버 사운드 재생 함수
    public void PlayGameOverSound()
    {
        if (gameOverSound != null)
        {
            audioSource.Stop(); // 배경음 정지
            audioSource.loop = false; // 효과음은 반복 X
            audioSource.clip = gameOverSound;
            audioSource.Play();
        }
    }

    // 게임 클리어 음악 재생 함수
    public void PlayClearBGM()
    {
        if (clearBGM != null)
        {
            audioSource.Stop(); // 배경음 정지
            audioSource.loop = true;
            audioSource.clip = clearBGM;
            audioSource.Play();
        }
    }
}