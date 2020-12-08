using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyilvantarto_v2.Categories
{
    class KozepiskolaiAnyakonyv : Category
    {
        protected override string relativePath => @"\Adatok\Középiskola\Anyakönyv";
        protected override string sqlTableName => "kozepiskolaanyakonyv";
        protected override List<string> customSqlColumns => new List<string>
        {
             "Középiskola kezdete",
             "Érettségi éve"
        };
    }
}
