using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HttpConnect.HttpUtil;

namespace HttpConnect.Forms
{
    public partial class HTTPserver : Form
    {
        HttpIntercept httpListener = new HttpIntercept();

        public HTTPserver()
        {
            InitializeComponent();
        }
        
        
        private void HTTPserver_Activated(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void HTTPserver_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += "   " + UserUtil.UtilMethods.GetVersionStr();
                MainThreed();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// サブスレッドにて受信監視
        /// </summary>
        private async void MainThreed()
        {
            httpListener.OnReceiveAPI += Util_OnReceiveAPI;
            httpListener.OnFinishAPI += HttpListener_OnFinishAPI;
            if (httpListener.Initialize())
            {
                await Task.Run(() => httpListener.StartReceive());
            }
            else
            {
                MessageBox.Show("Failed to listen HTTP. Please restart.");
            }
        }

        private void HttpListener_OnFinishAPI(EventArg.FinishAPIEventArg arg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<EventArg.FinishAPIEventArg>(HttpListener_OnFinishAPI), arg);
                return;
            }
            txtResponse.Text = arg.ResponseData.ResultMessage;
            txtHttpResp.Text = arg.ResponseData.ResultStatusCode.ToString();
        }

        private void Util_OnReceiveAPI(EventArg.ReceiveAPIEventArg arg)
        {
            if(InvokeRequired)
            {
                BeginInvoke(new Action<EventArg.ReceiveAPIEventArg>(Util_OnReceiveAPI), arg);
                return;
            }
            txtAPIName.Text = arg.ReceivedData.APIName;
            txtClient.Text = arg.ReceivedData.ClientIPAddress;
            txtQuery.Text = string.Join(",", arg.ReceivedData.GetQueryArgList());
        }

        private void HTTPserver_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Exit?", "confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    httpListener.EndProc();
                }
            }
            else
            {
                httpListener.EndProc();
            }
        }


    }
}
