import unittest
import numpy as np
from nptest import nptest


class Test_test1(unittest.TestCase):

    def test_rand_1(self):

        #np.random.seed(1234);

        f = np.random.rand()
        print(f)

        arr = np.random.rand(5000000);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));

    def test_randn_1(self):

        #np.random.seed(1234);

        f = np.random.randn()
        print(f)

        arr = np.random.randn(5000000);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));

    def test_randint_1(self):

        #np.random.seed(1234);

        f = np.random.randint(2,3,4)
        print(f)

        arr = np.random.randint(2,3,5000000);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));


        arr = np.random.randint(-2,3,5000000);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));

    def test_randuint_1(self):

        #np.random.seed(1234);

        f = np.random.randint(2,3,4, dtype=np.uint32)
        print(f)

        arr = np.random.randint(2,5,5000000, dtype=np.uint32);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));

    def test_randint64_1(self):

        #np.random.seed(1234);

        f = np.random.randint(2,3,4, dtype=np.int64)
        print(f)

        arr = np.random.randint(2,3,5000000, dtype=np.int64);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));


        arr = np.random.randint(-2,3,5000000, dtype=np.int64);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));

    def test_randuint64_1(self):

        #np.random.seed(1234);

        f = np.random.randint(2,3,4, dtype=np.uint64)
        print(f)

        arr = np.random.randint(2,5,5000000, dtype=np.uint64);
        print(np.amax(arr));
        print(np.amin(arr));
        print(np.average(arr));

    def test_standard_normal_1(self):

        #np.random.seed(1234);
        arr = np.random.standard_normal(5000000);
        print(np.max(arr));
        print(np.min(arr));
        print(np.average(arr));

        
    def test_beta_1(self):

        a = np.arange(1,11, dtype=np.float64);
        b = np.arange(1,11, dtype= np.float64);

        arr = np.random.beta(b, b, 10);
        print(arr);


if __name__ == '__main__':
    unittest.main()
