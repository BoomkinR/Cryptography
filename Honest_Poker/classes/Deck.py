import random
from .Card import Card


class Deck(object):

    def __init__(self):
            self.cards = [Card(rank, suit) for rank in range(2, 15) for suit in range(0, 4)]
            random.shuffle(self.cards)


    def PushCard(self):
        discard = self.cards[-1]
        del self.cards[-1]
        return discard