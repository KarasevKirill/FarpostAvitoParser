using System;
using System.Windows.Forms;

namespace Parser
{
    public partial class Form1 : Form
    {
        private string settingsPath;

        private Settings settings;

        private FolderBrowserDialog folderBrowser;

        public Form1()
        {
            InitializeComponent();

            settingsPath = $"{Application.StartupPath}\\settings.xml";

            folderBrowser = new FolderBrowserDialog();

            GetSettings();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// Получает объект настроек и выставляет состояние элементов формы согласно состоянию полей объекта настроек
        /// </summary>
        private void GetSettings()
        {
            settings = Settings.GetSettings(settingsPath);           

            try
            {
                foreach (var seller in settings.FarpostSellerNames)
                {
                    txtFarpostSellersNames.Text += $"{seller}\r\n";
                }

                foreach (var seller in settings.AvitoSellerNames)
                {
                    txtAvitoSellersNames.Text += $"{seller}\r\n";
                }

                foreach (var keyword in settings.Keywords)
                {
                    txtKeywords.Text += $"{keyword}\r\n";
                }

                foreach (var trashword in settings.Trashwords)
                {
                    txtTrashwords.Text += $"{trashword}\r\n";
                }

                foreach (var city in settings.Cities)
                {
                    txtCitiesFilter.Text += $"{city}\r\n";
                }

                txtSavePath.Text = settings.SaveDataPath;

                checkSwitchOff.Checked = settings.Switch;
                checkKeywords.Checked = settings.UseKeywords;
                checkTrashwords.Checked = settings.UseTrashwords;
                checkCitiesFilter.Checked = settings.UseCitiesFilter;
                checkUseFarpost.Checked = settings.UseFarpost;
                checkUseFarpost.Checked = settings.UseFarpost;
                checkUseAvito.Checked = settings.UseAvito;
            } catch { }
        }

        /// <summary>
        /// Сохраняет состояние элементов формы в объекте настроек и вызывает метод сохранения
        /// в файл
        /// </summary>
        private void SaveSettings()
        {

            settings.SaveDataPath = txtSavePath.Text;
            settings.FarpostSellerNames = GetFarpostSellersArray();
            settings.AvitoSellerNames = GetAvitoSellersArray();
            settings.Keywords = GetKeywordsArray();
            settings.Trashwords = GetTrashwordsArray();
            settings.Cities = GetCitiesArray();

            settings.Switch = checkSwitchOff.Checked;
            settings.UseKeywords = checkKeywords.Checked;
            settings.UseTrashwords = checkTrashwords.Checked;
            settings.UseCitiesFilter = checkCitiesFilter.Checked;
            settings.UseFarpost = checkUseFarpost.Checked;
            settings.UseAvito = checkUseAvito.Checked;

            Settings.SaveSettings(settingsPath, settings);
        }

        /// <summary>
        /// Возвращает массив ников продавцов с фарпоста
        /// </summary>
        /// <returns></returns>
        private string[] GetFarpostSellersArray()
        {
            return txtFarpostSellersNames.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Возвращает массив ников продавцов с авито
        /// </summary>
        /// <returns></returns>
        private string[] GetAvitoSellersArray()
        {
            return txtAvitoSellersNames.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Возвращает массив ключевых слов для отбора
        /// </summary>
        /// <returns></returns>
        private string[] GetKeywordsArray()
        {
            return txtKeywords.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Возвращает массив имен городов для фильтра
        /// </summary>
        /// <returns></returns>
        private string[] GetCitiesArray()
        {
            return txtCitiesFilter.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Возвращает массив лишних слов для удаления
        /// </summary>
        /// <returns></returns>
        private string[] GetTrashwordsArray()
        {
            return txtTrashwords.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void btnSetSavePath_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
                txtSavePath.Text = folderBrowser.SelectedPath;
        }
    }
}
