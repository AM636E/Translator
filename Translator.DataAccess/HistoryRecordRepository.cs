using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorEntities = Translator.Domain;
using System.Data.Entity;
using Translator.Domain;

namespace TranslatorDataAccess
{
    public class HistoryRecordRepository : IHistoryRecordRepository
    {
        private readonly string _connectionString;

        public HistoryRecordRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public List<TranslatorEntities.HistoryRecord> GetAll()
        {
            using(TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                return context.HistoryRecords.ToList();
            }
        }

        public List<TranslatorEntities.HistoryRecord> GetByLanguage(string lang, int userId = 0)
        {
            List<TranslatorEntities.HistoryRecord> records = null;
            if(userId == 0)
            {
                records = GetByUser(userId);
            }
            else
            {
                records = GetAll();
            }

            return records.Where(record => record.SourceLanguage == lang).ToList();
        }

        public List<TranslatorEntities.HistoryRecord> GetByUser(int userId)
        {
            return GetAll().Where(r => r.UserId == userId).ToList();
        }


        public void AddRecord(HistoryRecord record)
        {
            using(TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                record.Time = DateTime.Now;
                context.HistoryRecords.Add(record);
                context.SaveChanges();
            }
        }

        public void DeleteRecord(HistoryRecord record)
        {
            using (TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                context.HistoryRecords.Remove(record);
                context.SaveChanges();
            }
        }
    }
}
