using System;
using Bergs.Pxc.Pxcoiexn.Interface;

namespace Bergs.Pxc.PxcwFIxn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 500;
            using (MinhaTela minhaTela = new MinhaTela(@"C:\soft\pxc\data\Pxcz01da.mdb"))
            {
                minhaTela.Executar();
            }
        }
    }
}
