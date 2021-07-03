from .Deck import Deck
from .Card import Card
import random


class Player:
    def __init__(self, id, C, D, p):
        self.cards = []
        self.Id = id
        self.D = D
        self.C = C
        self.p = p

    def Shadow(self, deck):
        print(f'последний элемент был {deck[-1].rank}')
        for item in deck:
            item.rank = pow(item.rank ,self.C , self.p)
        random.shuffle(deck)
        #print(f'Player {self.Id} затенил колоду')
        print(f'стал {deck[-1].rank}')
        return deck

    def Unshadow(self, item):

            print(f'Player {self.Id} открыл карту {item.rank}')
            item.rank = pow(item.rank ,self.D , self.p)
            print(f'Она стала {item.rank}' )

            return item

    def Info(self):
        print(f'ID : {self.Id} \n P : {self.p} \n D : {self.D} \n C : {self.C}')
        if len(self.cards ) >0:
            for i in self.cards:
                print(f'{i}')