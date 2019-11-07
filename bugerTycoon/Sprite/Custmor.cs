using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Customor
{
    //---------------------------------
    // 고객의 정보 클래스
    // 인스턴스  //  고객 주문 햄버거, 고객의 인내심 수치 
    //----------------------------------

    public int    patience;                  // 인내심
    public Burger orderBurger;               // 주문버거큐
    public string orderStirng;               // 주문버거문자열값


    public Customor()
    {
        orderBurger = new Burger();
        patience = (int)CustomorMaxLife.Maxpatience;
    }

    public void RandomOrder()
    {                                                                             // 이 문자열은 주문표시용
        orderStirng = orderBurger.SetBurgers();
    }

    public void OrderClear()                                                      // 주문 클리어
    {
        orderBurger.playerBurger.oQueue.Clear();
    }

}
