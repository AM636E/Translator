using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Domain;
namespace TranslatorDataAccess
{
    public interface IHistoryRecordRepository
    {
        List<HistoryRecord> GetAll();
        List<HistoryRecord> GetByLanguage(string lang, int userId = 0);
        List<HistoryRecord> GetByUser(int userId);
        void AddRecord(HistoryRecord record);
        void DeleteRecord(HistoryRecord record);
    }
}
