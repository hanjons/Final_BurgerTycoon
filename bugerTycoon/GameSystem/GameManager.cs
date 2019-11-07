using System;
using System.Threading;
using System.Collections.Generic;
using System.Media;
using System.Diagnostics;

//---------------------------------------------------------------------------------------
// 기능추가 절차
// 
// 키추가 -> GameManager클래스 내 input() 함수 이용하시면됩니다.
// 버거레시피 추가 ->  Burger 클래스, enum Kindburger, 버거 만드실때 vege버거 사이에 넣으면 됩니다.
//                   나머진 그사이에 있는값들 알아서 랜덤 돌려서 출력해주니 그런줄 알면됨.
// 
// 키추가 주석 검색 키워드 ( **키 )
// 식재료추가 주석 검색 키워드 (**식재료 )
// 레시피추가 주석 검색 키워드 ( **레시피 )
//----------------------------------------------------------------------------------------


class GameManager
{
    Thread     EffectsThread;         // 이펙트 사운드 Thread
    Burger     QBurger;               // 플레이어가 채워나갈 버거
    Rander     gameRander;            // 모든 랜더 처리
    Customor   customor;              // 고객, 주문내용, 인내심 을 가지고 있음
    Define     define;                // 그냥 define
    PlayerStat player;                // 플레이어 라이프, 돈

    public GameManager()
    {
        //-----------------------
        // 각 클래스 인스턴스화                                          // 싱글턴 패턴이 좋을거같은데 어설프게 썻다간 광역딜 들어올거같아서 놔둠
        //-----------------------
        QBurger       = new Burger();
        player        = new PlayerStat();
        customor      = new Customor();
        define        = new Define();
        gameRander    = new Rander(QBurger, customor, player);
        
    }

    public SceneValue Run(SceneValue a_scene)
    {
        //--------------------------------------
        // 메인 루프 부분
        // 1. 사용자가 키입력을 안해도 업데이트
        // 2. 틱으로 고객의 인내심이 줄어들게함
        // define.GetTargetFPSTicks()  ->> 수정시 pc마다 깜빡임줄어듬
        //--------------------------------------- 
        Debug.Assert(a_scene == SceneValue.InGame);                                  // 예외처리

        DateTime old           = DateTime.Now;
        long  ticks            = 0;
        long  phaseTicks       = 0;													 // 다들아시는 틱.		
        long  TargetTicks      = (long)define.GetTargetFPSTicks();
        float targetTicksphase = 0.5f;                                               // 페이즈 조절용 틱초기값
        player.Money           = 0;                                                  // 재게임시 초기화
        customor.RandomOrder();                                                      // 처음 오더 세팅 지우면 게임 안돌아감
        
        while (true)
        {
            
            //---------------------------------------------
            // 메인 루프
            // 버거 판별 , 게임 오버룰도 담당
            // 하단은 다들 아시는 그 틱
            //---------------------------------------------

            long temp = (DateTime.Now - old).Ticks;                              // 여기도 틱
            old = DateTime.Now;
            phaseTicks += temp;                                                  // 페이즈틱
            ticks += temp;

            
            if (ticks > TargetTicks)                                              // 30프레임당 1번 랜더
            {
                Input();
                gameRander.FixRender();                                           // 메인랜더링 
                ticks -= TargetTicks;
            }

            //-----------------------------
            // 페이즈 구현 부분 
            // 페이즈 2 = 400원 이상
            // 페이즈 3 = 600원 이상
            // 페이즈 4 = 900원 이상
            //-----------------------------
            if (phaseTicks > 1000 * 10000 * targetTicksphase)                     // 페이즈 업데이트 부분
            {
                CheckPatience();
                gameRander.TickRander();                                          // 페이즈 틱당 랜더링
                phaseTicks = 0;
            } 

            if (player.Money > 300)                                               // 매직넘버 바꿀까 했는데 가독성이 이게 좋다는 평이 많아 놔둠
            {
                targetTicksphase = 0.2f;
            }

            if (player.Money > 500)
            {
                targetTicksphase = 0.1f;
            }

            if (player.Money > 800)
            {
                targetTicksphase = 0.05f;
            }

            if (player.Mental <= 0)                                               // 게임오버 해주는 부분
            {
                a_scene = SceneValue.EndMessage;
                player.Mental = 4;
                return a_scene;
            }

        }
    }

    public bool Input()
    {
        EffectsThread = new Thread(SoundEffects);                      //누르면 나오는 사운드.

        if (false == Console.KeyAvailable)                             // 예외처리
        {
            return false;
        }

        //-------------------------------
        // 키값에 따른 랜더 업데이트  **키
        //-------------------------------
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.LeftArrow:
                gameRander.Update(eDir.Left);
                break;
            case ConsoleKey.RightArrow:
                gameRander.Update(eDir.Right);
                break;
            case ConsoleKey.Spacebar:
                if (update())                                         // 버거 만땅일때 랜더를 한번 스킵해야 다음 버거가 미리찍히는일 없음 // 방지 if문
                {
                    gameRander.Update(eDir.Space);
                }
                EffectsThread.Start();                                 // 스페이스바에서만 이펙트 사운드
                break;
            default:
                break;
        }
        return true;
    }

   

    public bool CheckBurger()
    {
        //---------------------------------------------
        // 플레이어 버거와 주문 버거 비교 하는 부분
        //---------------------------------------------
        Food[] temp1 = QBurger.playerBurger.oQueue.ToArray();
        Food[] temp2 = customor.orderBurger.playerBurger.oQueue.ToArray();

        for (int i = 0; i < temp1.Length; i++)
        {
            if (temp1[i] != temp2[i])
            {
                return false;
            }
        }
        return true;
    }

    public void CheckPatience()
    {
        //---------------------------------------------------
        //  고객인내심 0일때 기본값으로 세팅함수
        //  0이 아니면 10씩 줄어듬 //  페이즈 틱당
        //---------------------------------------------------
        if (customor.patience <= 0)
        {
            customor.patience = (int)CustomorMaxLife.Maxpatience;
            player.Mental--;
            customor.OrderClear();
            customor.RandomOrder();
            QBurger.BurgerClear();

        }
        else { customor.patience -= 10; }
    }

    public bool update()
    {
        //---------------------------------------------
        // 업데이트 버거판별
        // 같으면 머니 100원 상승
        // 다르면 멘탈 1 차감
        // 버거 판별후 고객 인내심 리셋
        //---------------------------------------------
        if (QBurger.playerBurger.oQueue.Count == (int)MaxBurger.MaxburgerNum)
        {
            if (CheckBurger())
            {
                player.Money += 100;
                QBurger.BurgerClear();
                customor.OrderClear();
                customor.RandomOrder();
                customor.patience = (int)CustomorMaxLife.Maxpatience;
            }

            else
            {
                player.Mental -= 1;
                QBurger.BurgerClear();
                customor.OrderClear();
                customor.RandomOrder();
                customor.patience = (int)CustomorMaxLife.Maxpatience;
            }
            return false;
        }
        return true;

    }

    private static void SoundEffects()
    {
        //사운드 가져다 쓰는곳. // 예외처리 못함. // 사운드 플레이어 처음써봄
        var sound = new SoundPlayer { SoundLocation = "Resources/drop.wav" };
        sound.Play();
    }

}
