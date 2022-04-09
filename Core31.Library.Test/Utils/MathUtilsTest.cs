using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Core31.Library.Utils;

namespace Core31.Library.Test.Utils
{
    public class MathUtilsTest
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4, 5, 9)]

        public void Test(int input1, int input2, int expect)
        {
            int act = MathUtils.Add(input1, input2);
            Assert.Equal(expect, act);
        }
    }
}