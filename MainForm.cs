using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;

namespace JournalParserCore
{
    public partial class MainForm : Form
    {
        private static string _pathFile;
        private string _xmlFilePath;
        private bool _firstLaunch = true;
        public MainForm() => InitializeComponent();

        private void ButtonBrowseFileClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Файлы Excel (*.xls; *.xlsx)|*.xls;*.xlsx|Все файлы (*.*)|*.*";

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _pathFile = openFileDialog.FileName;
                _xmlFilePath = Path.Combine(Path.GetDirectoryName(_pathFile), "DevicesLogs.xml");
                ConvertExcelToXml(_pathFile, _xmlFilePath);
            }
        }

        private void ConvertExcelToXml(string excelFilePath, string xmlFilePath)
        {
            FileInfo excelFile = new(excelFilePath);

            using (XmlWriter xmlWriter = XmlWriter.Create(xmlFilePath))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\n");
                xmlWriter.WriteStartElement("root");
                xmlWriter.WriteWhitespace("\n");

                using (var package = new OfficeOpenXml.ExcelPackage(excelFile))
                {
                    // Получаем данные из Excel
                    var firstSheet = package.Workbook.Worksheets[0];    // Первый лист
                    int rowCountFirstSheet = firstSheet.Dimension.Rows; // Определения количества строк в столбце

                    xmlWriter.WriteStartElement("GuidesDevicesLogsUnifay"); // Блок GuidesDevicesLogsUnifay
                    xmlWriter.WriteWhitespace("\n");

                    for (int row = 2; row <= rowCountFirstSheet; row++)
                    {
                        string eventCode = firstSheet.Cells[row, 2].Value?.ToString();  // В квадратных скобка номер ряда и столбца
                        string eventText = (10200 + row - 2).ToString();

                        if (!string.IsNullOrEmpty(eventCode) && !string.IsNullOrEmpty(eventText))
                        {
                            xmlWriter.WriteWhitespace("\t");
                            xmlWriter.WriteStartElement("Event");
                            xmlWriter.WriteAttributeString("Code", eventCode.Trim());
                            xmlWriter.WriteAttributeString("Text", eventText.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteWhitespace("\n");
                        }
                    }

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\n\n");

                    var secondSheet = package.Workbook.Worksheets[1];       // Второй лист
                    int rowCountSecondSheet = firstSheet.Dimension.Rows;    // Определения количества строк в столбце

                    xmlWriter.WriteStartElement("AdditionalParameters");    // Блок AdditionalParameters
                    xmlWriter.WriteWhitespace("\n");

                    for (int row = 2; row <= rowCountSecondSheet; row++)
                    {
                        string parameterCode = secondSheet.Cells[row, 1].Value?.ToString();
                        string type = secondSheet.Cells[row, 2].Value?.ToString();
                        string parameterText = (10100 + row - 2).ToString();

                        switch (type)
                        {
                            case "вещественное":
                                type = "float";
                                break;
                            case "строка":
                                type = "string";
                                break;
                            case "целое":
                                type = "integer";
                                break;
                            case "дата/время":
                                type = "datetime";
                                break;
                        }

                        if (!string.IsNullOrEmpty(parameterCode) && !string.IsNullOrEmpty(type))
                        {
                            xmlWriter.WriteWhitespace("\t");
                            xmlWriter.WriteStartElement("Parameter");
                            xmlWriter.WriteAttributeString("Code", parameterCode.Trim());
                            xmlWriter.WriteAttributeString("Type", type.Trim());
                            xmlWriter.WriteAttributeString("Text", parameterText.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteWhitespace("\n");
                        }
                    }

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\n\n");

                    xmlWriter.WriteStartElement("GuideTranslate"); // Блок GuideTranslate
                    xmlWriter.WriteWhitespace("\n");
                    xmlWriter.WriteWhitespace("\t");
                    xmlWriter.WriteStartElement("Ru");
                    xmlWriter.WriteWhitespace("\n");

                    // Текст с дополнительных параметров
                    for (int row = 2; row <= rowCountSecondSheet; row++)
                    {
                        string additionalText = secondSheet.Cells[row, 3].Value?.ToString();
                        string additionalTranslateCode = (10100 + row - 2).ToString();

                        if (additionalText != null && additionalText.Contains("\n"))
                        {
                            additionalText = additionalText.Replace("\n","");
                        }

                        if (!string.IsNullOrEmpty(additionalText) && !string.IsNullOrEmpty(additionalTranslateCode))
                        {
                            xmlWriter.WriteWhitespace("\t\t");
                            xmlWriter.WriteStartElement("Index");
                            xmlWriter.WriteAttributeString("Code", additionalTranslateCode.Trim());
                            xmlWriter.WriteAttributeString("Translate", additionalText.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteWhitespace("\n");
                        }
                    }

                    // Текст с унифицированного журнала
                    for (int row = 2; row <= rowCountFirstSheet; row++)
                    {
                        string translateCode = (10200 + row - 2).ToString();
                        string translateText = firstSheet.Cells[row, 3].Value?.ToString();

                        // Проверка на ненужные символы
                        if (translateText != null && translateText.Contains("\n"))
                        {
                            translateText = translateText.Replace("\n", "");
                        }
                        if (translateText != null && translateText.Contains("\""))
                        {
                            translateText = translateText.Replace("\"", "");
                        }

                        if (!string.IsNullOrEmpty(translateCode) && !string.IsNullOrEmpty(translateText))
                        {
                            xmlWriter.WriteWhitespace("\t\t");
                            xmlWriter.WriteStartElement("Index");
                            xmlWriter.WriteAttributeString("Code", translateCode.Trim());
                            xmlWriter.WriteAttributeString("Translate", translateText.Trim());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteWhitespace("\n");
                        }
                    }

                    xmlWriter.WriteWhitespace("\t");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\n\t");
                    xmlWriter.WriteStartElement("Eng");
                    xmlWriter.WriteWhitespace("\n\t");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\n");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\n");
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            if (_firstLaunch)
            {
                label.Location = new Point(label.Location.X + 29, label.Location.Y);
                _firstLaunch = false;
            }
            
            label.Text = "Done";
        }

        // Косметика
        private void PictureBoxCloseClick(object sender, EventArgs e) => Application.Exit();

        private void LinkToGitClick(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/SergeySeptember/JournalParser",
                UseShellExecute = true
            });
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void MoveForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void MainFormMouseDown(object sender, MouseEventArgs e) => MoveForm();

        private void LabelLogoMouseDown(object sender, MouseEventArgs e) => MoveForm();
    }
}