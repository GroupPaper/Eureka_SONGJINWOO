using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    // ī����� ���� �θ� ������Ʈ (Hierarchy ������ ���� ����)
    public Transform cards;

    // ������ ī�� ������ (Inspector���� ����)
    public GameObject card;

    void Start()
    {
        // 10���� ī�� �ε��� �迭 ���� (�� 20��)
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };

        // �迭�� �������� ���� (Shuffle)
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

        // 20���� ī�带 �����ϰ� ��ġ
        for (int i = 0; i < 20; i++)
        {
            // ī�� �������� ���� (�θ�� ���� Board ������Ʈ)
            GameObject go = Instantiate(card, transform);

            // ī���� ��ġ ���
            // x�� �� ��ġ (5��), y�� �� ��ġ (4��)
            float x = (i / 4) * 2.5f - 5.0f; // ���� ���� ����
            float y = (i % 4) * 2.0f - 3.5f; // ���� ���� ����

            // ī���� ��ġ ����
            go.transform.position = new Vector2(x, y);

            // ī�忡 �̹��� ��ȣ(idx)�� ����
            go.GetComponent<Card>().Setting(arr[i]);
        }
    }
}
