using DevFramework.Northwind.Business.Concrete.Managers;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace DevFramework.Northwind.Business.Tests
{
    [TestClass]
    public class ProductManagerTest
    {
        [ExpectedException(typeof(ValidationException))] 
        [TestMethod]
        public void Product_validation_check()
        {
            Mock<IProductDal> mock = new Mock<IProductDal>(); //Test için IProductDal'ın concreteine ihtiyacımız var, Mock toolu bunu sağlıyor bize
            ProductManager productManager = new ProductManager(mock.Object);

            productManager.Add(new Product()); //Kurallara aykırı olarak her şeyi boş gönderdik
        }
    }
}
