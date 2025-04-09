using System.Collections;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform cards;          // 카드들을 넣을 부모 오브젝트
    public GameObject card;          // 카드 프리팹

    private const int totalCards = 20;
    private const int columns = 4;



    void Start()
    {
        StartCoroutine(SpawnCards());
    }

    IEnumerator SpawnCards()
    {
        // 카드 인덱스를 생성하고 셔플
        int[] arr = Enumerable.Range(0, totalCards / 2).SelectMany(i => new[] { i, i }).OrderBy(_ => Random.value).ToArray();


        for (int i = 0; i < totalCards; i++)
        {
            // 카드 생성
            GameObject go = Instantiate(card, cards); // 부모는 cards

            // 타겟 위치 계산
            float x = (i / columns) * 2.5f - 5.0f;
            float y = (i % columns) * 2.0f - 3.5f;
            Vector2 targetPos = new Vector2(x, y);

            // 시작 위치: 화면 위쪽 (y + 10)
            Vector2 startPos = new Vector2(x, y + 10f);
            go.transform.position = startPos;

            // 크기 초기화
            go.transform.localScale = Vector3.zero;

            // 카드 이미지 세팅
            go.GetComponent<Card>().Setting(arr[i]);

            // 등장 애니메이션 실행
            StartCoroutine(AnimateCardDrop(go.transform, targetPos));

            // 다음 카드 생성까지 대기
            yield return new WaitForSeconds(0.05f);

        }

        GameManager.Instance.isReady = true;
        GameManager.Instance.CheckAndPlayBackgroundMusic(); // 시간 상태에 따라 BGM 재생

    }

    // 카드가 위에서 떨어지며 스케일이 커지는 애니메이션
    IEnumerator AnimateCardDrop(Transform card, Vector2 targetPos)
    {
        float duration = 0.3f;
        float elapsed = 0f;

        Vector2 startPos = card.position;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            card.position = Vector2.Lerp(startPos, targetPos, t);
            card.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        card.position = targetPos;
        card.localScale = endScale;
    }
}
