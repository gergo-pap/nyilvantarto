using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyilvantarto_v2.Categories
{
    class SzakmaiVizsgaTorzslap : Category
    {
        public override string relativePath => @"\Adatok\Szakmai vizsga\Törzslap";
        public override string categoryPrettyName => "Szakmaivizsga törzslap";
        public override string sqlTableName => "szakmaivizsgatorzslap";
        public override string row1Spec => "vizsgaEvVeg";
        public override string row2Spec => "vizsgaTavasz1Osz0";
        public override Panel panelFeltolt { get; set; }
        public override TextBox textBoxFileNameFeltolt { get; set; }

        protected override List<string> customSqlColumns => new List<string>
        {
            "Vizsga éve",
            "Vizsga időszaka"
        };
    }
}
