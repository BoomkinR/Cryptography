from .Player import Player
from .Deck import Deck
import random
from math import gcd


class Protocol:
    def __init__(self):
        self.p = int(3733)
        self.player = []
        self.deck = Deck()
        self.Showen_cards = []

    def Add_Player(self):
        n = input("Количество участников от 2 до 6 : ")
        self.player = []
        for i in range(int(n)):
            C = self.Give_C()
            l, l1, D = self.gcdex(self.p - 1, C)
            print(D*C % (self.p-1))
            self.player.append(Player(i, C, D, self.p))

    def Move_Shufle(self):  # 1 step
        for i in self.player:
            self.deck.cards = i.Shadow(self.deck.cards)
        print('Тусовка прошла успешно, копы не приехали')

    def Give_Cards(self):
        for i in range(len(self.player) * 2):
            tcard = self.Uncover(self.deck.PushCard())
            self.player[i % 3].cards.append(tcard)
        for i in self.player:
            print(i.Info())

    def Show_card(self, n):
        for i in range(n):
            self.Showen_cards.append(self.Uncover(self.deck.PushCard()))
        for i in self.Showen_cards:
            print(i)


    def Uncover(self, cd):
        for i in self.player:
            cd = i.Unshadow(cd)
        return cd
# РАБОТАЕТ
    def Give_C(self):  # Находит рандомный взаимнопростой объект алгоритм долгий
        rand = random.randint(97, 377)
        k = 1
        while gcd(rand, self.p - 1) != 1:
            k += 1
            rand = (rand * k + 1) % self.p
        print(f"Give_C {rand}")
        return rand #



    def gcdex(self, a, b):
        if b == 0:
            return a, 1, 0
        else:
            d, x, y = self.gcdex(b, a % b)
            return d, y, x - y * (a // b)
