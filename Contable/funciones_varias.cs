using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;

namespace Contable
{
    public class funciones_varias
    {

        public void PrepararForm(Form F)
        {
            F.BackColor = Color.LightGray;
            F.Font = new Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            foreach (Control ctrl in F.Controls)
            {
                if (ctrl is Label)
                {
                    ctrl.ForeColor = Color.Black;
                    ctrl.BackColor = Color.LightGray;
                }
                if (ctrl is ListBox )
                {
                    ctrl.BackColor = Color.White;
                }
                if (ctrl is TextBox || ctrl is ComboBox  )
                {
                    ctrl.ForeColor = Color.Black;
                    ctrl.BackColor = Color.White;
                }
                if (ctrl is Button)
                {
                    ctrl.ForeColor = Color.Black;
                    ctrl.BackColor = Color.SteelBlue ;
                }
                //F.BackColor = Color.LightGray;
            }

        }
        public bool leerLicencia()
        {
            // Cargo la Ruta
            string linea = string.Empty;
            string Path;
            string FechaLic;

            encrypasocv.Encryption e = new encrypasocv.Encryption();

            Path = System.Windows.Forms.Application.StartupPath.ToString() + "\\cls99.lic";

            // verifico existencia del archivo
            if (File.Exists(Path) == false)
                return false;

            // Leo el archivo
            StreamReader files = new StreamReader(Path);
            while (!files.EndOfStream)
                linea = files.ReadLine();
            files.Close();
            // verifico
            if (linea == string.Empty)
                return false;

            FechaLic = e.Decrypt(linea);
            // comparo las fechas
            var dFechaLic = Convert.ToDateTime(FechaLic);
            var dFechaHoy = DateTime.Today.Date;
            if (dFechaHoy > dFechaLic)
            {
                File.Delete(Path);
                return false;
            }
            return true;
        }
    }
}
