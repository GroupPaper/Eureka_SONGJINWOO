using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �������� �����ϴ� AudioManager Ŭ����
public class AudioManager : MonoBehaviour
{
    // �̱��� ����: ��𼭵� AudioManager.Instance�� ���� ����
    public static AudioManager Instance;

    // ����� �� ��Ȳ�� ȿ���� ����� Ŭ��
    public AudioClip normalBGM;     // �Ϲ� ��� ����
    public AudioClip urgentBGM;     // ����� �� ���� (ex. �ð� ����)
    public AudioClip gameOverSound; // ���� ���� �� ȿ����
    public AudioClip clearBGM;      // Ŭ���� �� ����

    private AudioSource audioSource;    // ������� ����ϴ� ������Ʈ
    private bool isUrgentPlaying = false; // ��� ������ �� ���� ����ǵ��� üũ

    // Awake�� ���� �ε�� �� ���� ���� �����
    private void Awake()
    {
        // �̱��� ����: �ν��Ͻ��� ������ �ڽ��� �ν��Ͻ��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� AudioManager ����
            return;
        }
    }

    // ���� ���� �� ȣ���
    private void Start()
    {
        // AudioSource ������Ʈ�� �������� �߰� �� ����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;         // ������� �ݺ� ���
        audioSource.playOnAwake = false; // �ڵ� ��� X
        audioSource.volume = 0.5f;       // �⺻ ���� ����

        PlayNormalBGM(); // ó������ �Ϲ� ����� ���
    }

    // �Ϲ� ����� ��� �Լ�
    public void PlayNormalBGM()
    {
        audioSource.loop = true;
        audioSource.clip = normalBGM;
        audioSource.Play();
    }

    // ����� ��Ȳ�� ����� ��� �Լ� (�� ���� ����ǵ��� üũ)
    public void PlayUrgentBGM()
    {
        if (urgentBGM != null && !isUrgentPlaying)
        {
            audioSource.Stop(); // ���� ���� ����
            audioSource.loop = true;
            audioSource.clip = urgentBGM;
            audioSource.Play();
            isUrgentPlaying = true;
        }
    }

    // ���� ���� ���� ��� �Լ�
    public void PlayGameOverSound()
    {
        if (gameOverSound != null)
        {
            audioSource.Stop(); // ����� ����
            audioSource.loop = false; // ȿ������ �ݺ� X
            audioSource.clip = gameOverSound;
            audioSource.Play();
        }
    }

    // ���� Ŭ���� ���� ��� �Լ�
    public void PlayClearBGM()
    {
        if (clearBGM != null)
        {
            audioSource.Stop(); // ����� ����
            audioSource.loop = true;
            audioSource.clip = clearBGM;
            audioSource.Play();
        }
    }
}