using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Randomfind
{
    class RSA_Sign
    {
        //n = p*q
        public Biginteger_s P = new Biginteger_s();
        public Biginteger_s Q = new Biginteger_s();
        public Biginteger_s e;
        public BigInteger D;
        public RSA_Sign()
        {
            BigInteger Fe = (P.big - 1) * (Q.big - 1);
            // E - открытый ключ
            e = new Biginteger_s(3);
            // D = e(-1) mod Fe
            D = e.Inverse(P.big * Q.big);


            
        }

        
    }
}
