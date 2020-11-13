using NHibernate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Helpers
{
    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Debug.WriteLine(sql.ToString());
            return sql;
        }
    }
}
