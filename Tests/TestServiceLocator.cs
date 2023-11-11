using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Reko3d;

namespace Reko3d.Tests
{
    public class TestServiceLocator
    {
        private interface IService { }
        private class ServiceA : IService { }
        private class ServiceB : IService { }

        private ServiceA serviceA;
        private ServiceB serviceB;
        
        [SetUp]
        public void SetUp()
        {
            serviceA = new ServiceA();
            serviceB = new ServiceB();
        }

        [TearDown]
        public void TearDown()
        {
            IOC.Clear();
        }
        

        [Test]
        public void TestRegister()
        {
            IOC.Register<IService>(serviceA);
            Assert.AreEqual(serviceA, IOC.Get<IService>());
            IService service;
            Assert.IsTrue(IOC.TryGet<IService>(out service));
            Assert.AreEqual(serviceA, service);
        }

        [Test]
        public void TestUnregister()
        {
            IOC.Register<IService>(serviceA);
            Assert.AreEqual(serviceA, IOC.Get<IService>());
            IOC.Unregister<IService>(serviceA);
            Assert.IsNull(IOC.Get<IService>());
            Assert.IsFalse(IOC.TryGet<IService>(out _));
        }
        
        [Test]
        public void TestRegisterNewService()
        {
            IOC.Register<IService>(serviceA);
            Assert.AreEqual(serviceA, IOC.Get<IService>());
            IOC.Register<IService>(serviceB);
            Assert.AreEqual(serviceB, IOC.Get<IService>());
        }
        
        [Test]
        public void TestDifferentServices()
        {
            IOC.Register<ServiceA>(serviceA);
            IOC.Register<ServiceB>(serviceB);
            Assert.AreEqual(serviceA, IOC.Get<ServiceA>());
            Assert.AreEqual(serviceB, IOC.Get<ServiceB>());
            Assert.IsNull(IOC.Get<IService>());
        }
        
        
        [Test]
        public void TestUnregisterFalseService()
        {
            IOC.Register<IService>(serviceA);
            Assert.AreEqual(serviceA, IOC.Get<IService>());
            IOC.Unregister<IService>(serviceB);
            Assert.AreEqual(serviceA, IOC.Get<IService>());
        }
        
        [Test]
        public void TestClear()
        {
            IOC.Register<IService>(serviceA);
            Assert.AreEqual(serviceA, IOC.Get<IService>());
            IOC.Clear();
            Assert.IsNull(IOC.Get<IService>());
        }
    }
}
