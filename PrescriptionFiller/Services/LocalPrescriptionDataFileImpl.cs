using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;

[assembly: Xamarin.Forms.Dependency(typeof(LocalPrescriptionDataFileImpl))]
namespace PrescriptionFiller.Services
{
    public class LocalPrescriptionDataFileImpl : ILocalPrescriptionDataFile
    {
        private const string DATA_FILE_NAME = "prescriptionfiles_new.txt";
        private const string NEXT_ID_FILE = "nextid.txt";
        private static LocalPrescriptionDataFileImpl _instance;

        public static LocalPrescriptionDataFileImpl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LocalPrescriptionDataFileImpl();

                return _instance;
            }
        }

        public IEnumerable<PrescriptionItem> getPrescriptionItems(int userId)
        {
            return getPrescriptionItems(userId, true);
        }

        public IEnumerable<PrescriptionItem> getPrescriptionItems()
        {
            return getPrescriptionItems(-1, false);
        }

        private IEnumerable<PrescriptionItem> getPrescriptionItems(int userId, bool filterByUserId)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, DATA_FILE_NAME);

            Console.WriteLine("LOCAL_FILE_PATH = " + filePath);

            if (!System.IO.File.Exists(filePath))
            {

                Console.WriteLine("CREATE_NEW_FILE");
                File.Create(filePath);
                return new PrescriptionItem[0];
            }

            string text = System.IO.File.ReadAllText(filePath);

            Console.WriteLine("FILE_CONTENT = " + text);

            //            HomePage.Instance.DisplayAlert("LocalPrescriptionDataFileImpl.getPrescriptionItems","text: "+text +
            //                                           "\n userId: "+userId,"cancel");
            if (text == null || string.IsNullOrEmpty(text))
            {
                return new PrescriptionItem[0];
            }

            PrescriptionItem[] prescriptionItemsArr = text != null ? JsonConvert.DeserializeObject<PrescriptionItem[]>(text) : null;
            PrescriptionItem[] results = null;
            if (prescriptionItemsArr != null)
            {

                Console.WriteLine("Call-09");

                // prescriptionItemsArr = Array.FindAll(prescriptionItemsArr, c => c.thumbPath != null && File.Exists(c.thumbPath));
            }
            if (filterByUserId && prescriptionItemsArr != null)
            {

                Console.WriteLine("Call-10");

                results = prescriptionItemsArr;

                //  results = Array.FindAll(prescriptionItemsArr, c => c.userId == userId);
            }
            else
            {

                Console.WriteLine("Call-11");

                results = prescriptionItemsArr;
            }

            // Console.WriteLine("Call-12 = " + results.ToString());
            return results;
        }

        public int savePrescriptionItem(PrescriptionItem prescriptionItem)
        {
            IEnumerable<PrescriptionItem> currentPrescriptionItemsEnum = getPrescriptionItems();
            IList<PrescriptionItem> newPrescriptionItems = new List<PrescriptionItem>();
            bool prescriptionItemIsInFile = false;
            int itemCounter = 0;
            if (currentPrescriptionItemsEnum != null)
            {
                foreach (PrescriptionItem prescriptionItemInFile in currentPrescriptionItemsEnum)
                {
                    if (prescriptionItemInFile.ID == prescriptionItem.ID)
                    {
                        newPrescriptionItems.Insert(itemCounter, prescriptionItem);
                        prescriptionItemIsInFile = true;
                    }
                    else
                    {
                        newPrescriptionItems.Insert(itemCounter, prescriptionItemInFile);
                    }
                    itemCounter++;
                }
                if (!prescriptionItemIsInFile)
                {
                    prescriptionItem.ID = getNextId();
                    newPrescriptionItems.Insert(itemCounter, prescriptionItem);
                }
            }
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, DATA_FILE_NAME);

            Console.WriteLine("RAHUL_SAAVED_PATH: " + documentsPath);

            string text = JsonConvert.SerializeObject(newPrescriptionItems);
            System.IO.File.WriteAllText(filePath, text);
            return prescriptionItem.ID;
        }
        private int getNextId()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, NEXT_ID_FILE);
            string text = null;
            if (System.IO.File.Exists(filePath))
            {
                text = System.IO.File.ReadAllText(filePath);
            }
            else
            {
                text = null;
            }
            if (text == null || text.Equals(""))
            {
                text = "1";
            }
            int nextId;
            Int32.TryParse(text, out nextId);
            nextId++;
            string newText = nextId.ToString();
            System.IO.File.WriteAllText(filePath, newText);
            return nextId;
        }
    }
}
