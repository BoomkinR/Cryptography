from classes.RSA import RSA
from classes.HandShake_Protocol import HandShake_Protocol
from classes.Shauma import Shauma
from des import DesKey


rsa = RSA()
hand =  HandShake_Protocol()
shauma = Shauma()
key = 'abcdefgh'
DES = DesKey(key.encode('utf-8'))

# A -> B r1
r1 = '13372280'
print(f'A->B r1: {r1}')
print('B -> A s1 = Ek(r1,22813370)')
s1 = DES.encrypt((str(r1) + str(22813370)).encode('utf-8'))
print(f's1 = {s1}')
print('A получает s1 , проверяет r1')
D_s1 = DES.decrypt(s1)
print(f"r1' = {str(D_s1[0:len(r1)])[2:-1]} r2' = {D_s1[len(r1)::]}")
print()
if str(D_s1[0:len(r1)])[2:-1]== r1:
    print('Идентефицирован Б как свой')
    r2 =str(D_s1[len(r1)::])[2:-1]
    s2 = DES.encrypt((str(r2)+str(r1)).encode('utf-8'))
    print(f's2 = {s2}')
    input ('A->B s2 для проверки')
    D_s2 = DES.decrypt(s2)
    if str(D_s2[0:len(str(22813370))])[2:-1] == '22813370':
        print('Идентефицирован A как свой')
        input ('B->A n,e для подписи открытые ключи')
        n = shauma.n
        e=shauma.e
        input('A загадывает число k')
        x = 8345515145
        k=17
        print(f' k ={k} \n x = {x}')
        D_x = x*pow(k,e,n)
        print(f' тправляет dx = {D_x}')
        input('B подписывает')
        D_x = pow(D_x,shauma.d,n)
        print(f'Dx = {D_x}')

        D_A = D_x* rsa.Get_invert(k) %n

        print(f'A после расшифра получает {D_A}')

