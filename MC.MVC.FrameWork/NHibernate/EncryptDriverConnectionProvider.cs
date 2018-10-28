using MC.MVC.Framework.Encrypt;
using NHibernate.Cfg;
using NHibernate.Connection;
using System.Collections.Generic;

namespace MC.MVC.Framework.Data.NHibernate
{
    public class EncryptDriverConnectionProvider : DriverConnectionProvider
    {
        public EncryptDriverConnectionProvider()
            : base()
        {

        }

        public override void Configure(IDictionary<string, string> settings)
        {
            string conn = settings[Environment.ConnectionString];
            DesEncryptor des = new DesEncryptor();
            settings[Environment.ConnectionString] = des.Decrypt(conn);
            base.Configure(settings);
        }
    }
}
