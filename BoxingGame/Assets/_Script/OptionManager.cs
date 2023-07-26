using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //옵션 버튼 오브젝트 변수
    private Button option;
    //옵션창 오브젝트 변수
    private Transform optionWindow;
    //옵션창 나가기 버튼 오브젝트 변수
    private Button quitOption;
    //볼륨 슬라이더 오브젝트 변수
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Awake()
    {
        //옵션 버튼 컴포넌트 찾기
        option = GetComponent<Button>();
        //옵션 버튼을 통해 옵션창 오브젝트 찾기
        optionWindow = transform.Find("Option Window");

        //옵션창을 통해 옵션 나가기 버튼 컴포넌트 찾기
        quitOption = optionWindow.transform.Find("Quit Option").GetComponent<Button>();
        //옵션창을 통해 볼륨 슬라이더 컴포넌트 찾기
        volumeSlider = optionWindow.transform.Find("Volume Slider").GetComponent<Slider>();

        //옵션창 비활성화
        optionWindow.gameObject.SetActive(false);

        //옵션 버튼에 옵션창 여는 함수 연결
        option.onClick.AddListener(OpenTheOption);
        //옵션 나가기 버튼에 옵션창 닫는 함수 연결
        quitOption.onClick.AddListener(CloseTheOption);
        volumeSlider.onValueChanged.AddListener(SoundManager.Instance.ChangeVolumeBG);
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
