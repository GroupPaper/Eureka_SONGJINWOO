using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{

    private static ExitManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR // �̰��� ���� �س��� �����Ϳ��� �������� Ȱ��ȭ �ǰ�
        UnityEditor.EditorApplication.isPlaying = false; // ����� �ش� ������ ��Ȱ��ȭ �ǰ� �ϴܿ� ������ Ȱ��ȭ�Ǿ� ������ �߻����� ����
#else
        Application.Quit();
#endif
    }

}
