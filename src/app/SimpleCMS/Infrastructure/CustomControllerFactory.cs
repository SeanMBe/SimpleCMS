using System;
using System.Web.Mvc;
using StructureMap;

namespace SimpleCMS.Infrastructure
{
    public class CustomControllerFactory : DefaultControllerFactory {
        protected IController GetControllerInstance(Type controllerType) {
            if (controllerType == null)
                throw new ArgumentNullException("controllerType");

            return ObjectFactory.GetInstance(controllerType) as Controller;
        }
    }
}