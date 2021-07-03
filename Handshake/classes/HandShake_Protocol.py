from .Grain import Grain

class HandShake_Protocol:

    def __init__(self):

        self.key = 88005553535






    def algoritm(self, text, key): #grain

        text = text.rstrip()
        stroka = ""
        grain = Grain(text, key)
        for z in grain.binary:
            stroka += (grain.TakeBit(z))
        return grain.ToString(stroka)