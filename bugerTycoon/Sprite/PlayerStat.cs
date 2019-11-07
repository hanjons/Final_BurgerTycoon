using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PlayerStat
{
    public int Money;  //돈
    public int Mental; //멘탈

    public PlayerStat()
    {
        Money = 0;
        Mental = (int)MaxMental.Max;   //max멘탈은 4임
    }
}


