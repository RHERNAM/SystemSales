using AccesDLL;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Clases.Validaciones
{
    public class Repositorios
    {
        public static string[] PermisoUsuario { get; set; }

        public static bool ValidarPermiso(string Permiso)
        {
            string[] Validacion = Permiso.Split(',');
            foreach (var item in Validacion)
            {
                if (item!="" && PermisoUsuario.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }


        public static void ImprimirDirecto(ReportDocument reportDocument)
        {
            String PrintName = "";
            if (PrintName==string.Empty)
            {
                PrintDocument printDocument = new PrintDocument();
                PrintName = printDocument.PrinterSettings.PrinterName;
            }

            reportDocument.PrintOptions.PrinterName = PrintName;
            reportDocument.PrintToPrinter(1, false, 0, 0);
            Soporte.MsgInformacion("Se ha enviado el Ticket a Imprimir");

        }
    }
}
