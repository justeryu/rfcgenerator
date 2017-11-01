using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RfcGenerator.Winform
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
      try
      {
        RfcGenerator.Namespace = this.txtNamespace.Text;
        if (this.txtNamespace.Text == "")
        {
          RfcGenerator.Namespace = "Sap";
        }
        RfcGenerator.HelperNamespace = this.txtHelperNamespace.Text;
        if (this.txtRfcName.Text == "")
        {
          this.lstResults.Items.Add("Error: Rfc name can not be empty!");
        }
        else
        {
          RfcGenerator.GenerateFunction(this.txtRfcName.Text);
          this.lstResults.Items.Add("Rfc generated.");
        }
      }
      catch (Exception ex)
      {
        this.lstResults.Items.Add(ex.Message);
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.txtNamespace.Text = (ConfigurationSettings.AppSettings["RfcNamespace"] ?? this.txtNamespace.Text);
      this.txtHelperNamespace.Text = (ConfigurationSettings.AppSettings["HelperNamespace"] ?? this.txtHelperNamespace.Text);
    }
  }
}
