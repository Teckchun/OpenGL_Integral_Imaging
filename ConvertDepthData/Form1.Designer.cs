namespace ConvertDepthData
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenMappingImage = new System.Windows.Forms.Button();
            this.btnMappingDepth = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenMappingImage
            // 
            this.btnOpenMappingImage.Location = new System.Drawing.Point(12, 12);
            this.btnOpenMappingImage.Name = "btnOpenMappingImage";
            this.btnOpenMappingImage.Size = new System.Drawing.Size(99, 39);
            this.btnOpenMappingImage.TabIndex = 0;
            this.btnOpenMappingImage.Text = "Mapping Image";
            this.btnOpenMappingImage.UseVisualStyleBackColor = true;
            this.btnOpenMappingImage.Click += new System.EventHandler(this.btnOpenMappingImage_Click);
            // 
            // btnMappingDepth
            // 
            this.btnMappingDepth.Location = new System.Drawing.Point(117, 12);
            this.btnMappingDepth.Name = "btnMappingDepth";
            this.btnMappingDepth.Size = new System.Drawing.Size(99, 39);
            this.btnMappingDepth.TabIndex = 0;
            this.btnMappingDepth.Text = "Mapping Depth";
            this.btnMappingDepth.UseVisualStyleBackColor = true;
            this.btnMappingDepth.Click += new System.EventHandler(this.btnMappingDepth_Click);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(222, 12);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(99, 39);
            this.btnSaveData.TabIndex = 0;
            this.btnSaveData.Text = "Save Data";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(435, 441);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 510);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.btnMappingDepth);
            this.Controls.Add(this.btnOpenMappingImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenMappingImage;
        private System.Windows.Forms.Button btnMappingDepth;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

