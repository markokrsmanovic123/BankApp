using BankApp.Commands;
using BankApp.Mappers;
using BankApp.Repository.Interfaces;
using MediatR;
using System.Xml;

namespace BankApp.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Unit>
    {
        public IUnitOfWork _unitOfWork { get; private set; }
        public ITransactionMapper _transactionMapper;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, ITransactionMapper transactionMapper)
        {
            _unitOfWork = unitOfWork;
            _transactionMapper = transactionMapper;
        }

        public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.FormFile == null || request.FormFile.Length == 0)
            {
                throw new ArgumentException("No file uploaded");
            }

            var fileName = request.FormFile.FileName;
            var xmlContent = new XmlDocument();
            
            try
            {
                xmlContent.Load(request.FormFile.OpenReadStream());
            }
            catch (Exception ex)
            {
                throw new XmlException("The file that was provided is not of valid format", ex.InnerException);
            }

            var transactionNode = xmlContent.SelectSingleNode("//TransakcioniRacunPrivredaIzvod/Stavke");

            if (transactionNode == null) 
            {
                throw new XmlException("XML structure is not valid.");
            }

            var transactionModel = await _transactionMapper.MapXmlToModel(transactionNode, fileName);

            _unitOfWork.RaiffeisenRsdRepository.Add(transactionModel);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
