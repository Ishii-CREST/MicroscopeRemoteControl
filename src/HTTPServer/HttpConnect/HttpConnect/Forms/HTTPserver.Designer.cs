namespace HttpConnect.Forms
{
    partial class HTTPserver
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLastReceived = new System.Windows.Forms.Label();
            this.txtAPIName = new System.Windows.Forms.TextBox();
            this.lblParams = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.lblClient = new System.Windows.Forms.Label();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.lblSend = new System.Windows.Forms.Label();
            this.txtHttpResp = new System.Windows.Forms.TextBox();
            this.lblHttpResp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLastReceived
            // 
            this.lblLastReceived.AutoSize = true;
            this.lblLastReceived.Location = new System.Drawing.Point(22, 15);
            this.lblLastReceived.Name = "lblLastReceived";
            this.lblLastReceived.Size = new System.Drawing.Size(88, 12);
            this.lblLastReceived.TabIndex = 0;
            this.lblLastReceived.Text = "LastReceived　：";
            // 
            // txtAPIName
            // 
            this.txtAPIName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAPIName.Location = new System.Drawing.Point(125, 12);
            this.txtAPIName.Name = "txtAPIName";
            this.txtAPIName.ReadOnly = true;
            this.txtAPIName.Size = new System.Drawing.Size(258, 19);
            this.txtAPIName.TabIndex = 1;
            // 
            // lblParams
            // 
            this.lblParams.AutoSize = true;
            this.lblParams.Location = new System.Drawing.Point(53, 65);
            this.lblParams.Name = "lblParams";
            this.lblParams.Size = new System.Drawing.Size(57, 12);
            this.lblParams.TabIndex = 2;
            this.lblParams.Text = "Params　：";
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(125, 62);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ReadOnly = true;
            this.txtQuery.Size = new System.Drawing.Size(258, 19);
            this.txtQuery.TabIndex = 3;
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(61, 41);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(49, 12);
            this.lblClient.TabIndex = 4;
            this.lblClient.Text = "Client　：";
            // 
            // txtClient
            // 
            this.txtClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClient.Location = new System.Drawing.Point(125, 37);
            this.txtClient.Name = "txtClient";
            this.txtClient.ReadOnly = true;
            this.txtClient.Size = new System.Drawing.Size(258, 19);
            this.txtClient.TabIndex = 5;
            // 
            // txtResponse
            // 
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.Location = new System.Drawing.Point(125, 87);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ReadOnly = true;
            this.txtResponse.Size = new System.Drawing.Size(258, 19);
            this.txtResponse.TabIndex = 8;
            // 
            // lblSend
            // 
            this.lblSend.AutoSize = true;
            this.lblSend.Location = new System.Drawing.Point(66, 90);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(44, 12);
            this.lblSend.TabIndex = 7;
            this.lblSend.Text = "Send　：";
            // 
            // txtHttpResp
            // 
            this.txtHttpResp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHttpResp.Location = new System.Drawing.Point(125, 112);
            this.txtHttpResp.Name = "txtHttpResp";
            this.txtHttpResp.ReadOnly = true;
            this.txtHttpResp.Size = new System.Drawing.Size(258, 19);
            this.txtHttpResp.TabIndex = 10;
            // 
            // lblHttpResp
            // 
            this.lblHttpResp.AutoSize = true;
            this.lblHttpResp.Location = new System.Drawing.Point(8, 114);
            this.lblHttpResp.Name = "lblHttpResp";
            this.lblHttpResp.Size = new System.Drawing.Size(102, 12);
            this.lblHttpResp.TabIndex = 9;
            this.lblHttpResp.Text = "HTTP Response　：";
            // 
            // HTTPserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 146);
            this.Controls.Add(this.txtHttpResp);
            this.Controls.Add(this.lblHttpResp);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.lblSend);
            this.Controls.Add(this.txtClient);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.lblParams);
            this.Controls.Add(this.txtAPIName);
            this.Controls.Add(this.lblLastReceived);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HTTPserver";
            this.ShowIcon = false;
            this.Text = "HTTP RemoteAgent";
            this.Activated += new System.EventHandler(this.HTTPserver_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HTTPserver_FormClosing);
            this.Load += new System.EventHandler(this.HTTPserver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLastReceived;
        private System.Windows.Forms.TextBox txtAPIName;
        private System.Windows.Forms.Label lblParams;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.TextBox txtHttpResp;
        private System.Windows.Forms.Label lblHttpResp;
    }
}

