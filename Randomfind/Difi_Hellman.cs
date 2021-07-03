using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Randomfind
{
    class Difi_Hellman
    {
       public Biginteger_s module = new Biginteger_s();
        public int g;

        public int Xa = new Random().Next();
        public int Xb = new Random().Next();
        public BigInteger Ya;
        public BigInteger Yb;
        public BigInteger Kab;
        public BigInteger Kba;

        public Difi_Hellman()
        {
            this.g = Find_G();
            Emulate_AToB();
            Emulate_BToA();
            this.Kab = BigInteger.ModPow(Yb, Xa, module.big); 
            this.Kba = BigInteger.ModPow(Ya,Xb,module.big);
        }


        public void Emulate_AToB()
        {
            this.Ya = BigInteger.ModPow(this.g, this.Xa, this.module.big);
            
        }
        public void Emulate_BToA()
        {
            this.Yb = BigInteger.ModPow(this.g, this.Xb, this.module.big);
        }


        private int Find_G()
        {
            int G = 0;
            List<BigInteger> L = new List<BigInteger>();
            for(int i =0; i<module.razlozenie.Length; i++)
            {
                if (module.stepen[i] >0)
                {
                    L.Add((module.big - 1) / module.razlozenie[i]);
                }
            }
            foreach (var num in module.razlozenie)
            {
                bool flag = true;

                foreach (var item in L)
                {
                    if (BigInteger.ModPow(num,item,module.big) ==1)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    G = num;
                    break;
                }
            }
            return G;
        }




    }
}
