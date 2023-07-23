using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //사운드매니저 싱글톤 인스턴스 변수 선언
    private static SoundManager instance;

    //사운드매니저 싱글톤 인스턴스 프로퍼티 선언
    public static SoundManager Instance
    {
        get
        {
            //인스턴스에 아무것도 저장되어있지 않을 경우
            if(instance == null)
            {
                //씬에서 사운드매니저 컴포넌트를 가진 오브젝트가 있는지 검사
                var obj = FindObjectOfType<SoundManager>();
                //씬에 사운드매니저 컴포넌트 오브젝트가 있으면 인스턴스 변수에 저장
                if(obj != null) 
                {
                    instance = obj;
                }
                //씬에 없다면 사운드매니저 컴포넌트가 있는 오브젝트 생성하고 인스턴스 변수에 저장
                else
                {
                    var newobj = new GameObject().AddComponent<SoundManager>();
                    instance = newobj;
                }
            }
            //인스턴스 반환
            return instance;
        }
    }

    //오디오사운드 변수 선언
    private AudioSource bgSound;
    //오디오클립 리스트 선언
    public AudioClip[] bgList;

    //볼륨크기 상수 변수 선언
    private float bgVolume = 0.1f;

    private void Awake()
    {
        //씬에 사운드매니저 컴포넌트 중복 여부 검사
        var objs = FindObjectsOfType<SoundManager>();
        //씬에 사운드 매니저 컴포넌트가 중복인 경우 지금  생성한 객체 삭제
        if(objs.Length != 1) 
        {
            Destroy(gameObject);
            return;
        }
        //게임 실행시 항상 존재하도록 함수 호출
        DontDestroyOnLoad(gameObject);

        //오디오 사운드 컴포넌트 연결
        bgSound = GetComponent<AudioSource>();

        //배경음악 실행 함수 호출
        PlayingBackgroundSound(bgList[0]);
    }

    //배경음악 실행 함수
    public void PlayingBackgroundSound(AudioClip clip)
    {
        //배경음악 클립 변수 선언
        bgSound.clip = clip;
        //배경음악 무한반복
        bgSound.loop = true;
        //볼륨크기를 배경음악 볼륨에 저장
        bgSound.volume = bgVolume;
        //배경음악 재생
        bgSound.Play();
    }

    //배경음악 볼륨 조절 함수
    public void ChangeVolumeBG(float value)
    {
        //볼륨조절 슬라이드의 볼륨상수를 볼륨크기 변수에 저장
        bgVolume = value;
        //볼륨크기를 배경음악 불륨에 저장
        bgSound.volume = bgVolume;
    }
}
