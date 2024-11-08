using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace OV_Launcher
{
    public partial class Form1 : Form
    {

        // Oyun
        private string settingsFilePath1 = "settings1.txt"; // button1 için ayar dosyası
        private string settingsFilePath2 = "settings2.txt"; // button3 için ayar dosyası
        private string settingsFilePath3 = "settings3.txt"; // button5 için ayar dosyası
        private string settingsFilePath4 = "settings4.txt"; // Button11 için ayar dosyası
        private string settingsFilePath5 = "settings5.txt"; // Button 14 için ayar dosyası
        private string settingsFilePath6 = "settings6.txt"; // Button 17 için ayar dosyası
        private string settingsFilePath7 = "settings7.txt"; // Button 20 için ayar dosyası
        private string settingsFilePath8 = "settings8.txt"; // Button 23 için ayar dosyası
        private string settingsFilePath9 = "settings9.txt"; // Button 26 için ayar dosyası
        private string settingsFilePath10 = "settings10.txt"; // Button 29 için ayar dosyası
        private string settingsFilePath11 = "settings11.txt"; // Button 32 için ayar dosyası
        private string settingsFilePath12 = "settings12.txt"; // Button 35 için ayar dosyası

        // Label isimleri
        private string placeholderText1 = "";
        private string placeholderText2 = "";
        private string placeholderText3 = "";
        private string placeholderText4 = "";
        private string placeholderText5 = "";
        private string placeholderText6 = "";
        private string placeholderText7 = "";
        private string placeholderText8 = "";
        private string placeholderText9 = "";
        private string placeholderText10 = "";
        private string placeholderText11 = "";
        private string placeholderText12 = "";

        // Label dosyaları
        private string settingsLabelPath1 = "labelName1.txt";
        private string settingsLabelPath2 = "labelName2.txt";
        private string settingsLabelPath3 = "labelName3.txt";
        private string settingsLabelPath4 = "labelName4.txt";
        private string settingsLabelPath5 = "labelName5.txt";
        private string settingsLabelPath6 = "labelName6.txt";
        private string settingsLabelPath7 = "labelName7.txt";
        private string settingsLabelPath8 = "labelName8.txt";
        private string settingsLabelPath9 = "labelName9.txt";
        private string settingsLabelPath10 = "labelName10.txt";
        private string settingsLabelPath11 = "labelName11.txt";
        private string settingsLabelPath12 = "labelName12.txt";



        // Diğer ayarlar
        private bool manualChange = false;

        private static readonly string lastUpdateFilePath = "lastUpdate.txt";
        private static readonly string repoUrl = "https://api.github.com/repos/rmco3/OV-Launcher/commits";
        private bool showUpdateNotifications = true;

        public Form1()
        {
            InitializeComponent();
            CheckForUpdates();

            Form1_Load(this, EventArgs.Empty); // Form yüklendiğinde Form1_Load metodu çağrılacak
            this.Load += new System.EventHandler(this.Form1_Load);

            // Yer tutucu metnini ayarlayın
            SetPlaceholder(textBox1, "");
            SetPlaceholder(textBox2, "");
            SetPlaceholder(textBox3, "");
            SetPlaceholder(textBox4, "");
            SetPlaceholder(textBox5, "");
            SetPlaceholder(textBox6, "");
            SetPlaceholder(textBox7, "");
            SetPlaceholder(textBox8, "");
            SetPlaceholder(textBox9, "");
            SetPlaceholder(textBox10, "");
            SetPlaceholder(textBox11, "");
            SetPlaceholder(textBox12, "");

            // Güncelleme bildirimleri ayarını yükle
            showUpdateNotifications = Properties.Settings.Default.ShowUpdateNotifications;
            checkBox2.Checked = showUpdateNotifications;

            // Diğer ayarlar
            LoadSettings();


            // Yer tutucu metinleri ayarla
            SetPlaceholder(textBox1, placeholderText1);
            SetPlaceholder(textBox2, placeholderText2);
            SetPlaceholder(textBox3, placeholderText3);
            SetPlaceholder(textBox4, placeholderText4);
            SetPlaceholder(textBox5, placeholderText5);
            SetPlaceholder(textBox6, placeholderText6);
            SetPlaceholder(textBox7, placeholderText7);
            SetPlaceholder(textBox8, placeholderText8);
            SetPlaceholder(textBox9, placeholderText9);
            SetPlaceholder(textBox10, placeholderText10);
            SetPlaceholder(textBox11, placeholderText11);
            SetPlaceholder(textBox12, placeholderText12);


            // Tam ekran yapmayı devre dışı bırak
            MaximizeBox = false;

            // Yanlardan çekip büyütmeyi devre dışı bırak
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private async void CheckForUpdates()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "OV Launcher");

                string responseBody = await client.GetStringAsync(repoUrl);
                JArray commits = JArray.Parse(responseBody);

                string latestCommitHash = commits[0]["sha"].ToString();

                string previousCommitHash = File.Exists(lastUpdateFilePath) ? File.ReadAllText(lastUpdateFilePath) : string.Empty;

                if (latestCommitHash != previousCommitHash && showUpdateNotifications)
                {
                    // En yeni commit bilgilerini al ve kullanıcıya göster
                    string message = commits[0]["commit"]["message"].ToString();
                    string author = commits[0]["commit"]["author"]["name"].ToString();
                    string date = commits[0]["commit"]["author"]["date"].ToString();

                    string updateDetails = $"Yeni Güncelleme:\n\nTarih: {date}\nYazar: {author}\nMesaj: {message}";

                    MessageBox.Show(updateDetails, "Güncelleme Bildirimi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Yeni commit hash'ini dosyaya kaydet
                    File.WriteAllText(lastUpdateFilePath, latestCommitHash);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme kontrol edilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsLabelPath1))
            {
                string savedName1 = File.ReadAllText(settingsLabelPath1);
                label3.Text = savedName1;
            }
            else
            {
                label3.Text = "Oyun 1"; // Varsayılan eski isim
            }

            if (File.Exists(settingsLabelPath2))
            {
                string savedName2 = File.ReadAllText(settingsLabelPath2);
                label5.Text = savedName2;
            }
            else
            {
                label5.Text = "Oyun 2"; // Varsayılan ikinci eski isim
            }

            if (File.Exists(settingsLabelPath3))
            {
                string savedName3 = File.ReadAllText(settingsLabelPath3);
                label7.Text = savedName3;
            }
            else
            {
                label7.Text = "Oyun 3"; // Varsayılan üçüncü eski isim
            }

            if (File.Exists(settingsLabelPath4))
            {
                string savedName4 = File.ReadAllText(settingsLabelPath4);
                label8.Text = savedName4;
            }
            else
            {
                label8.Text = "Oyun 4"; // Varsayılan dördüncü eski isim
            }

            if (File.Exists(settingsLabelPath5))
            {
                string savedName5 = File.ReadAllText(settingsLabelPath5);
                label12.Text = savedName5;
            }
            else
            {
                label12.Text = "Oyun 5"; // Varsayılan beşinci eski isim
            }

            if (File.Exists(settingsLabelPath6))
            {
                string savedName6 = File.ReadAllText(settingsLabelPath6);
                label14.Text = savedName6;
            }
            else
            {
                label14.Text = "Oyun 6"; // Varsayılan altıncı eski isim
            }

            if (File.Exists(settingsLabelPath7))
            {
                string savedName7 = File.ReadAllText(settingsLabelPath7);
                label26.Text = savedName7;
            }
            else
            {
                label26.Text = "Oyun 7"; // Varsayılan yedinci eski isim
            }

            if (File.Exists(settingsLabelPath8))
            {
                string savedName8 = File.ReadAllText(settingsLabelPath8);
                label30.Text = savedName8;
            }
            else
            {
                label30.Text = "Oyun 8"; // Varsayılan sekizinci eski isim
            }

            if (File.Exists(settingsLabelPath9))
            {
                string savedName9 = File.ReadAllText(settingsLabelPath9);
                label32.Text = savedName9;
            }
            else
            {
                label32.Text = "Oyun 9"; // Varsayılan dokuzuncu eski isim
            }

            if (File.Exists(settingsLabelPath10))
            {
                string savedName10 = File.ReadAllText(settingsLabelPath10);
                label34.Text = savedName10;
            }
            else
            {
                label34.Text = "Oyun 10"; // Varsayılan dokuzuncu eski isim
            }

            if (File.Exists(settingsLabelPath11))
            {
                string savedName11 = File.ReadAllText(settingsLabelPath11);
                label38.Text = savedName11;
            }
            else
            {
                label38.Text = "Oyun 11"; // Varsayılan on birinci eski isim
            }

            if (File.Exists(settingsLabelPath11))
            {
                string savedName12 = File.ReadAllText(settingsLabelPath12);
                label51.Text = savedName12;
            }
            else
            {
                label51.Text = "Oyun 12"; // Varsayılan on birinci eski isim
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Ayar dosyalarını oku ve varsa label'lere yaz
            if (File.Exists(settingsFilePath1))
            {
                label1.Text = File.ReadAllText(settingsFilePath1);
            }

            if (File.Exists(settingsFilePath2))
            {
                label4.Text = File.ReadAllText(settingsFilePath2);
            }

            if (File.Exists(settingsFilePath3))
            {
                // Eklenen oyunları göstermek için label6'da okuyabiliriz
                label6.Text = File.ReadAllText(settingsFilePath3);
            }

            // settingsFilePath4 için label2'yi yükle
            if (File.Exists(settingsFilePath4))
            {
                label2.Text = File.ReadAllText(settingsFilePath4); // Button11 için ayar dosyası
            }

            if (File.Exists(settingsFilePath5))
            {
                label13.Text = File.ReadAllText(settingsFilePath5); // Button 14 için ayar dosyası
            }

            if (File.Exists(settingsFilePath6))
            {
                label15.Text = File.ReadAllText(settingsFilePath6); // Button 17 için ayar dosyası
            }

            if (File.Exists(settingsFilePath7))
            {
                label29.Text = File.ReadAllText(settingsFilePath7); // Button 20 için ayar dosyası
            }

            if (File.Exists(settingsFilePath8))
            {
                label31.Text = File.ReadAllText(settingsFilePath8); // Button 23 için ayar dosyası
            }

            if (File.Exists(settingsFilePath9))
            {
                label33.Text = File.ReadAllText(settingsFilePath9); // Button 26 için ayar dosyası
            }

            if (File.Exists(settingsFilePath10))
            {
                label35.Text = File.ReadAllText(settingsFilePath10); // Button 29 için ayar dosyası
            }

            if (File.Exists(settingsFilePath11))
            {
                label50.Text = File.ReadAllText(settingsFilePath11); // Button 32 için ayar dosyası
            }

            if (File.Exists(settingsFilePath10))
            {
                label52.Text = File.ReadAllText(settingsFilePath12); // Button 35 için ayar dosyası
            }


            // Daha önce eklediğimiz diğer resimleri geri yüklemek için kodlar:
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu))
            {
                pictureBox1.ImageLocation = Properties.Settings.Default.ResimYolu;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu2))
            {
                pictureBox2.ImageLocation = Properties.Settings.Default.ResimYolu2;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu3))
            {
                pictureBox3.ImageLocation = Properties.Settings.Default.ResimYolu3;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu4))
            {
                pictureBox4.ImageLocation = Properties.Settings.Default.ResimYolu4;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu5))
            {
                pictureBox5.ImageLocation = Properties.Settings.Default.ResimYolu5;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu6))
            {
                pictureBox6.ImageLocation = Properties.Settings.Default.ResimYolu6;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu8))
            {
                pictureBox8.ImageLocation = Properties.Settings.Default.ResimYolu8;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu9))
            {
                pictureBox9.ImageLocation = Properties.Settings.Default.ResimYolu9;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu10))
            {
                pictureBox10.ImageLocation = Properties.Settings.Default.ResimYolu10;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu11))
            {
                pictureBox11.ImageLocation = Properties.Settings.Default.ResimYolu11;
            }

            // ResimYolu13 (LinkLabel13 ve PictureBox13) için kaydedilen resmi geri yükle
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu13))
            {
                pictureBox13.ImageLocation = Properties.Settings.Default.ResimYolu13;
            }

            // ResimYolu14 (LinkLabel14 ve PictureBox14) için kaydedilen resmi geri yükle
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ResimYolu14))
            {
                pictureBox14.ImageLocation = Properties.Settings.Default.ResimYolu14;
            }

            // Başlangıçta uygulamayı çalıştırma
            manualChange = false; // Programatik olarak checkbox durumunu ayarlarken bayrağı false yapıyoruz

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            if (key.GetValue("OV Launcher") != null)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            manualChange = true; // Kullanıcı manuel olarak checkbox'ı değiştirebileceği duruma geldik



            // Diğer ayarlar
            KaranlikModuUygula();



            int selectedTheme = Properties.Settings.Default.SelectedTheme;

            if (selectedTheme == 1)
            {
                radioButton1.Checked = true;
                KaranlikModuUygula();
            }
            else if (selectedTheme == 2)
            {
                radioButton2.Checked = true;
                ApplyLightTheme();
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private void KaranlikModuUygula()
        {
            this.BackColor = Color.FromArgb(45, 45, 48);
            foreach (TabPage tab in tabPage.TabPages)
            {
                tab.BackColor = Color.FromArgb(45, 45, 48);
                tab.ForeColor = Color.White;
            }
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.BackColor = Color.FromArgb(45, 45, 48);
                    button.ForeColor = Color.White;
                }
            }
        }

        private void ApplyLightTheme()
        {
            this.BackColor = SystemColors.Control;
            foreach (TabPage tab in tabPage.TabPages)
            {
                tab.BackColor = SystemColors.Control;
                tab.ForeColor = SystemColors.ControlText;
            }
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.BackColor = SystemColors.Control;
                    button.ForeColor = SystemColors.ControlText;
                }
                else if (control is Label)
                {
                    Label label = (Label)control;
                    label.ForeColor = SystemColors.ControlText;
                }
                else if (control is TabControl)
                {
                    TabControl tabControl = (TabControl)control;
                    foreach (TabPage tab in tabControl.TabPages)
                    {
                        tab.BackColor = SystemColors.Control;
                        tab.ForeColor = SystemColors.ControlText;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath1, label1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartExeFile(label1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath2, label4); // Button3 için exe seçimi
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StartExeFile(label4.Text); // Button4 için exe çalıştırma
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath3, label6); // Button5 için exe seçimi
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StartExeFile(label6.Text); // Button6 için exe çalıştırma
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath4, label2); // Button11 için exe seçimi
        }

        private void button12_Click(object sender, EventArgs e)
        {
            StartExeFile(label2.Text); // Button12 için exe çalıştırma
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath5, label13); // Button 14 için exe seçimi
        }

        private void button15_Click(object sender, EventArgs e)
        {
            StartExeFile(label13.Text); // Button 15 için exe çalıştırma
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath6, label15); // Button 17 için exe seçimi
        }

        private void button18_Click(object sender, EventArgs e)
        {
            StartExeFile(label15.Text); // Button 18 için exe çalıştırma
        }

        private void button20_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath7, label29); // Button 20 için exe seçimi
        }

        private void button21_Click(object sender, EventArgs e)
        {
            StartExeFile(label29.Text); // Button 21 için exe çalıştırma
        }

        private void button23_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath8, label31); // Button 23 için exe seçimi
        }

        private void button24_Click(object sender, EventArgs e)
        {
            StartExeFile(label31.Text); // Button 24 için exe çalıştırma
        }

        private void button26_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath9, label33); // Button 26 için exe seçimi
        }

        private void button27_Click(object sender, EventArgs e)
        {
            StartExeFile(label33.Text); // Button 27 için exe çalıştırma
        }

        private void button29_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath10, label35); // Button 29 için exe seçimi
        }

        private void button30_Click(object sender, EventArgs e)
        {
            StartExeFile(label35.Text); // Button 30 için exe çalıştırma
        }

        private void button32_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath11, label50); // Button 32 için exe seçimi
        }

        private void button33_Click(object sender, EventArgs e)
        {
            StartExeFile(label50.Text); // Button 33 için exe çalıştırma
        }

        private void button35_Click(object sender, EventArgs e)
        {
            SelectExeFile(settingsFilePath12, label52); // Button 35 için exe seçimi
        }

        private void button36_Click(object sender, EventArgs e)
        {
            StartExeFile(label52.Text); // Button 36 için exe çalıştırma
        }


        private void SelectExeFile(string settingsFilePath, Label label)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "";
                openFileDialog.Filter = "Executable Files (*.exe)|*.exe";
                openFileDialog.Title = "Bir .exe dosyası seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen dosyanın yolunu label'de göster ve ayar dosyasına yaz
                    label.Text = openFileDialog.FileName;
                    File.WriteAllText(settingsFilePath, openFileDialog.FileName);
                }
            }
        }

        private void StartExeFile(string exePath)
        {
            if (!string.IsNullOrEmpty(exePath) && exePath.EndsWith(".exe"))
            {
                Process.Start(exePath);
            }
            else
            {
                MessageBox.Show("Lütfen bir .exe dosyası seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet
                Properties.Settings.Default.ResimYolu = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox'a yükle
                pictureBox1.ImageLocation = openFileDialog.FileName;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox2 için)
                Properties.Settings.Default.ResimYolu2 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox2'ye yükle
                pictureBox2.ImageLocation = openFileDialog.FileName;
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox3 için)
                Properties.Settings.Default.ResimYolu3 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox3'e yükle
                pictureBox3.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Button7 ile label1'i sıfırla
            label1.Text = "Oyun seçilmedi!"; // Label'ı boşalt
            File.WriteAllText(settingsFilePath1, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox1.Image = null; // PictureBox1'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath1))
            {
                File.Delete(settingsFilePath1);
            }

            // Oyun label ismini eski haline getir
            label3.Text = "Oyun 1"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath1))
            {
                File.Delete(settingsLabelPath1);
            }



            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Button8 ile label4'ü sıfırla
            label4.Text = "Oyun seçilmedi!"; // Label'ı boşalt
            File.WriteAllText(settingsFilePath2, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox2.Image = null; // PictureBox2'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath2))
            {
                File.Delete(settingsFilePath2);
            }

            // Oyun label ismini eski haline getir
            label5.Text = "Oyun 2"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath2))
            {
                File.Delete(settingsLabelPath2);
            }



            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu2 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Button9 ile label6'yı sıfırla
            label6.Text = "Oyun seçilmedi!"; // Label'ı boşalt
            File.WriteAllText(settingsFilePath3, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox3.Image = null; // PictureBox3'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath3))
            {
                File.Delete(settingsFilePath3);
            }

            // Oyun label ismini eski haline getir
            label7.Text = "Oyun 3"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath3))
            {
                File.Delete(settingsLabelPath3);
            }



            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu3 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox4 için)
                Properties.Settings.Default.ResimYolu4 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox4'e yükle
                pictureBox4.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Button10 ile label2'yi sıfırla
            label2.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath4, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox4.Image = null; // PictureBox4'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath4))
            {
                File.Delete(settingsFilePath4);
            }

            // Oyun label ismini eski haline getir
            label8.Text = "Oyun 4"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath4))
            {
                File.Delete(settingsLabelPath4);
            }



            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu4 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (manualChange) // Eğer değişiklik kullanıcı tarafından yapıldıysa
            {
                if (checkBox1.Checked)
                {
                    MessageBox.Show("Bilgisiyar çalışınca OV Launcher otomatik olarak başlatılacak!");

                    // OV Launcher'ı kayıt defterine ekleyerek başlangıçta çalışmasını sağlıyoruz
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key.SetValue("OV Launcher 1.3", Application.ExecutablePath);
                }
                else
                {
                    MessageBox.Show("Bilgisiyar çalışınca OV Launcher otomatik olarak başlatılmayacak!");

                    // OV Launcher'ı kayıt defterinden kaldırarak başlangıçtan çıkarıyoruz
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key.DeleteValue("OV Launcher 1.3", false);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                KaranlikModuUygula();
                SaveSelectedTheme(1);  // Seçimi kaydet
            }
        }

        // RadioButton2 (Aydınlık Mod) değiştiğinde çağrılan olay
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                ApplyLightTheme();
                SaveSelectedTheme(2);  // Seçimi kaydet
            }
        }

        // Seçilen temayı ayarlamak için fonksiyon
        private void SaveSelectedTheme(int theme)
        {
            Properties.Settings.Default.SelectedTheme = theme;
            Properties.Settings.Default.Save();  // Ayarı kaydet
        }

        // linkLabel5'e tıklandığında çalışacak event
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox5 için)
                Properties.Settings.Default.ResimYolu5 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox5'e yükle
                pictureBox5.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Button13 ile label13'yi sıfırla
            label13.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath5, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox5.Image = null; // PictureBox5'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath5))
            {
                File.Delete(settingsFilePath5);
            }

            // Oyun label ismini eski haline getir
            label12.Text = "Oyun 5"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath5))
            {
                File.Delete(settingsLabelPath5);
            }



            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu5 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // linkLabel6'ya tıklandığında çalışacak event
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox6 için)
                Properties.Settings.Default.ResimYolu6 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox6'ya yükle
                pictureBox6.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // Button10 ile label15'yi sıfırla
            label15.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath6, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox6.Image = null; // PictureBox6'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath6))
            {
                File.Delete(settingsFilePath6);
            }

            // Oyun label ismini eski haline getir
            label14.Text = "Oyun 6"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath6))
            {
                File.Delete(settingsLabelPath6);
            }




            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu6 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnShowMessage_Click(object sender, EventArgs e)
        {
            string licenseMessage = @"The MIT License (MIT)

Copyright © 2024 rmco3

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";

            MessageBox.Show(licenseMessage, "The MIT License (MIT)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox8 için)
                Properties.Settings.Default.ResimYolu8 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox8'e yükle
                pictureBox8.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // Button10 ile label29'i sıfırla
            label29.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath7, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox8.Image = null; // PictureBox8'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath7))
            {
                File.Delete(settingsFilePath7);
            }

            // Oyun label ismini eski haline getir
            label26.Text = "Oyun 7"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath7))
            {
                File.Delete(settingsLabelPath7);
            }



            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu8 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox9 için)
                Properties.Settings.Default.ResimYolu9 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox9'a yükle
                pictureBox9.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            // Button22 ile label31'i sıfırla
            label31.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath8, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox9.Image = null; // PictureBox9'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath8))
            {
                File.Delete(settingsFilePath8);
            }

            // Oyun label ismini eski haline getir
            label30.Text = "Oyun 8"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath8))
            {
                File.Delete(settingsLabelPath8);
            }



            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu9 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadDefaultSettings()
        {
            // Varsayılan ayarları buradan yükleyin
            radioButton1.Checked = true;  // Radio buton 1'in seçilmesini sağlar
            checkBox1.Checked = false;   // CheckBox1'in seçilmemesini sağlar

        }

        private void btnResetToDefaults_Click(object sender, EventArgs e)
        {
            // Kullanıcının onayını alın
            DialogResult result = MessageBox.Show("Tüm ayarları varsayılanlara döndürmek istediğinizden emin misiniz? Bu işlem geri alınamaz.",
                                                  "Onay bekleniyor",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Varsayılan ayarları yükleyin
                    LoadDefaultSettings();

                    // Buton işlevlerini çağırarak ek işlemleri yapın
                    button7_Click(sender, e);
                    button8_Click(sender, e);
                    button9_Click(sender, e);
                    button10_Click(sender, e);
                    button13_Click(sender, e);
                    button16_Click(sender, e);
                    button19_Click(sender, e);
                    button22_Click(sender, e);
                    button25_Click(sender, e);
                    button28_Click(sender, e);
                    button31_Click(sender, e);
                    button34_Click(sender, e);

                    // Kullanıcıya işlem tamamlandığını bildirin
                    MessageBox.Show("Ayarlar varsayılanlara döndürüldü.",
                                    "Bilgi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bilgi verin
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox10 için)
                Properties.Settings.Default.ResimYolu10 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox10'a yükle
                pictureBox10.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            // Button25 ile label33'i sıfırla
            label33.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath9, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox10.Image = null; // PictureBox10'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath9))
            {
                File.Delete(settingsFilePath9);
            }

            // Oyun label ismini eski haline getir
            label32.Text = "Oyun 9"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath9))
            {
                File.Delete(settingsLabelPath9);
            }

            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu10 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox11 için)
                Properties.Settings.Default.ResimYolu11 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox11'e yükle
                pictureBox11.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            // Button28 ile label35'i sıfırla
            label35.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath10, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox11.Image = null; // PictureBox10'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath10))
            {
                File.Delete(settingsFilePath10);
            }

            // Oyun label ismini eski haline getir
            label34.Text = "Oyun 10"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath10))
            {
                File.Delete(settingsLabelPath10);
            }

            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu11 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Kullanıcının onayını alın
            DialogResult result = MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?",
                                                  "Onay bekleniyor",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bilgi verin
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu özellik şu anda çalışmıyor. En iyi deneyimi sağlamak için, bu özellik sonraki güncellemelerde düzeltilecektir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = string.Empty;
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        private void BtnAddName1_Click(object sender, EventArgs e)
        {
            string newName = textBox1.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText1 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Yeni ismi giriyoruz
            label3.Text = newName;
            File.WriteAllText(settingsLabelPath1, newName); // Yeni ismi dosyaya kaydet
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName2_Click(object sender, EventArgs e)
        {
            string newName = textBox2.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText2 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label5.Text = newName;
            File.WriteAllText(settingsLabelPath2, newName); // Yeni ismi dosyaya kaydet
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName3_Click(object sender, EventArgs e)
        {
            string newName = textBox3.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText3 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label7.Text = newName;
            File.WriteAllText(settingsLabelPath3, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName4_Click(object sender, EventArgs e)
        {
            string newName = textBox4.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText4 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label8.Text = newName;
            File.WriteAllText(settingsLabelPath4, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName5_Click(object sender, EventArgs e)
        {
            string newName = textBox5.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText5 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label12.Text = newName;
            File.WriteAllText(settingsLabelPath5, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName6_Click(object sender, EventArgs e)
        {
            string newName = textBox6.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText6 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label14.Text = newName;
            File.WriteAllText(settingsLabelPath6, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName7_Click(object sender, EventArgs e)
        {
            string newName = textBox7.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText7 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label26.Text = newName;
            File.WriteAllText(settingsLabelPath7, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName8_Click(object sender, EventArgs e)
        {
            string newName = textBox8.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText8 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label30.Text = newName;
            File.WriteAllText(settingsLabelPath8, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnAddName9_Click(object sender, EventArgs e)
        {
            string newName = textBox9.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText9 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label32.Text = newName;
            File.WriteAllText(settingsLabelPath9, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddName10_Click(object sender, EventArgs e)
        {
            string newName = textBox10.Text;

            // Yer tutucu metni algıla
            if (newName == placeholderText10 || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Lütfen geçerli bir isim girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            label34.Text = newName;
            File.WriteAllText(settingsLabelPath10, newName);
            MessageBox.Show("Yeni isim girildi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox13 için)
                Properties.Settings.Default.ResimYolu13 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox13'e yükle
                pictureBox13.ImageLocation = openFileDialog.FileName;
            }
        }

        private void BtnAddName11_Click(object sender, EventArgs e)
        {
            // textBox11'in içeriğini kontrol et
            if (string.IsNullOrWhiteSpace(textBox11.Text) || textBox11.Text == "İsim girin...")
            {
                MessageBox.Show("Lütfen geçerli bir isim girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // İsim değiştir ve kaydet
            label38.Text = textBox11.Text;
            File.WriteAllText(settingsLabelPath11, textBox11.Text);
            MessageBox.Show("Yeni isim girildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            // Button28 ile label35'i sıfırla
            label50.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath11, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox13.Image = null; // PictureBox10'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath11))
            {
                File.Delete(settingsFilePath11);
            }

            // Oyun label ismini eski haline getir
            label38.Text = "Oyun 11"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath11))
            {
                File.Delete(settingsLabelPath11);
            }

            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu13 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // OpenFileDialog'u başlatıyoruz
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir resim dosyası seçin";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            // Eğer kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosya yolunu ayarlara kaydet (PictureBox14 için)
                Properties.Settings.Default.ResimYolu14 = openFileDialog.FileName;
                Properties.Settings.Default.Save(); // Ayarları kaydetmeyi unutma!

                // Seçilen dosya yolundan resmi PictureBox14'e yükle
                pictureBox14.ImageLocation = openFileDialog.FileName;
            }
        }

        private void BtnAddName12_Click(object sender, EventArgs e)
        {
            // textBox12'nin içeriğini kontrol et
            if (string.IsNullOrWhiteSpace(textBox12.Text) || textBox12.Text == "İsim girin...")
            {
                MessageBox.Show("Lütfen geçerli bir isim girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // İsim değiştir ve kaydet
            label51.Text = textBox12.Text;
            File.WriteAllText(settingsLabelPath12, textBox12.Text);
            MessageBox.Show("Yeni isim girildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            // Button28 ile label35'i sıfırla
            label52.Text = "Oyun seçilmedi!";
            File.WriteAllText(settingsFilePath12, "Oyun seçilmedi!"); // Ayar dosyasına "Oyun seçilmedi!" yaz
            pictureBox14.Image = null; // PictureBox14'deki resmi boşalt

            // Dosya sil
            if (File.Exists(settingsFilePath12))
            {
                File.Delete(settingsFilePath12);
            }

            // Oyun label ismini eski haline getir
            label51.Text = "Oyun 12"; // Eski ismi buraya yazın

            // Dosyayı sil
            if (File.Exists(settingsLabelPath12))
            {
                File.Delete(settingsLabelPath12);
            }

            // Ayar dosyasındaki resmi sıfırla
            Properties.Settings.Default.ResimYolu14 = ""; // Resim yolunu boşalt
            Properties.Settings.Default.Save(); // Ayarları kaydet

            MessageBox.Show("Oyun sıfırlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel12_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // GitHub bağlantısını açmak için
            string url = "https://github.com/rmco3/OV-Launcher";

            // Varsayılan tarayıcıda URL'yi aç
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // YouTube bağlantısını açmak için
            string url = "https://www.youtube.com/@rmco3/videos";

            // Varsayılan tarayıcıda URL'yi aç
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            showUpdateNotifications = checkBox2.Checked;
            Properties.Settings.Default.ShowUpdateNotifications = showUpdateNotifications;
            Properties.Settings.Default.Save();
        }
    }
}

















