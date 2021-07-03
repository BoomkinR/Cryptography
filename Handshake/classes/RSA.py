from math import gcd
import hashlib


class RSA:

    def __init__(self):
        self.p = 1529433128427638353921 # только для 1 идентивикатора
        self.q = 8059707327845829427201
        self.n = self.p * self.q
        self.F_e = (self.p - 1) * (self.q - 1)
        self.e = self.Find_Open_Key()
        self.d = self.Get_invert(self.e)
        self.key = '932320'


    def Find_Open_Key(self):
        i = 1
        e = 123
        while gcd(e, self.F_e) != 1:
            e = e * i + 1
            i += 1
        return e

    def Get_invert(self, e):
        a, b, res = self.gcdex(self.F_e, e)
        return res

    def gcdex(self, a, b):
        if b == 0:
            return a, 1, 0
        else:
            d, x, y = self.gcdex(b, a % b)
            return d, y, x - y * (a // b)

    def GetHashSum(self, text):
        hash = hashlib.sha256()
        hash.update(text.encode('utf-8'))
        dex = hash.hexdigest()
        res = 0
        for i in range(len(dex)):
            res += int(dex[len(dex) - 1 - i], 16) * pow(10, len(dex) - 1 - i)
        return res

    def Write(self,text):
        h = self.GetHashSum(text)
        s = pow(self.h, self.d, self.n)
        print(s)
        return str(text) + '||'+str(s)

    def Verification(self, text):
        txt = text.split('||')
        print(pow(txt[2],self.e, self.n))
        if txt[1] == pow(txt[2],self.e, self.n):
            print('Wll done')



