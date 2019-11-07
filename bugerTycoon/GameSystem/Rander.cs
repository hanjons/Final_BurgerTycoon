using System;


class Rander
{
    //------------------------------
    // 플레이어 커서 X, Y
    // 전역으로 생성
    //------------------------------
    static public int _cursorX = 3;
    static public int _cursorY = 10;

    Burger     Qburger;
    Customor   customor;               // 작명규칙지키려다 이것만 소문자
    PlayerStat player;


    public Rander(Burger a_Qburger, Customor a_customor, PlayerStat a_player)
    {
        //------------------------------
        // 캐싱부분  GameManager -> Rander
        //------------------------------
        Qburger  = a_Qburger;
        customor = a_customor;
        player   = a_player;
    }


    public void FixRender()
    {
        //-----------------------------------------
        // 메인 랜더  //깜빡거림때문에 덮어씌우게 변경
        //-----------------------------------------
        Console.CursorVisible = false;  // 커서 표시 안되게
        RenderStat();                   // 플레이어 상태 (돈, 멘탈) 랜더
        CustomorRender();               // 고객 랜더
        FoodUIRender();                 // 패티고르는 ui랜더
        RecipeRender();                 // 주문 버거 레시피 랜더
        RenderBurgerTile();             // 플레이어 버거 생성테두리 랜더
        RenderBuger();                  // 플레이어가 만드는 버거 랜더
        RenderCursor();                 // 커서 랜더   

    }

    public void TickRender()
    {
        //------------------------------
        // 페이즈틱당 출력되는 부분
        //------------------------------
        Console.CursorVisible = false;  
        CustomorPatience();				
    }


    public void RecipeRender()
    {
        //------------------------------
        // ** 식재료 추가하시려면 이곳을 이용하세요 **식재료**
        // 주문 레시피 랜더
        // 1. 고객의 버거 주문 재료내용(Queue) -> arr로 받아옴
        // 2. 그냥 좌표에 공백의 bar를 찍고 찍을때마다 색깔 바꿔줌
        //------------------------------
        const int recipeX = 70;
        const int recipeY = 10;

        Food[] arr = customor.orderBurger.playerBurger.oQueue.ToArray();

        Console.SetCursorPosition(recipeX, recipeY);
        Console.Write("레시피를 보고 만드세요");
        Console.SetCursorPosition(recipeX + 5, recipeY + 1);
        Console.Write(customor.orderStirng+"        ");                                    //화면 깜박임 문제때문에 되도록 랜더가 덮어쓰기위해 공백 추가 // 각각 스트링 길이 틀림

        for (int i = 0; i < arr.Length; i++)
        {
            Console.SetCursorPosition(recipeX + 3, recipeY + i + 3);

            switch (arr[i])
            {
                case Food.Bread:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write("             ");
                    break;
                case Food.Cheese:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("             ");
                    break;
                case Food.Patty:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("             ");
                    break;
                case Food.Vege:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("             ");
                    break;
                default:
                    break;
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    public void FoodUIRender()
    {
        //----------------------------------
        // 커서가 움직이는 UI 임.
        // 플레이어가 버거에 채워넣을 식재료 고르는 부분
        // ** 추가 식재료 추가히시려면 여기도 추가하세요 **식재료**
        //----------------------------------
        const int breadX = 6;
        const int breadY = 12;


        Console.SetCursorPosition(breadX, breadY);
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("빠앙");
        Console.SetCursorPosition(breadX + 10, breadY);
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("패티");
        Console.SetCursorPosition(breadX + 20, breadY);
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("야채");
        Console.SetCursorPosition(breadX + 30, breadY);
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("치즈");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.SetCursorPosition((int)Cursordefualt.CousorX, (int)Cursordefualt.CousorY);
    }


    public void CustomorRender()
    {
        //----------------------------------
        // 고객 그림, 주문내용 랜더
        //----------------------------------
        const int X = 12;
        const int Y = 2;
        const int OrderX = X + 20;                     //주문 표시좌표                                   
        string[] Cus = new string[5];
        Cus[0] = "       ┌─┼┼┼─┐      ";
        Cus[1] = "       │ @ @ │      ";
        Cus[2] = "     ┌──┘───└──┐    ";
        Cus[3] = "     │         │    ";
        Cus[4] = "     │         │    ";

        for (int i = 0; i < Cus.Length; i++)           //이래야 그림 그리기 쉬워서 만듬
        {
            Console.SetCursorPosition(X, Y + i);
            Console.WriteLine(Cus[i]);
        }

        Console.SetCursorPosition(OrderX, Y);
        Console.Write(customor.orderStirng+"        ");   //화면 깜박임 문제때문에 되도록 랜더가 덮어쓰기위해 공백 추가 // 각각 스트링 길이 틀림
        Console.SetCursorPosition(OrderX, Y + 1);
        Console.Write("주문한다 내놔!! 빨리");

    }


    public void CustomorPatience()
    {
        //----------------------------------
        // 고객 인내심 표시
        //----------------------------------
        const int X = 3;
        const int Y = 22;

        Console.SetCursorPosition(X, Y);
        Console.Write(_cursorX + "," + _cursorY);
        Console.WriteLine();
        Console.Write("  손님 분노의 게이지 : ");
        Console.Write("■■■■■■■■■■■■■■■■■■■■");
        Console.SetCursorPosition(X+20, Y+1);
        for (int i = 0; i < customor.patience; i = i + 10)          // 네모한개 = 고객 인내심 10
        {
            Console.Write("  ");
        }


    }


    public void RenderCursor()
    {
        //---------------------
        // 커서 랜더
        // 커서 이동 바운더리 설정
        // 3~ 33까지만 움직임 양쪽끝 이동시 처음과 끝으로 이동
        //----------------------
        if (_cursorX < 3)
        {
            _cursorX = 33;
        }

        if (_cursorX > 33)
        {
            _cursorX = 3;
        }

        Console.SetCursorPosition(_cursorX, _cursorY);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("   ↓   ");
        Console.ForegroundColor = ConsoleColor.White;

    }

    public void RenderBurgerTile()
    {
        //---------------------------------------
        // 플레이어 버거 표시 테두리 찍어주는 부분
        //--------------------------------------
        const int renderUIy = 8;
        const int renderUIx = 50;

        Console.SetCursorPosition(renderUIx, renderUIy);

        for (int y = renderUIy; y < renderUIy + 7; y++)
        {
            for (int x = renderUIx; x < renderUIx + 15; x++)
            {

                if (y == renderUIy || y == renderUIy + 6)
                {
                    Console.Write("-");
                    continue;
                }

                if (x == renderUIx || x == renderUIx + 14)
                {
                    Console.Write("|");
                    continue;
                }

                Console.Write(" ");

            }

            Console.WriteLine(); Console.SetCursorPosition(renderUIx, y + 1);

        }
    }

    public void RenderBuger()
    {
        //--------------------------
        // 플레이어 버거 랜더 
        // 네모 하나 찍어주고
        // 현재 플레이어 버거에 들어와있는 내용 출력
        // ** 식재료 추가시 여기도 수정**  **식재료**
        //--------------------------
        const int renderUIy = 8;
        const int randerUIx = 50;

        Food[] afood = Qburger.playerBurger.oQueue.ToArray();  // 현재 버거값 게임매니저에서 넘겨받아서 활용

        for (int i = 0; i < afood.Length; i++)
        {
            Console.SetCursorPosition(randerUIx + 1, renderUIy + i + 1);

            switch (afood[i])
            {
                case Food.Bread:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write("             ");
                    break;
                case Food.Cheese:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("             ");
                    break;
                case Food.Patty:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("             ");
                    break;
                case Food.Vege:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("             ");
                    break;
                default:
                    break;
            }

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
        }

    }

    public void RenderStat()
    {
        //--------------------------
        // 플레어이 상태
        // 플레이어 맨탈만큼 하트로 찍기
        //--------------------------
        int PositionX = 75;
        int PositionY = 3;

        Console.SetCursorPosition(PositionX, PositionY);
        Console.Write("Fucking Money : " + player.Money + " 햄벅$");
        Console.SetCursorPosition(PositionX, PositionY + 1);
        Console.Write("    My Mental : ");
        Console.Write("♥♥♥♥");
        Console.SetCursorPosition(PositionX+16, PositionY+1);
        for (int i = 0; i < (int)MaxMental.Max-player.Mental; i++)
        {
            Console.Write("  ");

        }

    }
    public void CusorShadow()
    {
        //---------------------------
        // 커서 그림자 지워주는 함수
        //--------------------------
        Console.SetCursorPosition(_cursorX, _cursorY);
        Console.WriteLine("       ");
    }

    public void Update(eDir a_eDir)
    {
        //-----------------------------
        // 커서키 조절부
        // 스페이스바 일경우 FoodUIRender 의 식재료 좌표와 매칭
        // 스페이스 누르면 엔큐 실행 -> 좌표값 판별하여 식재료 반별
        // ** 식재료 추가시 여기서 수정   **식재료
        // ** 키 추가시 여기서 수정       **키
        //-----------------------------
        switch (a_eDir)
        {
            case eDir.Left:
                CusorShadow();
                _cursorX -= 10;
                break;
            case eDir.Right:
                CusorShadow();
                _cursorX += 10;
                break;
            case eDir.Space:
                if      (_cursorX == 3)  { Qburger.playerBurger.Enqueue(Food.Bread); }
                else if (_cursorX == 13) { Qburger.playerBurger.Enqueue(Food.Patty); }
                else if (_cursorX == 23) { Qburger.playerBurger.Enqueue(Food.Vege); }
                else if (_cursorX == 33) { Qburger.playerBurger.Enqueue(Food.Cheese); }
                break;
            default:
                break;
        }
    }
}
