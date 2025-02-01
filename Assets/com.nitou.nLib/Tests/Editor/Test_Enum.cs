using System;
using System.Collections.Generic;
using NUnit.Framework;

// [REF]
//  qiita: Unity Test Runnerの使い方を理解する https://qiita.com/riekure/items/b0f89280ecfcfa626f7b

namespace Nitou.Tests {

    public enum SampleEnum {
        First,
        Second,
        Third,
        Fourth
    }

    public enum TestEnum {
        First,
        Second,
        Third,
        Fourth
    }


    public class EnumExtensionsTests {

        [Test]
        public void IsAnyOf_SingleMatch_ReturnsTrue() {
            // Arrange
            var value = TestEnum.Second;

            // Act
            var result = value.IsAnyOf(TestEnum.Second);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAnyOf_MultipleMatches_ReturnsTrue() {
            // Arrange
            var value = TestEnum.Third;

            // Act
            var result = value.IsAnyOf(TestEnum.First, TestEnum.Third, TestEnum.Fourth);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAnyOf_NoMatch_ReturnsFalse() {
            // Arrange
            var value = TestEnum.First;

            // Act
            var result = value.IsAnyOf(TestEnum.Second, TestEnum.Third);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsAnyOf_EmptyArray_ReturnsFalse() {
            // Arrange
            var value = TestEnum.First;

            // Act
            var result = value.IsAnyOf(new TestEnum[] { });

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsAnyOf_NullArray_ThrowsArgumentNullException() {
            // Arrange
            var value = TestEnum.First;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => value.IsAnyOf(null));
        }
    }


    public class EnumUtilTests {
        [Test]
        public void Count_ReturnsCorrectNumberOfEnumValues() {
            int count = EnumUtil.Count<SampleEnum>();
            Assert.AreEqual(4, count);
        }

        [Test]
        public void GetFirst_ReturnsFirstEnumValue() {
            var first = EnumUtil.GetFirst<SampleEnum>();
            Assert.AreEqual(SampleEnum.First, first);
        }

        [Test]
        public void GetLast_ReturnsLastEnumValue() {
            var last = EnumUtil.GetLast<SampleEnum>();
            Assert.AreEqual(SampleEnum.Fourth, last);
        }

        [Test]
        public void TryGetNext_ReturnsCorrectNextEnumValue() {
            var result = EnumUtil.TryGetNext(SampleEnum.Second, out SampleEnum next);
            Assert.IsTrue(result);
            Assert.AreEqual(SampleEnum.Third, next);
        }

        [Test]
        public void TryGetNext_ReturnsFalseWhenAtEnd() {
            var result = EnumUtil.TryGetNext(SampleEnum.Fourth, out SampleEnum next);
            Assert.IsFalse(result);
            Assert.AreEqual(default(SampleEnum), next);
        }

        [Test]
        public void TryGetPrevious_ReturnsCorrectPreviousEnumValue() {
            var result = EnumUtil.TryGetPrevious(SampleEnum.Third, out SampleEnum previous);
            Assert.IsTrue(result);
            Assert.AreEqual(SampleEnum.Second, previous);
        }

        [Test]
        public void TryGetPrevious_ReturnsFalseAndLastEnumValueWhenAtStart() {
            var result = EnumUtil.TryGetPrevious(SampleEnum.First, out SampleEnum previous);
            Assert.IsFalse(result);
            Assert.AreEqual(SampleEnum.Fourth, previous);
        }

        [Test]
        public void GetRandom_ReturnsValidEnumValue() {
            var random = EnumUtil.GetRandom<SampleEnum>();
            Assert.IsTrue(Enum.IsDefined(typeof(SampleEnum), random));
        }


        [Test]
        public void KeyToType_ReturnsCorrectEnumValue() {
            var value = EnumUtil.KeyToType<SampleEnum>("Second");
            Assert.AreEqual(SampleEnum.Second, value);
        }

        [Test]
        public void KeyToType_ThrowsArgumentExceptionForInvalidKey() {
            Assert.Throws<ArgumentException>(() => EnumUtil.KeyToType<SampleEnum>("InvalidKey"));
        }

        [Test]
        public void NoToType_ReturnsCorrectEnumValue() {
            var value = EnumUtil.NoToType<SampleEnum>(2);
            Assert.AreEqual(SampleEnum.Third, value);
        }

        [Test]
        public void NoToType_ThrowsArgumentOutOfRangeExceptionForInvalidNumber() {
            Assert.Throws<ArgumentOutOfRangeException>(() => EnumUtil.NoToType<SampleEnum>(-1));
        }

        [Test]
        public void ContainsKey_ReturnsTrueForValidKey() {
            var contains = EnumUtil.ContainsKey<SampleEnum>("First");
            Assert.IsTrue(contains);
        }

        [Test]
        public void ContainsKey_ReturnsFalseForInvalidKey() {
            var contains = EnumUtil.ContainsKey<SampleEnum>("InvalidKey");
            Assert.IsFalse(contains);
        }

        [Test]
        public void IsFirst_ReturnsTrueForFirstEnumValue() {
            var isFirst = EnumUtil.IsFirst(SampleEnum.First);
            Assert.IsTrue(isFirst);
        }

        [Test]
        public void IsFirst_ReturnsFalseForNonFirstEnumValue() {
            var isFirst = EnumUtil.IsFirst(SampleEnum.Second);
            Assert.IsFalse(isFirst);
        }

        [Test]
        public void IsLast_ReturnsTrueForLastEnumValue() {
            var isLast = EnumUtil.IsLast(SampleEnum.Fourth);
            Assert.IsTrue(isLast);
        }

        [Test]
        public void IsLast_ReturnsFalseForNonLastEnumValue() {
            var isLast = EnumUtil.IsLast(SampleEnum.Third);
            Assert.IsFalse(isLast);
        }

        [Test]
        public void ForEach_ExecutesActionOnAllEnumValues() {
            var values = new List<SampleEnum>();
            EnumUtil.ForEach<SampleEnum>(value => values.Add(value));

            Assert.AreEqual(4, values.Count);
            Assert.Contains(SampleEnum.First, values);
            Assert.Contains(SampleEnum.Second, values);
            Assert.Contains(SampleEnum.Third, values);
            Assert.Contains(SampleEnum.Fourth, values);
        }
    }
}
