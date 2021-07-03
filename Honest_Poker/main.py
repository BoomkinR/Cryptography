from classes.Card import Card
from classes.Deck import Deck
from classes.Player import Player
from  classes.Protokol import Protocol


Game = Protocol()
Game.Add_Player() # добавим игроков
Game.Move_Shufle()
Game.Give_Cards()
input("Press key to show FLOP")
Game.Show_card(3)
input("Press key to show Turn")
Game.Show_card(1)
input("Press key to show River")
Game.Show_card(1)

