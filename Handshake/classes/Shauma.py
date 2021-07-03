from math import gcd

class Shauma:

    def __init__(self):
        self.p = 1529433128427638353921  # только для 1 идентивикатора
        self.q = 8059707327845829427201
        self.n = self.p * self.q
        self.F_e = (self.p - 1) * (self.q - 1)
        self.d = self.Find_Open_Key()
        self.e = self.Get_invert(self.d)


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