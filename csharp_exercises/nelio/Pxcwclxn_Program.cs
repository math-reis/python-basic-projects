using System;
using Bergs.Pxc.Pxcoiexn.Interface;

namespace Bergs.Pxc.Pxcwclxn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 250;
            using (MinhaTela minhaTela = new MinhaTela(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                minhaTela.Executar();
            }
        }
    }
}
