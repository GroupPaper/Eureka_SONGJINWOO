using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    // 카드들을 넣을 부모 오브젝트 (Hierarchy 정리를 위해 사용됨)
    public Transform cards;

    // 생성할 카드 프리팹 (Inspector에서 연결)
    public GameObject card;

    void Start()
    {
        // 10쌍의 카드 인덱스 배열 생성 (총 20장)
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };

        // 배열을 무작위로 섞음 (Shuffle)
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

        // 20장의 카드를 생성하고 배치
        for (int i = 0; i < 20; i++)
        {
            // 카드 프리팹을 생성 (부모는 현재 Board 오브젝트)
            GameObject go = Instantiate(card, transform);

            // 카드의 위치 계산
            // x는 줄 위치 (5줄), y는 열 위치 (4열)
            float x = (i / 4) * 2.5f - 5.0f; // 가로 간격 조정
            float y = (i % 4) * 2.0f - 3.5f; // 세로 간격 조정

            // 카드의 위치 설정
            go.transform.position = new Vector2(x, y);

            // 카드에 이미지 번호(idx)를 설정
            go.GetComponent<Card>().Setting(arr[i]);
        }
    }
}
