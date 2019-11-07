using System;
using System.Collections.Generic;

enum MaxMental
{
    Max = 4,
}

enum KindBurgers            // 버거종류   // ** 레시피 추가하시려면 이곳을 이용하세요 **레시피**
{
    CheeseBurger = 1,
    BulgogiBurger = 2,
    BigMac = 3,
    VegeBurger = 4,
}

enum CustomorMaxLife       // 고객의 최대 인내심 (매직넘버 방지용)
{
    Maxpatience = 200,
}

enum SceneValue            // 씬의 종류
{
    Start,
    InGame,
    EndMessage,
    End,
}

enum Cursordefualt         // 커서의 초기값 (매직넘버 방지용 / 초기화용)
{
    CousorX = 3,
    CousorY = 23,
}


enum Food                 // 음식재료 '식재료'를 영어로 쓰려했으나 길고 의미전달이 어려움
{
    Bread = 1,
    Cheese = 2,
    Patty = 3,
    Vege = 4,
}

enum eDir                 // 방향키
{
    Left,
    Right,
    Space,
}


enum MaxBurger            // 최대 버거의 패티갯수  (최대 버거패티 추가하려면 여기에 추가) // 작명이 애매한데 푸드보단 버거가 나은듯
{
    MaxburgerNum = 5,
}


class MyQueue<T>
{
    //-------------------------------------------------------------
    // 유한한 큐 만드는 셀프 큐 대충 이해하고 가져옴 / 정상작동
    // 기존큐 기능쓰려면 레퍼런스.oQueue.원래큐기능
    // 기존큐 + 큐의 인덱스가 유한함 이라고보면 됨
    // 사실상 큐인관계로 무한이 돌리면 저장소 계속 잡아먹음 쉬프트의 개념 / but 콘솔이라 괜찮아
    //-------------------------------------------------------------

    Queue<T> mQueue = new Queue<T>();

    public Queue<T> oQueue
    {
        get { return mQueue; }   
        set { mQueue = value; }
    }

    int iFixedCount = 5;
    T iLastValue;

    public MyQueue(int _count)
    {
        iFixedCount = _count;
    }

    public Int32 Count
    {
        get { return mQueue.Count; }
    }

    public void Enqueue(T oItem)
    {
        iLastValue = oItem;
        if (mQueue.Count >= iFixedCount) { return; }     //커스터 마이징 함. 최대값 이상의 값 못넣음.
        else
            mQueue.Enqueue(oItem);
    }

    public void PrintQ()
    {
        //-------------------------------------------
        // 테스트용 인데 그냥 공부하기 좋을거같아 남겨놓음
        //-------------------------------------------
        T[] arr = mQueue.ToArray();
        foreach (var item in arr)
        {
            Console.WriteLine(item);
        }
    }

    public int Langth
    {
        get { return iFixedCount; }
    }

    public T LastValue
    {
        get { return iLastValue; }
    }
}

class Define
{
    public float GetTargetFPSTicks()
    {
        // 1초 60번
        // 1초 1000 밀리 세컨드
        // 1밀리세컨드 10000틱
        // 이건 굉장히 선생님 코드 ㅇㅈ 존내 짱임
        const float nTarget = 30;
        return 10000 * 1000 / nTarget;
    }
}

