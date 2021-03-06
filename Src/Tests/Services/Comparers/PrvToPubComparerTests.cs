﻿// The FinderOuter
// Copyright (c) 2020 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using FinderOuter.Services.Comparers;
using System.Collections.Generic;
using Xunit;

namespace Tests.Services.Comparers
{
    public class PrvToPubComparerTests
    {
        public static IEnumerable<object[]> GetHashCases()
        {
            yield return new object[] { KeyHelper.Pub1CompHex, true };
            yield return new object[] { KeyHelper.Pub1UnCompHex, true };
            yield return new object[] { "040b3ad1cea48c61bdcff356675d92010290cdc2e04e1c9e68b6a01d3cec746c17", false };
            yield return new object[] { "0b3ad1cea48c61bdcff356675d92010290cdc2e04e1c9e68b6a01d3cec746c17", false };
            yield return new object[] { "FOO", false };
        }

        [Theory]
        [MemberData(nameof(GetHashCases))]
        public void InitTest(string pubHex, bool expected)
        {
            var comp = new PrvToPubComparer();
            bool actual = comp.Init(pubHex);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CompareTest()
        {
            var comp1 = new PrvToPubComparer();
            var comp2 = new PrvToPubComparer();
            Assert.True(comp1.Init(KeyHelper.Pub1CompHex));
            Assert.True(comp2.Init(KeyHelper.Pub1UnCompHex));

            byte[] key = KeyHelper.Prv1.ToBytes();
            key[0]++;

            bool b1 = comp1.Compare(key);
            bool b2 = comp2.Compare(key);
            Assert.False(b1);
            Assert.False(b2);

            key[0]--;
            b1 = comp1.Compare(key);
            b2 = comp2.Compare(key);
            Assert.True(b1);
            Assert.True(b2);
        }
    }
}
