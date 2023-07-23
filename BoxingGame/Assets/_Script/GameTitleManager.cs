using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTitleManager : MonoBehaviour
{
    //싱글플레이 버튼 오브젝트 변수
    private Button singlebutton;
    //옵션 버튼 오브젝트 변수
    private Button option;
    //옵션창 오브젝트 변수
    private Transform optionWindow;
    //옵션창 나가기 버튼 오브젝트 변수
    private Button quitOption;
    //볼륨 슬라이더 오브젝트 변수
    public Slider volumeSlider;


    // 씬 시작시 버튼 컴포넌트 설정하기
    public void Awake()
    {
        //싱글플레이 버튼 컴포넌트 찾기
        singlebutton = transform.Find("SinglePlay").GetComponent<Button>();
        //옵션 버튼 컴포넌트 찾기
        option = transform.Find("Option").GetComponent<Button>();

        //옵션 버튼을 통해 옵션창 오브젝트 찾기
        optionWindow = option.transform.Find("Option Window");
        //옵션창을 통해 옵션 나가기 버튼 컴포넌트 찾기
        quitOption = optionWindow.transform.Find("Quit Option").GetComponent<Button>();
        //옵션창을 통해 볼륨 슬라이더 컴포넌트 찾기
        volumeSlider = optionWindow.transform.Find("Volume Slider").GetComponent<Slider>();
        Debug.Log(volumeSlider);

        //옵션창 비활성화
        optionWindow.gameObject.SetActive(false);

        //싱글플레이 버튼에 로딩 화면 함수 연결
        singlebutton.onClick.AddListener(GoToSinglePlay);
        //옵션 버튼에 옵션창 여는 함수 연결
        option.onClick.AddListener(OpenTheOption);
        //옵션 나가기 버튼에 옵션창 닫는 함수 연결
        quitOption.onClick.AddListener(CloseTheOption);
    }

    //싱글플레이 버튼을 눌렀을 때, 이동할 씬을 파라미터로 해서 로드씬 함수 부르기
    void GoToSinglePlay()
    {
        LoadingSceneManager.LoadScene("TestScene");
    }

    //옵션 버튼 눌렀을 때, 옵션창 활성화
    void OpenTheOption()
    {
        optionWindow.gameObject.SetActive(true);
    }

    //옵션 나가기 버튼 눌렀을 때, 옵션창 비활성화 
    void CloseTheOption() 
    {
        optionWindow.gameObject.SetActive(false);
    }
}
