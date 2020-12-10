
namespace HamsterMall
{
    partial class HamsterMall
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.loadButton = new System.Windows.Forms.Button();
            this.meshFileText = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.Ambient = new System.Windows.Forms.PictureBox();
            this.Background = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Ambient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(13, 13);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(112, 23);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load Mesh";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // meshFileText
            // 
            this.meshFileText.AutoSize = true;
            this.meshFileText.Location = new System.Drawing.Point(131, 18);
            this.meshFileText.Name = "meshFileText";
            this.meshFileText.Size = new System.Drawing.Size(0, 13);
            this.meshFileText.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 209);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(112, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Export MeshWorld";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "MESHWORLD";
            // 
            // Ambient
            // 
            this.Ambient.BackColor = System.Drawing.Color.White;
            this.Ambient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ambient.Location = new System.Drawing.Point(107, 42);
            this.Ambient.Name = "Ambient";
            this.Ambient.Size = new System.Drawing.Size(32, 32);
            this.Ambient.TabIndex = 3;
            this.Ambient.TabStop = false;
            this.Ambient.Click += new System.EventHandler(this.Ambient_Click);
            // 
            // Background
            // 
            this.Background.BackColor = System.Drawing.Color.Blue;
            this.Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Background.Location = new System.Drawing.Point(107, 80);
            this.Background.Name = "Background";
            this.Background.Size = new System.Drawing.Size(32, 32);
            this.Background.TabIndex = 4;
            this.Background.TabStop = false;
            this.Background.Click += new System.EventHandler(this.Background_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ambient Color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Background Color:";
            // 
            // HamsterMall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 244);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Background);
            this.Controls.Add(this.Ambient);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.meshFileText);
            this.Controls.Add(this.loadButton);
            this.Name = "HamsterMall";
            this.Text = "HamsterMall";
            ((System.ComponentModel.ISupportInitialize)(this.Ambient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Background)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Label meshFileText;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox Ambient;
        private System.Windows.Forms.PictureBox Background;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

