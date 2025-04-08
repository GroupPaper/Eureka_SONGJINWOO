using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ� (�ٸ� ��ũ��Ʈ���� ���� ���� �����ϰ� ��)
    public static GameManager Instance;

    // ���� ���õ� ī�� �� ��
    public Card firstCard;
    public Card secondCard;

    // Ÿ�̸� ���� UI
    public Text timeTxt;
    float time = 60.0f; // �� �ð� (60��)

    // ���� ���� �г� �� ����� ��ư
    public GameObject gameOverPanel;
    public Button restartButton;

    // ����� ������ ����ƴ��� Ȯ���ϴ� �÷���
    private bool isUrgentMusicPlayed = false;

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // ���� ���� �г� ��Ȱ��ȭ
        gameOverPanel.SetActive(false);

        // ����� ��ư�� Ŭ�� �̺�Ʈ ����
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        // �ð��� �����ִ� ���ȸ� Ÿ�̸� �۵�
        if (time > 0)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2"); // �Ҽ��� 2�ڸ����� ǥ��

            // �ð��� 20�� ���Ϸ� �پ��� ����� ���� ��� & Ÿ�̸� ���� ����
            if (time <= 20f && !isUrgentMusicPlayed)
            {
                AudioManager.Instance.PlayUrgentBGM();
                timeTxt.color = Color.red;
                isUrgentMusicPlayed = true;
            }

            // �ð��� 0 ���Ϸ� �������� ���� ���� ó��
            if (time <= 0)
            {
                time = 0;
                GameOver();
            }
        }
    }

    // ���� ���� ó�� �Լ�
    void GameOver()
    {
        AudioManager.Instance.PlayGameOverSound(); // ���� ���� ���� ���
        gameOverPanel.SetActive(true); // ���� ���� UI ǥ��
    }

    // ����� ��ư Ŭ�� �� ����
    public void RestartGame()
    {
        AudioManager.Instance.PlayNormalBGM(); // �⺻ BGM ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� �� �ٽ� �ε�
    }

    public int totalPairs = 10; // ��ü ī�� ¦ �� (10��)
    private int matchedPairs = 0; // ���� ���� ¦ ��

    // ī�� �� �� �� �� ó��
    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            // ���� ī���� ��� �����ϰ� ¦ ī��Ʈ ����
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            matchedPairs++;

            // ��� ¦�� �������� Ŭ���� ������ ��ȯ
            if (matchedPairs >= totalPairs)
            {
                Invoke("LoadEndScene", 0.1f); // ����� ������ �� ��ȯ
            }
        }
        else
        {
            // �ٸ� ī���� ��� �ٽ� ������
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        // ī�� ���� �ʱ�ȭ
        firstCard = null;
        secondCard = null;
    }

    // ���� Ŭ���� �� EndScene �ε�
    void LoadEndScene()
    {
        AudioManager.Instance.PlayClearBGM(); // Ŭ���� BGM ���
        SceneManager.LoadScene("EndScene"); // Ŭ���� ������ �̵�
    }
}