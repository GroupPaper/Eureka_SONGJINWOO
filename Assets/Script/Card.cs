using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class Card : MonoBehaviour
{
    public int idx = 0;  // 카드의 고유 번호 (짝 비교에 사용)

    public GameObject front; // 카드의 앞면 오브젝트
    public GameObject back;  // 카드의 뒷면 오브젝트
    public Animator anim;    // 카드 애니메이션 컨트롤러

    public SpriteRenderer frontImage; // 카드 앞면에 표시될 이미지 스프라이트

    // 사운드 클립들
    public AudioClip flipSound;     // 카드가 닫힐 때 나는 사운드
    public AudioClip destroySound;  // 카드가 제거될 때 나는 사운드
    public AudioClip clickSound;    // 카드가 클릭될 때 나는 사운드

    private AudioSource audioSource; // 사운드 재생용 AudioSource

    private Vector3 originalScale;

    void Awake()
    {
        // AudioSource가 없다면 자동으로 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // 시작 시 자동 재생 금지
        }

        originalScale = transform.localScale;
    }

    // 카드가 클릭되어 열릴 때 호출
    public void OpenCard()
    {
        // 클릭 사운드 재생
        if (clickSound != null && audioSource != null)
            audioSource.PlayOneShot(clickSound);

        // 애니메이션 및 앞/뒷면 전환
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        // GameManager에 열려 있는 카드 정보 등록
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched(); // 두 카드 비교 처리
        }
    }

    // 카드가 짝이 맞아 제거될 때 호출
    public void DestroyCard()
    {
        // 제거 사운드 재생
        if (destroySound != null && audioSource != null)
            audioSource.PlayOneShot(destroySound);

        // 0.5초 후 오브젝트 파괴
        Invoke("DestroyCardInvoke", 0.5f);
    }

    // 실제 오브젝트 제거
    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    // 카드가 닫힐 때 호출 (짝이 맞지 않을 경우)
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f); // 약간의 지연 후 닫기
    }

    // 카드 닫기 처리
    public void CloseCardInvoke()
    {
        // 닫기 사운드 재생
        if (flipSound != null && audioSource != null)
            audioSource.PlayOneShot(flipSound);

        // 애니메이션 및 앞/뒷면 전환
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);

        // 여기에서 크기를 원래대로
        transform.localScale = originalScale;
    }

    // 카드 번호 및 앞면 이미지 설정
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Image{idx}");
        // 예: Resources/ 폴더에 Image1, Image2, ... 이런 식으로 이미지가 있어야 함
    }
}


