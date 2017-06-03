using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GramaticApp.Resources;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace GramaticApp.Pages
{
    public partial class GramaticApp : System.Web.UI.Page
    {
        string ini = "INICIO";
        string fin = "FIN";
        string decl = @"(DECLARE)[\s]+[A-Z][0-9A-Z_]*((([\s]*)?[,]([\s]*)?[A-Z][0-9A-Z_]*)+)?[\s]+(CADENA|ENTERO|REAL|FECHA|LOGICO)([\s]*)?[;]";
        string asign = @"^[A-Z][0-9A-Z_]*[\s]*[=][\s]*[0-9A-Z_+/\-\s*()]*([\s]*)?[;]$";
        string pattern1 = @"^[A-Z0-9_]+[\s]+[0-9A-Z_,]+[\s]+([A-Z]+)?[\s]*[;]?";
        string pattern2 = @"^[A-Z0-9_]+[\s]*[=][\s]*[A-Z0-9-+*/()\s]+[\s]*[;]?";

        List<string> listDecl = new List<string>();
        List<string> listAsign = new List<string>();
        List<string> listOtro = new List<string>();
        List<string> listReserv = new List<string>();
        List<string> listVar = new List<string>();
        List<string> listVarAsig = new List<string>();

        List<int> linDecl = new List<int>();
        List<int> linAsig = new List<int>();
        List<int> linRes = new List<int>();
        List<int> linOtro = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCodigo.Attributes.Add("placeholder", "Ingresar código...");
                lblResultado.Text = "Resultado...";
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = String.Empty;
            txtCodigo.Attributes.Add("placeholder", "Ingresar código...");
            lblResultado.Text = "Resultado...";
        }

        protected void btnCompilar_Click(object sender, EventArgs e)
        {
            listDecl.Clear();
            listAsign.Clear();
            listOtro.Clear();
            listReserv.Clear();
            listVar.Clear();
            listVarAsig.Clear();

            linDecl.Clear();
            linAsig.Clear();
            linRes.Clear();
            linOtro.Clear();

            Regex Exp1 = new Regex(decl);
            Regex Exp2 = new Regex(asign);
            Regex Exp3 = new Regex(pattern1);
            Regex Exp4 = new Regex(pattern2);

            string text = txtCodigo.Text.ToUpper();
            List<string> list = new List<string>(text.Split(new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries));

            int cont = 0;
            foreach (var item in list)
            {
                cont++;
                if (Exp1.IsMatch(item)) {
                    linDecl.Add(cont);
                    listDecl.Add(item);
                } else if (Exp2.IsMatch(item)) {
                    linAsig.Add(cont);
                    listAsign.Add(item);
                } else if (item.Equals(ini) || item.Equals(fin)) {
                    linRes.Add(cont);
                    listReserv.Add(item);
                } else {
                    linOtro.Add(cont);
                    listOtro.Add(item);
                }
            }

            if (!ini.Equals(list.First())) {
                lblResultado.Text = "No se encontro la palabra reservada <big><b>INICIO</b></big> al iniciar el pseudocódigo.";
            } else if (!fin.Equals(list.Last())){
                lblResultado.Text = "No se encontro la palabra reservada <big><b>FIN</b></big> al final del pseudocódigo.";
            } else if (listOtro.Count() == 0) {
                foreach (var item in listDecl)
                {
                    string str1 = item.Substring(8);
                    string str2 = str1.Substring(0,str1.IndexOf("\u0020"));
                    string[] var = str2.Split(',');
                    for (int i = 0; i < var.Length; i++)
                    {
                        listVar.Add(var[i]);
                    }
                }

                string[] array = new string[listVar.Count];
                listVar.CopyTo(array);
                string res = "";
                foreach (var grouping in array.GroupBy(t => t).Where(t => t.Count() != 1))
                {
                    string valVar1 = string.Format("{0}", grouping.Key, grouping.Count());
                    string valVar2 = string.Format("{1}", grouping.Key, grouping.Count());
                    res = "La variable <big><b>"+valVar1.ToLower()+"</b></big> está declarado "+valVar2+" veces.";
                }

                if (res == string.Empty)
                {
                    foreach (var item in listAsign)
                    {
                        string str1 = item.Substring(0, item.IndexOf("=")).Trim();
                        listVarAsig.Add(str1);

                        string str2 = item.Substring(item.IndexOf("=") + 1);
                        string str3 = str2.Replace('+', ',').Replace('-', ',').Replace('*', ',').Replace('/', ',')
                            .Replace('(', ',').Replace(')', ',').Replace(';', ',');
                        string[] var = str3.Split(',');
                        List<string> listVal = new List<string>();
                        for (int i = 0; i < var.Length; i++)
                        {
                            if (var[i] != string.Empty && var[i] != " ")
                            {
                                listVal.Add(var[i]);
                            }
                        }

                        for (int i = 0; i < listVal.Count; i++)
                        {
                            Regex format = new Regex(@"[A-Z]");
                            if (format.IsMatch(listVal.ElementAt(i)))
                            {
                                listVarAsig.Add(listVal.ElementAt(i));
                            }
                        }
                    }

                    lblResultado.Text = "Compilación Exitosa...<br><br><big><b>OK.</b></big>";
                } else {
                    lblResultado.Text = res.ToString();
                }
            } else if (listOtro.Count() != 0) { 
                if (Exp3.IsMatch(listOtro.ElementAt(0))) {
                    Regex format1 = new Regex(@"^(DECLARE)[\s]+");
                    Regex format2 = new Regex(@"[\s]+[A-Z][0-9A-Z_]*((([\s]*)?[,]([\s]*)?[A-Z][0-9A-Z_]*)+)?[\s]+");
                    Regex format3 = new Regex(@"[\s]+(CADENA|ENTERO|REAL|FECHA|LOGICO)([\s]*)?");
                    if (!format1.IsMatch(listOtro.ElementAt(0))) {
                        lblResultado.Text = "No se encontro la palabra DECLARE en la línea " +
                            linOtro.ElementAt(0) + ".";
                    } else if (!format2.IsMatch(listOtro.ElementAt(0))) {
                        lblResultado.Text = "El formato de nombre o lista de variable(s) es incorrecto en la línea " + linOtro.ElementAt(0) + ".";
                    } else if (!format3.IsMatch(listOtro.ElementAt(0))) {
                        lblResultado.Text = "El tipo de dato es invalido en la línea " + linOtro.ElementAt(0) + ".";
                    } else {
                        lblResultado.Text = "Falto <big><b>;</b></big> en la declaración de la variable en la línea " + linOtro.ElementAt(0) + ".";
                    }
                } else if (Exp4.IsMatch(listOtro.ElementAt(0))) {
                    Regex format1 = new Regex(@"^[A-Z][0-9A-Z_]*[\s]*[=]");
                    Regex format2 = new Regex(@"[{}\\|<>'@#¡!¿?^&$%]+");
                    Regex format3 = new Regex(@"[=][\s]*[0-9A-Z_+/\-\s*()]+([\s]*)?");
                    if (!format1.IsMatch(listOtro.ElementAt(0))) {
                        lblResultado.Text = "El nombre de la variable es invalido en la línea " + linOtro.ElementAt(0) + ".";
                    } else if (format2.IsMatch(listOtro.ElementAt(0))) {
                        lblResultado.Text = "La Expresión a asignar posee un caracter invalido en la línea " + linOtro.ElementAt(0) + ".";
                    } else if (!format3.IsMatch(listOtro.ElementAt(0))) {
                        lblResultado.Text = "La Expresión a asignar posee un formato invalido en la línea " + linOtro.ElementAt(0) + ".";
                    } else {
                        lblResultado.Text = "Falto <big><b>;</b></big> en la asignación de la variable en la línea " + linOtro.ElementAt(0) + ".";
                    }
                } else {
                    lblResultado.Text = "Error de sintaxis en la línea "+linOtro.ElementAt(0)+".";
                }  
            } else {
                Mensaje("Excepción no identificada.");
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            Mensaje(this.lblResultado.Text);
        }

        protected void Mensaje(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }
    }
}