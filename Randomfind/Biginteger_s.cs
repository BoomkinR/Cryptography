using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Randomfind
{
    class Biginteger_s
    {
        public BigInteger big;
        public int n =0;
        public List<struk> prost = new List<struk>();
        public byte[] mas = new byte[16]; // байты минимума
        public int[] razlozenie = new int[9] { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
        public int[] stepen = new int[9];

        public  Biginteger_s(int n)
        {        
            mas[n % 16] = 128;
           this.big = new BigInteger(mas);
           
        }

        public Biginteger_s()
        {

            Random random = new Random();
            while (true)
            {
                for (int i = 0; i < stepen.Length; i++)
                {
                    stepen[i] = random.Next(10, 500);
                }

                this.big = 1;
                for (int j = 0; j < razlozenie.Length; j++)
                {
                    this.big *= new BigInteger(razlozenie[j]) ^ stepen[j];

                }
                mas[n % 16] = 128;
                if (new BigInteger(mas) < this.big+1)
                if (MillerRabinTest(this.big+1))
                {
                    this.big += 1;
                    break;
                }
            }
            
        } // если генерировать простое число, то так, тут с разложением

        public BigInteger MaxValue()
        {
            byte[] val = new byte[16];
            val[0] = 0;
            for (int i = 1; i < 16; i++)
            {
                val[i] = 1;
            }
            return new BigInteger(val);

        }

        public string Get(byte[] min) // получить число котороебудет больше минимально заданного байта
        {
            var mas = new byte[16];
            Random random = new Random();
            random.NextBytes(mas);
            int i = 0;
            while (true && i<16)
            {
                if (min[i] < mas[i]) break;
                mas[i] = min[i];
                i++;
            }

            BigInteger res = new BigInteger(mas);

            if (MillerRabinTest(res))
            {
                //eps
                double E = Convert.ToDouble(n-1) / Convert.ToDouble(n) / 4;
                this.prost.Add(new struk(this.n, res, Math.Pow(E,50)));

                n = 0;
            }
            else this.n++;

            return res.ToString();


        }


        public bool MillerRabinTest(BigInteger n, int k = 50 )
        {
            // если n == 2 или n == 3 - эти числа простые, возвращаем true
            if (n == 2 || n == 3)
                return true;

            // если n < 2 или n четное - возвращаем false
            if (n < 2 || n % 2 == 0)
                return false;

            // представим n − 1 в виде (2^s)·t, где t нечётно, это можно сделать последовательным делением n - 1 на 2
            BigInteger t = n - 1;

            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            // повторить k раз
            for (int i = 0; i < k; i++)
            {
                // выберем случайное целое число a в отрезке [2, n − 2]
                System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();

                byte[] _a = new byte[n.ToByteArray().LongLength];

                BigInteger a;

                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= n - 2);

                // x ← a^t mod n, вычислим с помощью возведения в степень по модулю
                BigInteger x = BigInteger.ModPow(a, t, n);

                // если x == 1 или x == n − 1, то перейти на следующую итерацию цикла
                if (x == 1 || x == n - 1)
                    continue;

                // повторить s − 1 раз
                for (int r = 1; r < s; r++)
                {
                    // x ← x^2 mod n
                    x = BigInteger.ModPow(x, 2, n);

                    // если x == 1, то вернуть "составное"
                    if (x == 1)
                        return false;

                    // если x == n − 1, то перейти на следующую итерацию внешнего цикла
                    if (x == n - 1)
                        break;
                }

                if (x != n - 1)
                    return false;
            }

            // вернуть "вероятно простое"
            return true;
        }

        public BigInteger GetQuantity() // вероятность получить простое число случайно
        {
            BigInteger min = this.big;
            BigInteger max = MaxValue();
            //Pi(x) = x/ln(x)
            //P = Pi(x) / (x1-x2)
            BigInteger log1 = new BigInteger(BigInteger.Log(min));
            BigInteger Pi1 = min  / log1;
            BigInteger log2 = new BigInteger(BigInteger.Log(max));
            BigInteger Pi2 = max / log2;
            var count = Pi2 - Pi1 ;

            var perc = (max - min) / count  ;
           // double percent = 1 / Convert.ToDouble(perc);
           // var res = new BigInteger(Math.Log(0.9, 1 - percent));

            return perc;
        }

        public BigInteger Inverse( BigInteger a) // обратынй элемент по модулю м a = Zp
        {
            //a = a%m
            //x*a + y*m = g
            BigInteger b = this.big;
            BigInteger x = 0;
            BigInteger y;
            BigInteger x1 = 1;
            BigInteger x2 = 0;
            BigInteger y1 = 1;
            BigInteger y2 = 0;
            BigInteger q;
            BigInteger r;
            if (BigInteger.GreatestCommonDivisor(a,b) >1)
            {
                Console.WriteLine("ne vzimno prostie");
            }

            while (a > 0 && b >0)
            {
                q = a / b;
                r = a - q * b; // можно сразу написать b = ... просто так удобней понимать
                x = x2 - q * x1;
                y = y2 - q* y1;

                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }
            return x;


            }


        public int Find_G()
        {
            int G = 0;
            List<BigInteger> L = new List<BigInteger>();
            for (int i = 0; i < this.razlozenie.Length; i++)
            {
                if (this.stepen[i] > 0)
                {
                    L.Add((this.big - 1) / this.razlozenie[i]);
                }
            }
            foreach (var num in this.razlozenie)
            {
                bool flag = true;

                foreach (var item in L)
                {
                    if (BigInteger.ModPow(num, item, this.big) == 1)
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
        } // нахождение порождающего элемента

    }

    class struk
    {
        public int k;
        public BigInteger prost;
        public double e;
        public struk(int k, BigInteger prost, double e)
        {
            this.k = k;
            this.prost = prost;
            this.e = e;
        }
    }

    
}
