﻿using System;
using System.Collections.Generic;
using PrescriptionFiller.Model;
using PrescriptionFiller.Services;

namespace PrescriptionFiller.LocalDatabase
{
    public class LocalPrescriptionDatabase
    {
        private static LocalPrescriptionDatabase _instance;

        public static LocalPrescriptionDatabase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LocalPrescriptionDatabase();

                return _instance;
            }
        }

        static object locker = new object();
        //      SQLiteConnection database;
        public LocalPrescriptionDatabase()
        {
            //          database = DependencyService.Get<ISQLite> ().GetConnection ();
            // create the tables
            //          database.CreateTable<PrescriptionItem>();
        }

        public IEnumerable<PrescriptionItem> GetItems(int userId)
        {
            lock (locker)
            {
                return LocalPrescriptionDataFileImpl.Instance.getPrescriptionItems(userId);
                //return database.Query<PrescriptionItem>("SELECT * FROM [PrescriptionItem] WHERE [userId] = ?", userId);
            }
        }

        public int SaveItem(PrescriptionItem item)
        {
            lock (locker)
            {
                Console.WriteLine("SAVE_ITEM_RAHUL: " + item.ID);

                //return item.ID;
                if (item.ID != 0)
                {
                    //database.Update(item);
                    LocalPrescriptionDataFileImpl.Instance.savePrescriptionItem(item);
                    return item.ID;
                }
                else
                {
                    return LocalPrescriptionDataFileImpl.Instance.savePrescriptionItem(item);
                    //return database.Insert(item);
                }
            }
        }

    }
}
