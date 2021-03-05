using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyilvantarto_v2.Categories
{
    class KozepiskolaiAnyakonyv : Category
    {
        public override string relativePath => @"\Adatok\Középiskola\Anyakönyv";
        public override string categoryPrettyName => "Középiskola anyakönyv";
        public override string sqlTableName => "kozepiskolaanyakonyv";
        public override string row1Spec => "vizsgaEvKezdet";
        public override string row2Spec => "vizsgaEvVeg";
        public override Panel panelFeltolt { get; set; }
        public override TextBox textBoxFileNameFeltolt { get; set; }

        public override Button button { get; set; }
        protected override List<string> customSqlColumns => new List<string>
        {
             "Középiskola kezdete",
             "Érettségi éve"
        };
    }
}
