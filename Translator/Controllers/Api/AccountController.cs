using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TranslatorDataAccess;
using TranslatorEntities;

namespace Translator.Controllers.Api
{
    public class AccountController : ApiController
    {
        private readonly IHistoryRecordRepository _repository;
        public AccountController(IHistoryRecordRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TranslatorEntities.HistoryRecord> GetUsers(int id)
        {
            // return _repository.GetAll().AsEnumerable();
            return new List<TranslatorEntities.HistoryRecord>()
            {
                new HistoryRecord()
                {
                    UserId = id,
                    SourceText = "test",
                }
            };
        }
    }
}
