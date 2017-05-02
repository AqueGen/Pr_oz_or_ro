using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    public class TendersServiceTestsEx<T>
        where T : IService
    {
        public static void Init<U>(Tender tender)
            where U : class, IService
        {
            using (var service = new TendersService())
            {
                var created = service.CreateTender(TendersServiceTests.ValidTender).Result;
                Tender = created.Data;
                Token = created.Token;
                Service = (T)Activator.CreateInstance(typeof(U), created.Data.Guid, null);
            }
        }

        protected static Tender Tender { get; set; }

        protected static string Token { get; set; }

        protected static T Service { get; set; }
    }
}