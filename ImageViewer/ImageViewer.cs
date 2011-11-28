using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImageViewer
{

    public partial class ImageViewer : Form
    {

        /// <summary>
        /// The dialog used when opening images from URL.
        /// </summary>
        private OpenUrlDialog _openUrlDialog;

        /// <summary>
        /// Constructor. Creates a new ImageViewer form.
        /// </summary>
        public ImageViewer()
        {
            InitializeComponent();

            // Hide the progressbar from start
            progressBar.Visible = false;
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Show the file dialog and open the file only if successful.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                LoadAsync(openFileDialog.FileName);
        }

        private void OpenFromUrlToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Create a dialog to open URLs.
            _openUrlDialog = new OpenUrlDialog();

            // Show the URL dialog and open the file only if successful.
            if (_openUrlDialog.ShowDialog() == DialogResult.OK)
                LoadAsync(_openUrlDialog.Url);
        }

        private void StretchImageToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Show or Hide scrollbars depending on SizeMode.
            UpdateScrollBars();

            pictureBox.SizeMode = stretchImageToolStripMenuItem.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Normal;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BackgroundToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Show the URL dialog and switch the background color if successful.
            if (colorDialog.ShowDialog() == DialogResult.OK)
                pictureBox.BackColor = colorDialog.Color;
        }

        private void CloseCurrentImageToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Clear the image.
            pictureBox.Image = null;

            // Update the status label
            statusLabel.Text = @"Welcome to ImageViewer by Freddie Pettersson!";

            // Show or Hide scrollbars depending on SizeMode.
            UpdateScrollBars();
        }

        private void PictureBoxLoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void PictureBoxLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Show or Hide scrollbars depending on SizeMode.
            UpdateScrollBars();

            // Hide the progressbar again
            progressBar.Visible = false;

            // Check if something actually loaded.
            if (pictureBox.Image == pictureBox.ErrorImage)
            {
                statusLabel.Text = @"Failed to load image! Bad URL.";
                return;
            }
            statusLabel.Text = @"Image loaded: " + pictureBox.ImageLocation;
        }

        /// <summary>
        /// Show or Hide scrollbars depending on the SizeMode.
        /// </summary>
        private void UpdateScrollBars()
        {
            if (!stretchImageToolStripMenuItem.Checked && pictureBox.Image != null)
            {
                tableLayoutPanel2.AutoScroll = true;
                tableLayoutPanel2.AutoScrollMinSize = pictureBox.Image.Size;
            }
            else
            {
                tableLayoutPanel2.AutoScroll = false;
                tableLayoutPanel2.AutoScrollMinSize = new Size(0, 0);
            }
        }

        /// <summary>
        /// Load the specified <code>image</code> into the ImageViewer.
        /// </summary>
        /// <param name="image">The path or URL to the image file to load.</param>
        private void LoadAsync(string image)
        {
            // Update the status label.
            statusLabel.Text = @"Loading image...";

            // Reset progress percentage
            progressBar.Value = 0;

            // Show the progressbar
            progressBar.Visible = true;

            // Load the image from URL asyncronously
            try
            {
                pictureBox.LoadAsync(image);
            }
            catch (Exception exception)
            {
                // Reset progress percentage
                progressBar.Value = 0;

                // Show the progressbar
                progressBar.Visible = false;

                // Update the status label.
                statusLabel.Text = @"Failed to load image! " + exception.Message;
            }
        }

    }
}
