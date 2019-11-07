using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugerTycoon
{
	class Program
	{
		static void Main(string[] args)
		{
			SceneManager Smng = new SceneManager();
			Smng.run();

		}
	}
}


//---------------------------------------------------------------------------------------
// 조작법 : 좌, 우 방향키 , 스페이스바, 버거 꽉 차면 스페이스 한번 더눌러야 손놈께 제출
//---------------------------------------------------------------------------------------
// Console.Clear() 추가하면 깜빡임 문제 생깁니다. 잡느라 뒤지는줄 알았습니다.
// 뜨레드안쓰고 틱이 각자 놀아야해서 pc환경마다 버거쪽 깜빡임 문제 있을수 있습니다. 저희집은 정상작동합니다.
//---------------------------------------------------------------------------------------