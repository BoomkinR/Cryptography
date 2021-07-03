class Card:


    def __init__(self, rank, suit):
        self.rank = rank
        self.suit = suit
        self.RANKS = {
            2: "2",
            3: "3",
            4: "4",
            5: "5",
            6: "6",
            7: "7",
            8: "8",
            9: "9",
            10: "10",
            11: "J",
            12: "Q",
            13: "K",
            14: "A",
        }
        self.SUITS = {
            3: u"\u2665",  # черви
            2: u"\u2666",  # буби
            1: u"\u2663",  # крести
            0: u"\u2660",  # Пики
        }

    def __str__(self):
        return f'{self.RANKS[self.rank]} {self.SUITS[self.suit]}'