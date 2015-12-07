//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Diagnostics.CodeAnalysis;

//namespace RAMLSharp.Test
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class TypeExtensionsTest
//    {
//        #region ToRamlType
//        #region integer tests
//        [TestMethod]
//        public void TypeExtensions_Int32_ReturnsInteger()
//        {
//            Assert.AreEqual(typeof(Int32).ToRamlType(), "integer", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_SByte_ReturnsInteger()
//        {
//            Assert.AreEqual(typeof(SByte).ToRamlType(), "integer", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_UInt16_ReturnsInteger()
//        {
//            Assert.AreEqual(typeof(UInt16).ToRamlType(), "integer", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_UInt32_ReturnsInteger()
//        {
//            Assert.AreEqual(typeof(UInt32).ToRamlType(), "integer", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_UInt64_ReturnsInteger()
//        {
//            Assert.AreEqual(typeof(UInt64).ToRamlType(), "integer", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_Int16_ReturnsInteger()
//        {
//            Assert.AreEqual(typeof(Int16).ToRamlType(), "integer", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_Int64_ReturnsInteger()
//        {
//            Assert.AreEqual(typeof(Int64).ToRamlType(), "integer", "The return string should be 'integer'.");
//        }
//        #endregion

//        #region number tests
//        [TestMethod]
//        public void TypeExtensions_Decimal_ReturnsNumber()
//        {
//            Assert.AreEqual(typeof(Decimal).ToRamlType(), "number", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_Double_ReturnsNumber()
//        {
//            Assert.AreEqual(typeof(Double).ToRamlType(), "number", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_Single_ReturnsNumber()
//        {
//            Assert.AreEqual(typeof(Single).ToRamlType(), "number", "The return string should be 'integer'.");
//        }
//        #endregion

//        #region boolean tests
//        [TestMethod]
//        public void TypeExtensions_Boolean_ReturnsBoolean()
//        {
//            Assert.AreEqual(typeof(Boolean).ToRamlType(), "boolean", "The return string should be 'integer'.");
//        }
//        #endregion

//        #region date tests
//        [TestMethod]
//        public void TypeExtensions_DateTime_ReturnsDate()
//        {
//            Assert.AreEqual(typeof(DateTime).ToRamlType(), "date", "The return string should be 'integer'.");
//        }
//        #endregion

//        #region string tests
//        [TestMethod]
//        public void TypeExtensions_string_ReturnsString()
//        {
//            Assert.AreEqual(typeof(string).ToRamlType(), "string", "The return string should be 'integer'.");
//        }

//        [TestMethod]
//        public void TypeExtensions_object_ReturnsString()
//        {
//            Assert.AreEqual(typeof(object).ToRamlType(), "string", "The return string should be 'integer'.");
//        }
//        #endregion
//        #endregion

//        #region IsComplexModel

//        [TestMethod]
//        public void IsComplexModel_string_ReturnsFalse()
//        {
//            Assert.AreEqual(typeof(string).IsComplexModel(), false, "The object is a primitive.");
//        }
        
//        [TestMethod]
//        public void IsComplexModel_TypeExtensionsTest_ReturnsTrue()
//        {
//            Assert.AreEqual(typeof(TypeExtensionsTest).IsComplexModel(), true, "The object is a primitive.");
//        }

//        [TestMethod]
//        public void IsComplexModel_NullableInt_ReturnsFalse()
//        {
//            Type value = typeof(Nullable<int>);
//            Assert.AreEqual( value.IsComplexModel(), false, "The object is a primitive.");
//        }

//        [TestMethod]
//        public void IsComplexModel_NullableLong_ReturnsFalse()
//        {
//            Type value = typeof(Nullable<long>);
//            Assert.AreEqual(value.IsComplexModel(), false, "The object is a primitive.");
//        }

//        [TestMethod]
//        public void IsComplexModel_NullableDateTime_ReturnsFalse()
//        {
//            Type value = typeof(Nullable<DateTime>);
//            Assert.AreEqual(value.IsComplexModel(), false, "The object is a primitive.");
//        }

//        [TestMethod]
//        public void IsComplexModel_NullableBool_ReturnsFalse()
//        {
//            Type value = typeof(Nullable<bool>);
//            Assert.AreEqual(value.IsComplexModel(), false, "The object is a primitive.");
//        }

//        [TestMethod]
//        public void IsComplexModel_NullableDecimal_ReturnsFalse()
//        {
//            Type value = typeof(Nullable<decimal>);
//            Assert.AreEqual(value.IsComplexModel(), false, "The object is a primitive.");
//        }

//        #endregion
//    }
//}
