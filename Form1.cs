using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Reflection.Emit;

namespace CW_OOP_Test1
{
    public partial class Form1 : Form
    {
        #region Variable
        MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_TRIPLEX, 0.6d, 0.6d);
        HaarCascade faceDetected;
        
        Image<Bgr, byte> Frame;
        //Image<Bgr, byte> origImage;
        
        Capture camera;
        
        Image<Gray, byte> result;
        Image<Gray, byte> TrainedFace = null;
        Image<Gray, byte> grayFace = null;

        List<Image<Bgr, byte>> origImages = new List<Image<Bgr, byte>>();
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labelsNames = new List<string>();
        List<string> labelsImages = new List<string>();
        List<string> Users = new List<string>();
        
        int Count, NumLables, t;
        string name, names = null;
        string label = null;

        private SqlConnection sqlConnection = null;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_CW"].ConnectionString))
            {
                sqlConnection.Open();
                //HaarCascade is for face detection
                faceDetected = new HaarCascade("haarcascade_frontalface_default.xml");
                SqlDataReader reader = null;

                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Name, ImagePath FROM [Table]", sqlConnection);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        label = Convert.ToString(reader["Name"]);
                        labelsNames.Add(label);
                        label = Convert.ToString(reader["ImagePath"]);
                        labelsImages.Add(label);
                        trainingImages.Add(new Image<Gray, byte>(label));
                    }

                    //string Labelsinf = File.ReadAllText(Application.StartupPath + "/Faces/Faces.txt");
                    //string[] Labels = Labelsinf.Split(',');
                    ////The first label before , will be the number of faces saved.
                    //NumLables = Convert.ToInt16(Labels[0]);
                    //Count = NumLables;
                    //string FacesLoad;
                    //for (int i = 1; i < NumLables + 1; i++)
                    //{
                    //    FacesLoad = "face" + i + ".bmp";
                    //    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + $"/Faces/{FacesLoad}"));
                    //    labelsNames.Add(Labels[i]);
                    //}

                }
                catch (Exception)
                {
                    MessageBox.Show("Nothing in the Database");
                }
            };
        }

        private void brwsBttn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "(*.JPG, *.PNG) | *.JPG, *.PNG";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    previewBox.Image = Image.FromFile(ofd.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("File hasn't inserted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void addPerson_Click(object sender, EventArgs e)
        {/*
            if (previewBox.Image != null && textBox1.Text != null)
            {
                MCvAvgComp[][] DetectedFaces = grayFace.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
                foreach (MCvAvgComp f in DetectedFaces[0])
                {
                    TrainedFace = previewBox.Image.Copy(f.rect).Convert<Gray, byte>();
                    break;
                }
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labelsNames.Add(textBox1.Text);

                //File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");
                for (int i = 0; i < trainingImages.ToArray().Length; i++)
                {
                    trainingImages.ToArray()[i].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                    //File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labelsNames.ToArray()[i] + ",");

                }
                using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_CW"].ConnectionString))
                {
                    sqlConnection.Open();

                    SqlCommand command = new SqlCommand(
                    $"INSERT INTO [Table] (Name, ImagePath) VALUES (@Name, @ImagePath)",
                    sqlConnection);
                    command.Parameters.AddWithValue("Name", textBox1.Text);
                    command.Parameters.AddWithValue("ImagePath", Application.StartupPath + @"\Faces\face" + (trainingImages.ToArray().Length - 1) + ".bmp");

                    command.ExecuteNonQuery();
                }
                MessageBox.Show(textBox1.Text + " Added Successfully");
            }*/
        }

        private void start_Click(object sender, EventArgs e)
        {
            camera = new Capture();
            camera.QueryFrame();
            Application.Idle += new EventHandler(FrameProcedure);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Count++;
            grayFace = camera.QueryGrayFrame().Resize(600, 320, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            MCvAvgComp[][] DetectedFaces = grayFace.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
            foreach (MCvAvgComp f in DetectedFaces[0])
            {
                TrainedFace = Frame.Copy(f.rect).Convert<Gray, byte>();
                break;
            }
            TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            trainingImages.Add(TrainedFace);
            labelsNames.Add(textName.Text);

            //File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");
            for (int i = 0; i < trainingImages.ToArray().Length; i++)
            {
                trainingImages.ToArray()[i].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                //File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labelsNames.ToArray()[i] + ",");

            }
            using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_CW"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(
                $"INSERT INTO [Table] (Name, ImagePath) VALUES (@Name, @ImagePath)",
                sqlConnection);
                command.Parameters.AddWithValue("Name", textName.Text);
                command.Parameters.AddWithValue("ImagePath", Application.StartupPath + @"\Faces\face" + (trainingImages.ToArray().Length - 1) + ".bmp");

                command.ExecuteNonQuery();
            }
            MessageBox.Show(textName.Text + " Added Successfully");
        }

        private void FrameProcedure(object sender, EventArgs e)
        {
            Users.Add("");
            Frame = camera.QueryFrame().Resize(600, 320, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            grayFace = Frame.Convert<Gray, Byte>();
            MCvAvgComp[][] facesDetectedNow = grayFace.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
            foreach (MCvAvgComp f in facesDetectedNow[0])
            {
                result=Frame.Copy(f.rect).Convert<Gray,Byte>().Resize(100,100,Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                Frame.Draw(f.rect, new Bgr(Color.Green), 3);
                if(trainingImages.ToArray().Length !=0)
                {
                    MCvTermCriteria termCriterias = new MCvTermCriteria(Count, 0.001);
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labelsNames.ToArray(), 1500, ref termCriterias);
                    name = recognizer.Recognize(result);
                    //Frame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));
                    label3.Text = name;
                }
                Users.Add("");
            }
            cameraBox.Image = Frame;
            names = "";
            Users.Clear();
        }
    }
}
