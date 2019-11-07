using System;
using System.Diagnostics;

class SceneManager
{
    SceneStart  start;                       //스타트씬                                       
    Sceneend    end;                         //종료씬
    GameManager gameManager;                 //인게임
    public static SceneValue Scene;          //현재 씬값

    public SceneManager()
    {
 
        Scene = new SceneValue();
        Scene = SceneValue.Start;
        start = new SceneStart();
        gameManager = new GameManager();
        end = new Sceneend();
    }


    public void run()
    {
        //------------------------------------
        // 스타트 -> 인게임 -> 계속할지물어봄
        //-----------------------------------
        while (Scene != SceneValue.End)
        {
            switch (Scene)
            {
                case SceneValue.Start:
                    start.run();
                    Scene = SceneValue.InGame;
                    break;
                case SceneValue.InGame:
                    Scene = gameManager.Run(Scene);
                    break;
                case SceneValue.EndMessage:
                    Scene = end.Run(Scene);
                    break;
                default:
                    break;
            }
        }
        EndMessage();
    }

    public void EndMessage()
    {
        Console.Clear();
        Console.WriteLine("이용해 주셔서 감사합니다.");
        Console.ReadKey();
    }
}

