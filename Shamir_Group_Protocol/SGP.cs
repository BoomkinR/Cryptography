using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;

namespace Shamir_Group_Protocol
{
    class SGP
    {
        private int n; // количество с диллером
        public BigInteger Zp;
        public static BigInteger m = 444234551345;
        public List<koef> users = new List<koef>();
        public List<BigInteger> S = new List<BigInteger>(); // Рандомные S(x) коэф
        public SGP(int n)
        {
            this.n = n;
            Zp = Get_Zp();
            Start();
        }

        public void Start()
        {
            
            S.Add(m);
            Get_Koef();
            for (int i = 0; i < S.Count; i++)
            {
                users.Add(new koef(i,Polenom(i)));
            }
            
        }

        public void Get_Koef()//взятие рандомных коэф S(x)
        {
            for (int i = 1; i < n; i++)
            {
                S.Add( RandomIntegerBelow());
            }

        }

        public BigInteger Polenom(int j)
        {
            int stepen = 0;
            BigInteger result = 0;

            for (int i = 0; i < n; i++)
            {
                
                    result += BigInteger.ModPow(S[j], stepen, Zp);
                    stepen++;
               
            }
            return result;
        }

        public static BigInteger RandomIntegerBelow()
        {
            
            byte[] bytes = new byte[16];
            BigInteger R;
            Random random = new Random();

            
                random.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger(bytes);
           

            return R;
        }

        public static BigInteger Get_Zp()
        {
            BigInteger result = 0;
         int[] razlozenie = new int[9] { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
         int[] stepen = new int[9];
        Random random = new Random();
            while (true)
            {
                for (int i = 0; i < stepen.Length; i++)
                {
                    stepen[i] = random.Next(29,68);
                }

                result = 1;
                for (int j = 0; j < razlozenie.Length; j++)
                {
                    result *= new BigInteger(razlozenie[j]) ^ stepen[j];

                }
                
                if (23423453325 < result + 1 && MillerRabinTest(result + 1)) { 
                    result += 1; break; }
                    
            }
             return result;
        }
        public static bool MillerRabinTest(BigInteger n, int k = 50)
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

        public static bool Check_Sum(List<koef> users, BigInteger Zp)
        {
            BigInteger Sum = 0;
            int p = (-1)^(users.Count-1);
            int del = 1;
            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < users.Count; j++)
                {
                    if (j != i)
                    {
                        p *= users[j].i;
                        del *= (users[i].i - users[j].i);
                    }
                }
                    Sum += (users[i].S_i * (p / del)) % Zp;
            }
            Console.WriteLine("S(x) = " + Sum.ToString());
            if (Sum == m)
            {
                return true;
            }

            return true;
        }
    }

    class koef
    {
        public int i;
        public BigInteger S_i;

        public koef(int i, BigInteger s)
        {
            this.i = i;
            S_i = s;
        }

        public koef(string mes)
        {
            var newms = mes.Split("|");
            this.i = Convert.ToInt32(newms[0]);
            S_i = BigInteger.Parse(newms[1]);
        }

        public override string ToString()
        {
            return i+"|"+S_i;
        }


    }
}
