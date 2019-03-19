import unittest
import numpy as np
from nptest import nptest

class MathematicalFunctionsTests(unittest.TestCase):

    def test_sin_1(self):

        a = np.arange(0, 10, dtype = np.float64)
        a = a[::2]
        b = np.sin(a)
        print(b)

        a = np.arange(0, 10, dtype = np.float32)
        a = a[::2]
        b = np.sin(a)
        print(b)

        a = np.arange(0, 10, dtype = np.int16)
        a = a[::2]
        b = np.sin(a)
        print(b)
        
        print("********")

        a = np.arange(0, 10, dtype = np.float64).reshape((1,2,5))
        a = a[::2]
        b = np.sin(a)
        print(b)

        print("********")

        a = np.array([[0,1,2,3,4],[5,6,7,8,9]])
        a = a[::2]

        x = a>2

        out = np.zeros_like(a, dtype=np.float64)

        b = np.sin(a, where= x, out = out )
        print(b)

    def test_cos_1(self):

        a = np.arange(0, 10, dtype = np.float64)
        a = a[::2]
        b = np.cos(a)
        print(b)

        a = np.arange(0, 10, dtype = np.float32)
        a = a[::2]
        b = np.cos(a)
        print(b)

        a = np.arange(0, 10, dtype = np.int16)
        a = a[::2]
        b = np.cos(a)
        print(b)
        
        print("********")

        a = np.arange(0, 10, dtype = np.float64).reshape((1,2,5))
        a = a[::2]
        b = np.cos(a)
        print(b)

        print("********")

        a = np.array([[0,1,2,3,4],[5,6,7,8,9]])
        a = a[::2]

        x = a>2

        out = np.zeros_like(a, dtype=np.float64)

        b = np.cos(a, where= x, out = out )
        print(b)

    def test_tan_1(self):

        a = np.arange(0, 10, dtype = np.float64)
        a = a[::2]
        b = np.tan(a)
        print(b)

        a = np.arange(0, 10, dtype = np.float32)
        a = a[::2]
        b = np.tan(a)
        print(b)

        a = np.arange(0, 10, dtype = np.int16)
        a = a[::2]
        b = np.tan(a)
        print(b)
        
        print("********")

        a = np.arange(0, 10, dtype = np.float64).reshape((1,2,5))
        a = a[::2]
        b = np.tan(a)
        print(b)

        print("********")

        a = np.array([[0,1,2,3,4],[5,6,7,8,9]])
        a = a[::2]

        x = a>2

        out = np.zeros_like(a, dtype=np.float64)

        b = np.tan(a, where= x, out = out )
        print(b)

    def test_arcsin_1(self):

        a = np.linspace(-1.0, 1.0, 12)
        print(a)
        b = np.arcsin(a)
        print(b)

       
        print("********")

        a = np.linspace(-1.0, 1.0, 12).reshape((2,2,3))
        a = a[::2]
        b = np.arcsin(a)
        print(b)

        print("********")

        a = np.linspace(-1.0, 1.0, 12)
        a = a[::2]

        x = a > -0.5
        print(x)

        out = np.zeros_like(a, dtype=np.float64)

        b = np.arcsin(a, where= x, out = out )
        print(b)

if __name__ == '__main__':
    unittest.main()