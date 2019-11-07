using System;
using System.Diagnostics;


class Sceneend
{
    public SceneValue Run(SceneValue a_SceneValue)
    {
        //-----------------------------------------------
        // 게임을 계속 할지 물어보는 부분
        //-----------------------------------------------
        Debug.Assert(a_SceneValue == SceneValue.EndMessage);

        Console.Clear();
        Console.WriteLine("다시하시려면 Y키 끝낼려면 N키");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.Y:
                a_SceneValue = SceneValue.Start;
                return a_SceneValue;
            case ConsoleKey.N:
                a_SceneValue = SceneValue.End;
                return a_SceneValue;
            default:
                return a_SceneValue;
        }
    }

}

