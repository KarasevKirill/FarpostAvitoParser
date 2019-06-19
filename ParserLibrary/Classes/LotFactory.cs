using ParserLibrary.Interfaces;
using System;

namespace ParserLibrary.Classes
{
    public class LotFactory<LotType> : ILotFactory where LotType : ILot
    {
        private readonly Type _lotType;

        public LotFactory()
        {
            _lotType = typeof(LotType);

            if (_lotType == null)
                throw new ArgumentException();
        }

        public ILot GetNewLot()
        {
            return Activator.CreateInstance(_lotType) as ILot;
        }
    }
}
