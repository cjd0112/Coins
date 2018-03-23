using System;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public static class IntArrayToLong
    {
        private const int aBits = 6;
        private const int bBits = 7;
        private const int cBits = 8;
        private const int dBits = 9;
        private const int eBits = 10;
        private const int fBits = 11;
        private const int gBits = 12;


        private const int aPos = aBits;
        private const int bPos = aPos + bBits;
        private const int cPos = bPos + cBits;
        private const int dPos = cPos + dBits;
        private const int ePos = dPos + eBits;
        private const int fPos = ePos + fBits;
        private const int gPos = fPos + gBits;

        private const int aMax = 1 << aBits;
        private const int bMax = 1 << bBits;
        private const int cMax = 1 << cBits;
        private const int dMax = 1 << dBits;
        private const int eMax = 1 << eBits;
        private const int fMax = 1 << fBits;
        private const int gMax = 1 << gBits;

        private const int aMask = aMax - 1;
        private const int bMask = bMax - 1;
        private const int cMask = cMax - 1;
        private const int dMask = dMax - 1;
        private const int eMask = eMax - 1;
        private const int fMask = fMax - 1;
        private const int gMask = gMax - 1;

        public static long ConvertToLong(int[] arr)
        {

#if DEBUG
            VerifySizes(arr);

#endif 
            long res = 0;
            var l = arr.Length;
            if (l == 7)
            {
                res = (((long) arr[0])) << 64 - aPos | ((long) arr[1]) << 64 - bPos | ((long) arr[2]) << 64 - cPos |
                      ((long) arr[3]) << 64 - dPos |
                      ((long) arr[4]) << 64 - ePos | ((long) arr[5]) << 64 - fPos | ((long) arr[6] << 64 - gPos);
            }

            else if (l == 6)
            {
                res = (((long)arr[0])) << 64 - bPos | ((long)arr[1]) << 64 - cPos | ((long)arr[2]) << 64 - dPos | ((long)arr[3]) << 64 - ePos |
                      ((long)arr[4]) << 64 - fPos | ((long)arr[5]) << 64 - gPos;

            }
            else if (l == 5)
            {
                res = ((long)arr[0]) << 64 - cPos | ((long)arr[1]) << 64 - dPos | ((long)arr[2]) << 64 - ePos |
                      ((long)arr[3]) << 64 - fPos | ((long)arr[4]) << 64 - gPos;

            }

            else if (l == 4)
            {
                res = ((long)arr[0]) << 64 - dPos | ((long)arr[1]) << 64 - ePos |
                      ((long)arr[2]) << 64 - fPos | ((long)arr[3]) << 64 - gPos;

            }
            else if (l == 3)
            {
                res = ((long)arr[0]) << 64 - ePos |
                      ((long)arr[1]) << 64 - fPos | ((long)arr[2]) << 64 - gPos;

            }
            else if (l == 2)
            {
                res = ((long)arr[0]) << 64 - fPos | ((long)arr[1]) << 64 - gPos;

            }
            else if (l == 1)
            {
                res = ((long)arr[0]) << 64 - gPos;

            }
            else
            {
                throw new Exception($"Invalid array length found - {0} - expected max 6");
            }

            return res;

        }


        // make public for testing
        public static int[] ConvertFromLong(long val)
        {
            long a = val >> 64 - aPos & aMask;
            long b = val >> 64 - bPos & bMask;
            long c = val >> 64 - cPos & cMask;
            long d = val >> 64 - dPos & dMask;
            long e = val >> 64 - ePos & eMask;
            long f = val >> 64 - fPos & fMask;
            long g = val >> 64 - gPos & gMask;
            if (a == 0 && b == 0 && c == 0 && d == 0 && e == 0 && f == 0)
            {
                return new int[] { (int) g};
            }
            else if (a == 0 && b == 0 && c == 0 && d == 0 && e == 0)
                return new int[] { (int)f,(int)g };
            else if (a == 0 && b == 0 && c == 0 && d == 0)
                return new int[] { (int)e, (int)f, (int)g };
            else if (a == 0 && b == 0 && c == 0)
                return new int[] { (int)d, (int)e, (int)f, (int)g };
            else if (a == 0 && b == 0)
                return new int[] { (int)c, (int)d, (int)e, (int)f, (int)g };
            else if (a == 0)
                return new int[] { (int)b, (int)c, (int)d, (int)e, (int)f, (int)g };

            return new int[] { (int)a, (int)b, (int)c, (int)d, (int)e, (int)f, (int)g };

        }



        static void VerifySizes(int[] arr)
        {
            var l = arr.Length;
            if (l == 7)
            {
                if (arr[0] >= aMax)
                    t(0, arr[0], aMax, l);
                if (arr[1] >= bMax)
                    t(1, arr[1], bMax, l);
                if (arr[2] >= cMax)
                    t(2, arr[2], cMax, l);
                if (arr[3] >= dMax)
                    t(3, arr[3], dMax, l);
                if (arr[4] >= eMax)
                    t(4, arr[4], eMax, l);
                if (arr[5] >= fMax)
                    t(5, arr[5], fMax, l); 
                if (arr[6] >= gMax)
                    t(6,arr[6],gMax,1);

            }
            else if (l == 6)
            {
                if (arr[0] >= bMax)
                    t(0, arr[0], bMax, l);
                if (arr[1] >= cMax)
                    t(1, arr[1], cMax, l);
                if (arr[2] >= dMax)
                    t(2, arr[2], dMax, l);
                if (arr[3] >= eMax)
                    t(2, arr[3], eMax, l);
                if (arr[4] >= fMax)
                    t(4, arr[4], fMax, l);
                if (arr[5] >= gMax)
                    t(5, arr[5], gMax, 1);


            }
            else if (l == 5)
            {
                if (arr[0] >= cMax)
                    t(0, arr[0], cMax, l);
                if (arr[1] >= dMax)
                    t(1, arr[1], dMax, l);
                if (arr[2] >= eMax)
                    t(2, arr[2], eMax, l);
                if (arr[3] >= fMax)
                    t(3, arr[3], fMax, l);
                if (arr[4] >= gMax)
                    t(4, arr[4], gMax, l);

            }
            else if (l == 4)
            {
                if (arr[0] >= dMax)
                    t(0, arr[0], dMax, l);
                if (arr[1] >= eMax)
                    t(1, arr[1], eMax, l);
                if (arr[2] >= fMax)
                    t(2, arr[2], fMax, l);
                if (arr[3] >= gMax)
                    t(3, arr[3], gMax, l);

            }
            else if (l == 3)
            {
                if (arr[0] >= eMax)
                    t(0, arr[0], eMax, l);
                if (arr[1] >= fMax)
                    t(1, arr[1], fMax, l);
                if (arr[2] >= gMax)
                    t(2, arr[2], gMax, l);
            }
            else if (l == 2)
            {
                if (arr[0] >= fMax)
                    t(0, arr[0], fMax, l);
                if (arr[1] >= gMax)
                    t(1, arr[1], gMax, l);
            }
            else if (l == 1)
            {
                if (arr[0] >= gMax)
                    t(0, arr[0], gMax, l);
            }


        }


        static void t(int pos, int val, int max, int array_length)
        {
            throw new ArgumentOutOfRangeException(
                $"Integer - @ {pos} in array of lenth - {array_length} of value {val} exceeded expected maximum - {max}");
        }

    }
}
