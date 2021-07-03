from classes.PSG import PSG


def getX(x1, x2, x3):
    if x1 == 0:
        return x2
    return x3


a = input('first psg:')
b = input('first psg num:')
psg1 = PSG(a, b)
a = input('second psg:')
b = input('second psg num:')
psg2 = PSG(a, b)
a = input('third psg:')
b = input('third psg num:')
psg3 = PSG(a, b)
List = []
for i in range(16):
    x1 = psg1.Get()
    x2 = psg1.Get()
    x3 = psg1.Get()
    List.append(getX(x1, x2, x3))

print(List)
