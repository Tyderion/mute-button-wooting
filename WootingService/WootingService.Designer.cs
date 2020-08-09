namespace WootingService
{
    partial class WootingService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.log = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.log)).BeginInit();
            // 
            // log
            // 
            this.log.Source = "WootingService";
            this.log.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.eventLog1_EntryWritten_1);
            // 
            // WootingService
            // 
            this.ServiceName = "WootingService";
            ((System.ComponentModel.ISupportInitialize)(this.log)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog log;
    }
}
