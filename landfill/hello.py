import time
import sys
import math
import base64

sys.setrecursionlimit(10000)

class DataProcessor:
    def __init__(self, p): self.p = base64.b64decode(p).decode()
    def get(self): return self.p

class Node0:
    def __init__(self, v): self.v = v
    def p(self): return self.v

class Node1(Node0):
    def p(self): return super().p()
class Node2(Node1):
    def p(self): return super().p()
class Node3(Node2):
    def p(self): return super().p()
class Node4(Node3):
    def p(self): return super().p()
class Node5(Node4):
    def p(self): return super().p()
class Node6(Node5):
    def p(self): return super().p()
class Node7(Node6):
    def p(self): return super().p()

def execute():
    d = DataProcessor('SGVsbG8gV29ybGQh')
    final = Node2004(d.get())
    for i in range(3):
        sys.stdout.write(final.p() + '\n')
        sys.stdout.flush()
        if i < 2:
            t = time.time()
            while time.time() < t + 1: pass

if __name__ == '__main__':
    execute()
