using System;
using System.Web;
using SimpleCMS.Core.Data;
using StructureMap;

namespace SimpleCMS.Infrastructure
{
    public class UnitOfWorkModule : IHttpModule {
        private IUnitOfWork unitOfWork;

        public void Init(HttpApplication context) {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;
        }

        private void ContextBeginRequest(object sender, EventArgs e) {
            unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
        }

        private void ContextEndRequest(object sender, EventArgs e) {
            Dispose();
        }

        public void Dispose() {
            unitOfWork.Dispose();
        }
    }
}