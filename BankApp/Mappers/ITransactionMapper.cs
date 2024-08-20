using BankApp.Models;
using System.Xml;

namespace BankApp.Mappers
{
    public interface ITransactionMapper
    {
        Task<Transaction> MapXmlToModel(XmlNode xmlNode, string fileName);
    }
}
