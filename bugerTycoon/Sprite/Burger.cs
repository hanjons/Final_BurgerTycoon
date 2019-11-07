using System;


class Burger
{
    public MyQueue<Food> playerBurger;   // 플레이어의 햄버거     
    public KindBurgers   foodValueType; // 패티의 타입.

    public Burger()
    {
        playerBurger = new MyQueue<Food>((int)MaxBurger.MaxburgerNum);
    }

    public string SetBurgers()
    {
        //--------------------------------------
        // 랜덤으로 버거값 받아와서 식재료넣는 부분 
        // ** 레시피 추가하시려면 이곳을 이용하세요 **레시피**
        //--------------------------------------
        KindBurgers foodValueType = RandBurgers();

        switch (foodValueType)
        {
            case KindBurgers.CheeseBurger:
                playerBurger.Enqueue(Food.Bread);
                playerBurger.Enqueue(Food.Cheese);
                playerBurger.Enqueue(Food.Patty);
                playerBurger.Enqueue(Food.Cheese);
                playerBurger.Enqueue(Food.Bread);
                break;
            case KindBurgers.BulgogiBurger:
                playerBurger.Enqueue(Food.Bread);
                playerBurger.Enqueue(Food.Vege);
                playerBurger.Enqueue(Food.Patty);
                playerBurger.Enqueue(Food.Cheese);
                playerBurger.Enqueue(Food.Bread);
                break;
            case KindBurgers.BigMac:
                playerBurger.Enqueue(Food.Bread);
                playerBurger.Enqueue(Food.Patty);
                playerBurger.Enqueue(Food.Patty);
                playerBurger.Enqueue(Food.Cheese);
                playerBurger.Enqueue(Food.Bread);
                break;
            case KindBurgers.VegeBurger:
                playerBurger.Enqueue(Food.Bread);
                playerBurger.Enqueue(Food.Vege);
                playerBurger.Enqueue(Food.Cheese);
                playerBurger.Enqueue(Food.Vege);
                playerBurger.Enqueue(Food.Bread);
                break;
            default:
                break;
        }
        return foodValueType.ToString();
    }




    public KindBurgers RandBurgers()
    {
        //--------------------------------
        // 랜덤으로 버거값 셋팅 해주는 함수
        // ** 레시피 추가하시려면 이곳을 이용하세요 **레시피**
        // 치즈버거와 베지버거 사이에 버거 추가해야 랜덤하게 돌아요~!
        //--------------------------------
        KindBurgers result = new KindBurgers();
        Random r = new Random();

        result = (KindBurgers)r.Next((int)KindBurgers.CheeseBurger, (int)KindBurgers.VegeBurger + 1);
        return result;
    }

    public void BurgerClear()
    {
        //큐에있는 버거값 초기화
        playerBurger.oQueue.Clear();
    }



}
