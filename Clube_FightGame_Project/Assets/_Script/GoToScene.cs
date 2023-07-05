using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToScene : MonoBehaviour
{
    //버튼 오브젝트 변수
    private Button singlebutton;

    // 씬 시작시 버튼 컴포넌트 설정하기
    void Start()
    {
        singlebutton = GetComponent<Button>();
        singlebutton.onClick.AddListener(GoToSinglePlay);
    }

    //싱글플레이 버튼을 눌렀을 때, 이동할 씬을 파라미터로 해서 로드씬 함수 부르기
    void GoToSinglePlay()
    {
        LoadingSceneSystem.LoadScene("TestScene");
    }
}
