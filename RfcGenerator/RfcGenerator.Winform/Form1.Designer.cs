namespace RfcGenerator.Winform
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtHelperNamespace = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtNamespace = new System.Windows.Forms.TextBox();
      this.lstResults = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtRfcName = new System.Windows.Forms.TextBox();
      this.btnGenerate = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.AutoSize = true;
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.txtHelperNamespace);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.txtNamespace);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(628, 85);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Config";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 49);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(128, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Using Helper Namespace";
      // 
      // txtHelperNamespace
      // 
      this.txtHelperNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHelperNamespace.Location = new System.Drawing.Point(152, 46);
      this.txtHelperNamespace.Name = "txtHelperNamespace";
      this.txtHelperNamespace.Size = new System.Drawing.Size(470, 20);
      this.txtHelperNamespace.TabIndex = 5;
      this.txtHelperNamespace.Text = "Dakicksoft.RfcGenerator.Sap.Helpers";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(145, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Generated Class Namespace";
      // 
      // txtNamespace
      // 
      this.txtNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtNamespace.Location = new System.Drawing.Point(152, 13);
      this.txtNamespace.Name = "txtNamespace";
      this.txtNamespace.Size = new System.Drawing.Size(470, 20);
      this.txtNamespace.TabIndex = 3;
      this.txtNamespace.Text = "Sap";
      // 
      // lstResults
      // 
      this.lstResults.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lstResults.FormattingEnabled = true;
      this.lstResults.Location = new System.Drawing.Point(0, 214);
      this.lstResults.Name = "lstResults";
      this.lstResults.Size = new System.Drawing.Size(628, 173);
      this.lstResults.TabIndex = 11;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 96);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Rfc Name";
      // 
      // txtRfcName
      // 
      this.txtRfcName.Location = new System.Drawing.Point(9, 126);
      this.txtRfcName.Name = "txtRfcName";
      this.txtRfcName.Size = new System.Drawing.Size(360, 20);
      this.txtRfcName.TabIndex = 9;
      // 
      // btnGenerate
      // 
      this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnGenerate.Location = new System.Drawing.Point(9, 162);
      this.btnGenerate.Name = "btnGenerate";
      this.btnGenerate.Size = new System.Drawing.Size(125, 46);
      this.btnGenerate.TabIndex = 8;
      this.btnGenerate.Text = "Generate";
      this.btnGenerate.UseVisualStyleBackColor = true;
      this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(628, 387);
      this.Controls.Add(this.lstResults);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtRfcName);
      this.Controls.Add(this.btnGenerate);
      this.Controls.Add(this.groupBox1);
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Welcome to RFC Generator";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtHelperNamespace;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtNamespace;
    private System.Windows.Forms.ListBox lstResults;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtRfcName;
    private System.Windows.Forms.Button btnGenerate;
  }
}

