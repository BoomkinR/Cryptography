from tokenize import t

import numpy


class PSG:

    def __init__(self, num, val):
        self.stay = list(bin(int(val))[2:int(num) + 2])
        self.cortege = {
            4: [0, 0, 1, 1],
            5: [1, 1, 0, 0, 1],
            6: [1, 0, 1, 0, 1, 1],
            7: [1, 0, 1, 0, 0, 1, 1]}
        self.mask = self.cortege[int(num)]

    def Get(self):
        x = 0
        for s, m in zip(self.stay, self.mask):
            x ^= int(s) & m

        self.stay.insert(0, x)
        res = self.stay.pop()

        return res
