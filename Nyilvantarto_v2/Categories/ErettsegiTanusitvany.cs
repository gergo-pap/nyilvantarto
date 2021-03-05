using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nyilvantarto_v2.Categories
{
    class ErettsegiTanusitvany : Category
    {
        public override string relativePath => @"\Adatok\Érettségi\Tanusítvány";
        public override string categoryPrettyName => "Érettségi tanusítvány";
        public override string sqlTableName => "erettsegitanusitvany";
        public override string row1Spec => "vizsgaEvVeg";
        public override string row2Spec => "tanuloiazonosito";
        public override Panel panelFeltolt { get; set; }
        public override TextBox textBoxFileNameFeltolt { get; set; }
        public override Button button { get; set; }

        protected override List<string> customSqlColumns => new List<string>
        {
            "Vizsga éve",
            "Tanulói azonosító"
        };

    }
}
