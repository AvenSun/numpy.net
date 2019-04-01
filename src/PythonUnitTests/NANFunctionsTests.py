import unittest
import numpy as np
from nptest import nptest


class NANFunctionsTests(unittest.TestCase):

    def test_nanmin_1(self):

        a = np.array([[1, 2], [3, np.nan]])

        b = np.nanmin(a)
        print(b)
    
        c = np.nanmin(a, axis=0)
        print(c)

        d = np.nanmin(a, axis=1)
        print(d)

        # When positive infinity and negative infinity are present:

        e = np.nanmin([1, 2, np.nan, np.inf])
        print(e)

        f = np.nanmin([1, 2, np.nan, np.NINF])
        print(f)

        g = np.amin([1, 2, -3, np.NINF])
        print(g)

    def test_nanmin_2(self):

        a = np.array([[1, 2], [3, np.nan]], dtype=np.float64)

        b = np.nanmin(a)
        print(b)
    
        c = np.nanmin(a, axis=0)
        print(c)

        d = np.nanmin(a, axis=1)
        print(d)

        # When positive infinity and negative infinity are present:

        e = np.nanmin([1, 2, np.nan, np.inf])
        print(e)

        f = np.nanmin([1, 2, np.nan, np.NINF])
        print(f)

        g = np.amin([1, 2, -3, np.NINF])
        print(g)

        
    def test_nanmin_3(self):

        a = np.array([[1, 2], [3, -4]], dtype=np.int64)

        b = np.nanmin(a)
        print(b)
    
        c = np.nanmin(a, axis=0)
        print(c)

        d = np.nanmin(a, axis=1)
        print(d)

    def test_nanmin_4(self):

        a = np.array([[np.nan, np.nan], [np.nan, np.nan]])

        b = np.nanmin(a)
        print(b)
    
        c = np.nanmin(a, axis=0)
        print(c)

        d = np.nanmin(a, axis=1)
        print(d)
     

    def test_nanmax_1(self):

        a = np.array([[1, 2], [3, np.nan]])

        b = np.nanmax(a)
        print(b)
    
        c = np.nanmax(a, axis=0)
        print(c)

        d = np.nanmax(a, axis=1)
        print(d)

        # When positive infinity and negative infinity are present:

        e = np.nanmax([1, 2, np.nan, np.NINF])
        print(e)

        f = np.nanmax([1, 2, np.nan, np.inf])
        print(f)

        g = np.amax([1, 2, -3, np.inf])
        print(g)


    def test_nanargmin_1(self):

        a = np.array([[np.nan, 4], [2, 3]])
        b = np.argmin(a)
        print(b)

        c = np.nanargmin(a)
        print(c)

        d = np.argmin(a, axis=0)
        print(d)

        e = np.nanargmin(a, axis=0)
        print(e)

        f = np.argmin(a, axis=1)
        print(f)

        g = np.nanargmin(a, axis=1)
        print(g)

        try:

            a = np.array([[np.nan, np.nan], [np.nan, np.nan]])
            h = np.nanargmin(a, axis=1)
            print(h)
        except:
           return

    def test_nanargmax_1(self):

        a = np.array([[np.nan, 4], [2, 3]], dtype=np.float64)
        b = np.argmax(a)
        print(b)

        c = np.nanargmax(a)
        print(c)

        d = np.argmax(a, axis=0)
        print(d)

        e = np.nanargmax(a, axis=0)
        print(e)

        f = np.argmax(a, axis=1)
        print(f)

        g = np.nanargmax(a, axis=1)
        print(g)

        try:

            a = np.array([[np.nan, np.nan], [np.nan, np.nan]])
            h = np.nanargmax(a, axis=1)
            print(h)
        except:
           return

if __name__ == '__main__':
    unittest.main()
