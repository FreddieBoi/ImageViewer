using System.Windows.Forms;

namespace ImageViewer
{

    public partial class OpenUrlDialog : Form
    {

        /// <summary>
        /// Constructor. Creates a new OpenUrlDialog.
        /// </summary>
        public OpenUrlDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the URL entered in this dialog.
        /// </summary>
        public string Url
        {
            get { return url.Text; }
        }

        private void OkButtonClick(object sender, System.EventArgs e)
        {
            // Set the DialogResult to OK
            DialogResult = DialogResult.OK;

            // Close the dialog.
            Close();
        }

        private void CancelButtonClick(object sender, System.EventArgs e)
        {
            // Set the DialogResult to Cancel
            DialogResult = DialogResult.Cancel;

            // Close the dialog.
            Close();
        }

    }

}
