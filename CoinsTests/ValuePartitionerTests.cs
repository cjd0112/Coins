using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsLib.Values;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class ValuePartitionerTests
    {
        [Test]
        public void Partition_1_IsEmptySet()
        {
            Assert.IsFalse(ValuePartitioner.PossibleWaysToDivideValueInTwo(1).Any() );
        }

        [Test]
        public void Partition_2_HasOneSequence()
        {
            Assert.IsTrue(ValuePartitioner.PossibleWaysToDivideValueInTwo(2).Count() == 1);
        }

        [Test]
        public void Partition_2_HasEqualValues()
        {
            Assert.IsTrue(ValuePartitioner.PossibleWaysToDivideValueInTwo(2).All(x=>x.lhs == 1 && x.rhs == 1));
        }


        [Test]
        public void Partition_3_HasTwoSequence()
        {
            Assert.IsTrue(ValuePartitioner.PossibleWaysToDivideValueInTwo(3).Count() == 2);
        }

        [Test]

        public void Partition_3_Has2v1()
        { 
            Assert.IsTrue(ValuePartitioner.PossibleWaysToDivideValueInTwo(3).Count(x=>x.lhs == 2 && x.rhs == 1) == 1);
            Assert.IsTrue(ValuePartitioner.PossibleWaysToDivideValueInTwo(3).Count(x=>x.lhs == 1 && x.rhs == 2)==1);

        }
    }
}
