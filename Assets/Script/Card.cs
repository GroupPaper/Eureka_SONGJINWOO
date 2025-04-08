using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class Card : MonoBehaviour
{
    public int idx = 0;  // ī���� ���� ��ȣ (¦ �񱳿� ���)

    public GameObject front; // ī���� �ո� ������Ʈ
    public GameObject back;  // ī���� �޸� ������Ʈ
    public Animator anim;    // ī�� �ִϸ��̼� ��Ʈ�ѷ�

    public SpriteRenderer frontImage; // ī�� �ո鿡 ǥ�õ� �̹��� ��������Ʈ

    // ���� Ŭ����
    public AudioClip flipSound;     // ī�尡 ���� �� ���� ����
    public AudioClip destroySound;  // ī�尡 ���ŵ� �� ���� ����
    public AudioClip clickSound;    // ī�尡 Ŭ���� �� ���� ����

    private AudioSource audioSource; // ���� ����� AudioSource

    private Vector3 originalScale;

    void Awake()
    {
        // AudioSource�� ���ٸ� �ڵ����� �߰�
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // ���� �� �ڵ� ��� ����
        }

        originalScale = transform.localScale;
    }

    // ī�尡 Ŭ���Ǿ� ���� �� ȣ��
    public void OpenCard()
    {
        // Ŭ�� ���� ���
        if (clickSound != null && audioSource != null)
            audioSource.PlayOneShot(clickSound);

        // �ִϸ��̼� �� ��/�޸� ��ȯ
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        // GameManager�� ���� �ִ� ī�� ���� ���
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched(); // �� ī�� �� ó��
        }
    }

    // ī�尡 ¦�� �¾� ���ŵ� �� ȣ��
    public void DestroyCard()
    {
        // ���� ���� ���
        if (destroySound != null && audioSource != null)
            audioSource.PlayOneShot(destroySound);

        // 0.5�� �� ������Ʈ �ı�
        Invoke("DestroyCardInvoke", 0.5f);
    }

    // ���� ������Ʈ ����
    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    // ī�尡 ���� �� ȣ�� (¦�� ���� ���� ���)
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f); // �ణ�� ���� �� �ݱ�
    }

    // ī�� �ݱ� ó��
    public void CloseCardInvoke()
    {
        // �ݱ� ���� ���
        if (flipSound != null && audioSource != null)
            audioSource.PlayOneShot(flipSound);

        // �ִϸ��̼� �� ��/�޸� ��ȯ
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);

        // ���⿡�� ũ�⸦ �������
        transform.localScale = originalScale;
    }

    // ī�� ��ȣ �� �ո� �̹��� ����
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Image{idx}");
        // ��: Resources/ ������ Image1, Image2, ... �̷� ������ �̹����� �־�� ��
    }
}


