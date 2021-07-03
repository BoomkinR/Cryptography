using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Net;
using System.Net.Sockets;

namespace Randomfind
{
    class Money_protocol
    {

        public Biginteger_s P = new Biginteger_s();
        public int g;
        private Biginteger_s x_Alice;
        private int b_Alice;
        private int b_Bob;
        private Biginteger_s x_Bob;
        private BigInteger r_Alice;
        private BigInteger r_Bob;
        
        public Money_protocol()
        {
            g = P.Find_G();

        }


        public void Emulation_Alice_step1() 
        {
            //step 1 A выбирает блоб B, генерирует x, передает r
            Console.WriteLine("write 0 if orel, write 1 if reshko");
            b_Alice = Convert.ToInt16(Console.ReadLine()) % 2;
            x_Alice = new Biginteger_s();
            if (x_Alice.big % 2 != b_Alice) x_Alice.big += 1;
            r_Alice = BigInteger.ModPow(g, x_Alice.big, P.big);
            Console.WriteLine($" A передает игроку B значение r = {r_Alice}");
        }

        public void Emulation_Bob_step1() 
        {

            //step 1 B выбирает блоб b, генерирует x, передает r
            Console.WriteLine("write 0 if orel, write 1 if reshko");
            b_Bob = Convert.ToInt16(Console.ReadLine()) % 2;
            x_Bob = new Biginteger_s();
            if (x_Bob.big % 2 != b_Bob) x_Bob.big += 1;
            r_Bob = BigInteger.ModPow(g, x_Bob.big, P.big);
            Console.WriteLine($" A передает игроку B значение r = {r_Bob}");
        }

        public void Emulation_Alice_step2()
        {
            Console.WriteLine("Этап раскрытия для Алисы");
            var b = b_Bob;
            var x = x_Bob;
            var r = BigInteger.ModPow(g, x.big, P.big);
            if (r == r_Bob)
            {
                Console.WriteLine("Bob не соврал");
            }
            else Console.WriteLine("VRUNISHKA");
        }

        public void Emulation_Bob_step2()
        {
            Console.WriteLine("Этап раскрытия для Алисы");
            var b = b_Alice;
            var x = x_Alice;
            var r = BigInteger.ModPow(g, x.big, P.big);
            if (r == r_Alice)
            {
                Console.WriteLine("Alice не соврал");
            }
            else Console.WriteLine("VRUNISHKA");
        }

    }
}
