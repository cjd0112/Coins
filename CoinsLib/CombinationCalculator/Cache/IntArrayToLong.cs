using System;
using System.Collections.Generic;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public static class IntArrayToLong
    {
        private const  int aBits = 8;
        private const int bBits = 8;
        private const int cBits = 8;
        private const int dBits = 10;
        private const int eBits = 12;
        private const int fBits = 14;

        private const int aPos = aBits;
        private const int bPos = aPos + bBits;
        private const int cPos = bPos + cBits;
        private const int dPos = cPos + dBits;
        private const int ePos = dPos + eBits;
        private const int fPos = ePos + fBits;

        private const int aMax = 1 << aBits;
        private const int bMax = 1 << bBits;
        private const int cMax = 1 << cBits;
        private const int dMax = 1 << dBits;
        private const int eMax = 1 << eBits;
        private const int fMax = 1 << fBits;

        private const int aMask = aMax - 1;
        private const int bMask = bMax - 1;
        private const int cMask = cMax - 1;
        private const int dMask = dMax - 1;
        private const int eMask = eMax - 1;
        private const int fMask = fMax - 1;

        public static long ConvertToLong(uint[] arr)
        {

#if DEBUG
            VerifySizes(arr);

#endif 
            long res = 0;
            var l = arr.Length;
            if (l == 6)
            {
                res = (((long)arr[0])) << 64 - aPos | ((long)arr[1]) << 64 - bPos | ((long)arr[2]) << 64 - cPos | ((long)arr[3]) << 64 - dPos |
                      ((long)arr[4]) << 64 - ePos | ((long)arr[5]) << 64 - fPos;

            }
            else if (l == 5)
            {
                res = ((long)arr[0]) << 64 - bPos | ((long)arr[1]) << 64 - cPos | ((long)arr[2]) << 64 - dPos |
                      ((long)arr[3]) << 64 - ePos | ((long)arr[4]) << 64 - fPos;

            }

            else if (l == 4)
            {
                res = ((long)arr[0]) << 64 - cPos | ((long)arr[1]) << 64 - dPos |
                      ((long)arr[2]) << 64 - ePos | ((long)arr[3]) << 64 - fPos;

            }
            else if (l == 3)
            {
                res = ((long)arr[0]) << 64 - dPos |
                      ((long)arr[1]) << 64 - ePos | ((long)arr[2]) << 64 - fPos;

            }
            else if (l == 2)
            {
                res = ((long)arr[0]) << 64 - ePos | ((long)arr[1]) << 64 - fPos;

            }
            else if (l == 1)
            {
                res = ((long)arr[0]) << 64 - fPos;

            }
            else
            {
                throw new Exception($"Invalid array length found - {0} - expected max 6");
            }

            return res;

        }


        // make public for testing
        public static uint[] ConvertFromLong(long val)
        {
            long a = val >> 64 - aPos & aMask;
            long b = val >> 64 - bPos & bMask;
            long c = val >> 64 - cPos & cMask;
            long d = val >> 64 - dPos & dMask;
            long e = val >> 64 - ePos & eMask;
            long f = val >> 64 - fPos & fMask;
            if (a == 0 && b == 0 && c == 0 && d == 0 && e == 0)
                return new uint[] { (uint)f };
            else if (a == 0 && b == 0 && c == 0 && d == 0)
                return new uint[] { (uint)e, (uint)f };
            else if (a == 0 && b == 0 && c == 0)
                return new uint[] { (uint)d, (uint)e, (uint)f };
            else if (a == 0 && b == 0)
                return new uint[] { (uint)c, (uint)d, (uint)e, (uint)f };
            else if (a == 0)
                return new uint[] { (uint)b, (uint)c, (uint)d, (uint)e, (uint)f };

            return new uint[] { (uint)a, (uint)b, (uint)c, (uint)d, (uint)e, (uint)f };

        }



        static void VerifySizes(uint[] arr)
        {
            var l = arr.Length;
            if (l == 6)
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

            }
            else if (l == 5)
            {
                if (arr[0] >= bMax)
                    t(0, arr[0], bMax, l);
                if (arr[1] >= cMax)
                    t(1, arr[1], cMax, l);
                if (arr[2] >= dMax)
                    t(2, arr[2], dMax, l);
                if (arr[3] >= eMax)
                    t(3, arr[3], eMax, l);
                if (arr[4] >= fMax)
                    t(4, arr[4], fMax, l);
            }
            else if (l == 4)
            {
                if (arr[0] >= cMax)
                    t(0, arr[0], cMax, l);
                if (arr[1] >= dMax)
                    t(1, arr[1], dMax, l);
                if (arr[2] >= eMax)
                    t(2, arr[2], eMax, l);
                if (arr[3] >= fMax)
                    t(3, arr[3], fMax, l);
            }
            else if (l == 3)
            {
                if (arr[0] >= dMax)
                    t(0, arr[0], dMax, l);
                if (arr[1] >= eMax)
                    t(1, arr[1], eMax, l);
                if (arr[2] >= fMax)
                    t(2, arr[2], fMax, l);
            }
            else if (l == 2)
            {
                if (arr[0] >= eMax)
                    t(0, arr[0], eMax, l);
                if (arr[1] >= fMax)
                    t(1, arr[1], fMax, l);
            }
            else if (l == 1)
            {
                if (arr[0] >= fMax)
                    t(0, arr[0], fMax, l);
            }


        }


        static void t(int pos, uint val, int max, int array_length)
        {
            throw new ArgumentOutOfRangeException(
                $"Integer - @ {pos} in array of lenth - {array_length} of value {val} exceeded expected maximum - {max}");
        }

    }
}
