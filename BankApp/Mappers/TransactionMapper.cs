using BankApp.Models;
using System.Text.RegularExpressions;
using System.Xml;

namespace BankApp.Mappers
{
    public class TransactionMapper : ITransactionMapper
    {
        public async Task<RaiffeisenRsd> MapXmlToModel(XmlNode xmlNode, string fileName)
        {
            string beforeTrim = xmlNode.Attributes["BrojZaReklamaciju"]?.Value;
            string pattern = @"\d{19}//";
            string afterTrim = Regex.Replace(beforeTrim, pattern, "");

            var model = new RaiffeisenRsd
            {
                TransactionDate = afterTrim,
                Reference = xmlNode.Attributes["Referenca"]?.Value,
                Recipient = xmlNode.Attributes["NalogKorisnik"]?.Value,
                TransactionDescription = xmlNode.Attributes["Opis"]?.Value,
                Debit = decimal.Parse(xmlNode.Attributes["Duguje"]?.Value ?? "0"),
                FileName = fileName
            };

            return await Task.FromResult(model);
        }
    }
}
