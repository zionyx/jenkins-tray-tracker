using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jenkins.TrayTracker.Entities;
using Jenkins.TrayTracker.BusinessComponents;
using Spring.Context.Support;

namespace Jenkins.TrayTracker.UI
{
    public partial class ClaimBuildForm : DevExpress.XtraEditors.XtraForm
    {
        ClaimService claimService;

        Project project;
        BuildDetails buildDetails;

        public ClaimBuildForm()
        {
            InitializeComponent();

            claimService = (ClaimService)ContextRegistry.GetContext().GetObject("ClaimService");
        }

        public void Initialize(Project project, BuildDetails buildDetails)
        {
            this.project = project;
            this.buildDetails = buildDetails;

            buildNumberLabel.Text = buildDetails.Number.ToString();
            reasonMemoEdit.Select();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string reason = reasonMemoEdit.Text;
            bool sticky = stickyCheckEdit.Checked;
            claimService.ClaimBuild(project, buildDetails, reason, sticky);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}