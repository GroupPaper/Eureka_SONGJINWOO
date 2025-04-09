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
#if UNITY_EDITOR // 이것을 선언 해놔야 에디터에서 종료기능이 활성화 되고
        UnityEditor.EditorApplication.isPlaying = false; // 빌드시 해당 문법이 비활성화 되고 하단에 문법이 활성화되어 에러가 발생하지 않음
#else
        Application.Quit();
#endif
    }

}
