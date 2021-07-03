using System;
using System.Collections.Generic;
using System.Text;

namespace Randomfind
{
    class Manager
    {


        public void ShowRandom_Zp()
        {
            Biginteger_s c = new Biginteger_s(1);
            int sum = 0;
            while (c.prost.Count < 10)
                c.Get(c.mas);
            for (int i = 0; i < 10; i++)
            {
                System.Console.Write(i + "||");
                System.Console.Write("||" + c.prost[i].prost.ToString() + "||");
                System.Console.Write(c.prost[i].e + "||");
                System.Console.WriteLine(c.prost[i].k);
                sum += c.prost[i].k;

            }

            System.Console.WriteLine("Матожидание =" + c.GetQuantity());
            System.Console.WriteLine("Получилось = " + sum / 10);
        }

        public void Defi_protocol()
        {
            
            Difi_Hellman difi = new Difi_Hellman();
            Console.WriteLine(difi.module.big);
            Console.WriteLine(difi.g);
            Console.WriteLine("Рандомное число А:"+ difi.Xa);
            Console.WriteLine("Рандомное число B:" + difi.Xb);
            Console.WriteLine("Key Ab: " + difi.Kab);
            Console.WriteLine("Key Ab: " + difi.Kba);
        }

        public void RSA_Signature()
        {
            var rsa = new RSA_Sign();
            Console.WriteLine(rsa.e.big + "-e");
            Console.WriteLine(rsa.P.big * rsa.Q.big +"P*Q");
            Console.WriteLine(rsa.D + "D");
        }

        public void Money_proto()
        {
            var protocol = new Money_protocol();
            Console.WriteLine(protocol.P.big +"  -Zp  "+ protocol.g);
            protocol.Emulation_Alice_step1();
            protocol.Emulation_Bob_step1();
            protocol.Emulation_Alice_step2();
            protocol.Emulation_Bob_step2();
        }
    }
}
