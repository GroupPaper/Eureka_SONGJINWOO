using System.Collections;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform cards;          // ī����� ���� �θ� ������Ʈ
    public GameObject card;          // ī�� ������

    private const int totalCards = 20;
    private const int columns = 4;



    void Start()
    {
        StartCoroutine(SpawnCards());
    }

    IEnumerator SpawnCards()
    {
        // ī�� �ε����� �����ϰ� ����
        int[] arr = Enumerable.Range(0, totalCards / 2).SelectMany(i => new[] { i, i }).OrderBy(_ => Random.value).ToArray();


        for (int i = 0; i < totalCards; i++)
        {
            // ī�� ����
            GameObject go = Instantiate(card, cards); // �θ�� cards

            // Ÿ�� ��ġ ���
            float x = (i / columns) * 2.5f - 5.0f;
            float y = (i % columns) * 2.0f - 3.5f;
            Vector2 targetPos = new Vector2(x, y);

            // ���� ��ġ: ȭ�� ���� (y + 10)
            Vector2 startPos = new Vector2(x, y + 10f);
            go.transform.position = startPos;

            // ũ�� �ʱ�ȭ
            go.transform.localScale = Vector3.zero;

            // ī�� �̹��� ����
            go.GetComponent<Card>().Setting(arr[i]);

            // ���� �ִϸ��̼� ����
            StartCoroutine(AnimateCardDrop(go.transform, targetPos));

            // ���� ī�� �������� ���
            yield return new WaitForSeconds(0.05f);

        }

        GameManager.Instance.isReady = true;
        GameManager.Instance.CheckAndPlayBackgroundMusic(); // �ð� ���¿� ���� BGM ���

    }

    // ī�尡 ������ �������� �������� Ŀ���� �ִϸ��̼�
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
