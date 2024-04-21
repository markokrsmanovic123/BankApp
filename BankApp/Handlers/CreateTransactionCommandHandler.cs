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
            var fileName = request.FormFile.FileName;
            var xmlContent = new XmlDocument();
            xmlContent.Load(request.FormFile.OpenReadStream());

            var transactionNode = xmlContent.SelectSingleNode("//TransakcioniRacunPrivredaIzvod/Stavke");

            var transactionModel = await _transactionMapper.MapXmlToModel(transactionNode, fileName);

            _unitOfWork.RaiffeisenRsdRepository.Add(transactionModel);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
